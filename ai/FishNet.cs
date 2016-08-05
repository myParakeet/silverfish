using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HREngine.Bots
{
    public sealed class FishNet
    {
        private static readonly FishNet instance = new FishNet();

        static FishNet() { } // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit

        public static FishNet Instance
        {
            get
            {
                return instance;
            }
        }

        private FishNet()
        {

        }

        private TcpListener listener;
        private TcpClient client;
        private bool isServer = false;

        public void startServer()
        {
            isServer = true;
            try
            {
                listener = new TcpListener(IPAddress.Any, Settings.Instance.tcpPort);
                listener.Start();
                Helpfunctions.Instance.ErrorLog($"[Network] Listening for client on port {Settings.Instance.tcpPort}");
                listener.BeginAcceptTcpClient(handleConnectionAsync, listener);
            }
            catch (SocketException)
            {
                Helpfunctions.Instance.ErrorLog($"[Network] Cant bind to port {Settings.Instance.tcpPort}");
            }
        }

        private void handleConnectionAsync(IAsyncResult result)
        {
            TcpClient newclient = listener.EndAcceptTcpClient(result);
            if (!isConnected(newclient.Client)) return;

            client = newclient; //new connections replace old, only 1 is intended
            Helpfunctions.Instance.ErrorLog($"[Network] New connection from {getIp(client.Client)}");
        }

        public Task startClient(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => Instance.startClientAsync(cancellationToken), cancellationToken);
        }

        public async Task startClientAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    client = new TcpClient();
                    client.Connect(Settings.Instance.netAddress, Settings.Instance.tcpPort);
                    
                    while (client.Connected)
                    {
                        await Task.Delay(100, cancellationToken);
                    }
                }
                catch (SocketException)
                {
                    Helpfunctions.Instance.ErrorLog("[Network] Connection Error: SocketException");
                }
                catch (Exception ex)
                {
                    Helpfunctions.Instance.ErrorLog($"[Network] Connection Error: {ex}");
                }

                await Task.Delay(5000, cancellationToken);
            }
        }

        // test if client is still connected
        public static bool isConnected(Socket client)
        {
            bool blockState = client.Blocking;

            try
            {
                byte[] tmp = new byte[1];

                client.Blocking = false;
                client.Send(tmp, 0, 0);
                return true;
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                return (e.NativeErrorCode.Equals(10035));
            }
            finally
            {
                client.Blocking = blockState;
            }
        }

        private string getIp(Socket s)
        {
            IPEndPoint remoteIpEndPoint = s.RemoteEndPoint as IPEndPoint;
            return remoteIpEndPoint?.ToString();
        }
        
        public void sendMessage(string msg)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                StreamWriter s = new StreamWriter(stream);

                s.WriteLine(msg);
                s.WriteLine("");
                s.Flush();
                Helpfunctions.Instance.ErrorLog($"[Network] Send Message: {msg}");
            }
            catch (Exception e)
            {
                Helpfunctions.Instance.ErrorLog($"[Network] Send Message exception: {e}");
            }
        }
        
        public KeyValuePair<string, string> readMessage()
        {
            if (client == null || !client.Connected) return new KeyValuePair<string, string>("","");
            KeyValuePair<string, string> msg = readLines(client.GetStream());
            Helpfunctions.Instance.ErrorLog($"[Network] Message: {msg.Key}\r\n{msg.Value}");

            switch (msg.Key)
            {
                case "crrntbrd.txt":
                    break;
                case "curdeck.txt":
                    break;
                case "actionstodo.txt":
                    break;
                default:
                    Helpfunctions.Instance.ErrorLog($"[Network] Unknown Message Type");
                    break;
            }
            return msg;
        }


        private static KeyValuePair<string, string> readLines(Stream source)
        {
            string header = "";
            string lines = "";

            try
            {
                StreamReader reader = new StreamReader(source);
                    header = reader.ReadLine();
                    string line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        lines += "\r\n" + line;
                        line = reader.ReadLine();
                        //Helpfunctions.Instance.ErrorLog($"[Network] Read line: {line}");
                    }
            }
            catch (Exception e)
            {
                Helpfunctions.Instance.ErrorLog($"[Network] Read Message exception: {e}");
            }
            return new KeyValuePair<string, string> (header, lines);
        }

        private static byte[] compressStream(Stream input)
        {
            using (MemoryStream compressStream = new MemoryStream())
            using (DeflateStream compressor = new DeflateStream(compressStream, CompressionMode.Compress))
            {
                input.CopyTo(compressor);
                return compressStream.ToArray();
            }
        }

        private static Stream decompressStream(byte[] input)
        {
            MemoryStream output = new MemoryStream();

            using (MemoryStream compressStream = new MemoryStream(input))
            using (DeflateStream decompressor = new DeflateStream(compressStream, CompressionMode.Decompress))
                decompressor.CopyTo(output);

            output.Position = 0;
            return output;
        }
    }
}