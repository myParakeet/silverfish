using System;
using System.Collections.Generic;

namespace HREngine.Bots
{

    public class Discovery
    {
        class discoveryitem
        {
            public CardDB.cardIDEnum cardid = CardDB.cardIDEnum.None;
            public int bonus = 0;
            public string ownclass = "";
            public string enemyclass = "";

            public discoveryitem(string line)
            {
                this.cardid = CardDB.Instance.cardIdstringToEnum(line.Split(',')[0]);
                this.bonus = Convert.ToInt32(line.Split(';')[0].Split(',')[1]);
                this.ownclass = line.Split(';')[1];
                this.enemyclass = line.Split(';')[2];
            }

        }

        private List<discoveryitem> discoverylist = new List<discoveryitem>();

        private static Discovery instance;

        public static Discovery Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Discovery();
                }
                return instance;
            }
        }

        private Discovery()
        {
            readCombos();
        }

        private void readCombos()
        {
            string[] lines = new string[0] { };
            this.discoverylist.Clear();
            try
            {
                string path = Settings.Instance.path;
                lines = System.IO.File.ReadAllLines(path + "_discovery.txt");
            }
            catch
            {
                Helpfunctions.Instance.ErrorLog("cant find _discovery.txt (if you didn't create your own discovery file, ignore this message)");
                return;
            }
            Helpfunctions.Instance.ErrorLog("read _discovery.txt...");
            foreach (string line in lines)
            {
                string shortline = line.Replace(" ", "");
                if (shortline.StartsWith("//")) continue;
                if (shortline.Length == 0) continue;

                try
                {
                    discoveryitem d = new discoveryitem(line);
                    this.discoverylist.Add(d);
                }
                catch
                {
                    Helpfunctions.Instance.ErrorLog("discoverymaker cant read: " + line);
                }
            }
            Helpfunctions.Instance.ErrorLog(discoverylist.Count + " discovery rules found");
        }

        public int getBonusValue(CardDB.cardIDEnum cardid, string ownclass, string enemyclass)
        {
            int bonus = 0;
            foreach (discoveryitem di in this.discoverylist)
            {
                if (di.cardid == cardid && (di.ownclass == "all" || di.ownclass == ownclass) && (di.enemyclass == "all" || di.enemyclass == enemyclass))
                {
                    if (di.bonus > bonus) bonus = di.bonus;
                }
            }

            return bonus;
        }

        public int getChoice(Playfield p)
        {
            int i = 0;
            int choice = 0;
            int prevbonus = 0;
            foreach (Handmanager.Handcard hc in Handmanager.Instance.handcardchoices)
            {
                CardDB.Card c = hc.card;
                i++;

                int bonus = getBonusValue(c.cardIDenum, Hrtprozis.heroEnumtoName(p.ownHeroName), Hrtprozis.heroEnumtoName(p.enemyHeroName));
                if (bonus > prevbonus)
                {
                    choice = i;
                    prevbonus = bonus;
                }
            }

            return choice;
        }
    }
}