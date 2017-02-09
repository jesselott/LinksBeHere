using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinksBeHere
{
    class LinkFinder
    {
        public static void findLinks()
        {
            string chatLogPath = @"C:\Users\wwstudent\Desktop\skypeChatHistory_8Feb17.txt";

            StreamReader chatLogSR = new StreamReader(chatLogPath);
            StreamWriter linkFinder = new StreamWriter("C:\\Users\\wwstudent\\Desktop\\skypeLinks.rtf");
            Regex urlFinder = new Regex(@"(ftp | https ?)://[^\s]+", RegexOptions.IgnoreCase);
            MatchCollection links = urlFinder.Matches(chatLogSR.ReadToEnd());
            foreach (Match match in links)
            {
                Console.WriteLine(match);
                linkFinder.WriteLine(match);
            }
        }
    }
}
