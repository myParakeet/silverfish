using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace carddb_cleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText("_carddb.txt");
            
            text = text.Replace("&lt;b&gt;", "");
            text = text.Replace("&lt;/b&gt;", "");

            text = text.Replace("&lt;i&gt;", "");
            text = text.Replace("&lt;/i&gt;", "");

            text = text.Replace("[x]", "");

            text = text.Replace("’", "'");
            text = text.Replace("&amp;", "&");
            text = text.Replace("“", "\"");
            text = text.Replace("”", "\"");

            string pattern = "<Tag([^<>]*?)type=\"String\">([^<>]*?)\n\n([^<>]*?)</Tag>\r\n";
            string replaceWith = "<Tag$1type=\"String\">$2 $3</Tag>\r\n";
            text = Regex.Replace(text, pattern, replaceWith);

            pattern = "<Tag([^<>]*?)type=\"String\">([^<>]*?)\n([^<>]*?)\n([^<>]*?)\n([^<>]*?)\n([^<>]*?)</Tag>\r\n";
            replaceWith = "<Tag$1type=\"String\">$2 $3 $4 $5 $6</Tag>\r\n";
            text = Regex.Replace(text, pattern, replaceWith);

            pattern = "<Tag([^<>]*?)type=\"String\">([^<>]*?)\n([^<>]*?)\n([^<>]*?)\n([^<>]*?)</Tag>\r\n";
            replaceWith = "<Tag$1type=\"String\">$2 $3 $4 $5</Tag>\r\n";
            text = Regex.Replace(text, pattern, replaceWith);

            pattern = "<Tag([^<>]*?)type=\"String\">([^<>]*?)\n([^<>]*?)\n([^<>]*?)</Tag>\r\n";
            replaceWith = "<Tag$1type=\"String\">$2 $3 $4</Tag>\r\n";
            text = Regex.Replace(text, pattern, replaceWith);

            pattern = "<Tag([^<>]*?)type=\"String\">([^<>]*?)\n([^<>]*?)</Tag>\r\n";
            replaceWith = "<Tag$1type=\"String\">$2 $3</Tag>\r\n";
            text = Regex.Replace(text, pattern, replaceWith);
            

            File.WriteAllText("_carddb.txt", text);
        }
    }
}
