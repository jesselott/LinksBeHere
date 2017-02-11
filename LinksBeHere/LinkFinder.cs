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
        // fields
        private string linkReadPath = "";
        private string linkWritePath = "";

        // properties
        public string LinkReadPath
        {
            get
            {
                return this.linkReadPath;
            }
            set
            {
                this.linkReadPath = LinkReadPath;
            }
        }

       
        public string LinkWritePath
        {
            get
            {
                return this.linkWritePath;
            }
            set
            {
                this.linkWritePath = LinkWritePath;
            }
        }

        // Method(s)
        public void FindLinks()
        {
            StreamReader textReader = new StreamReader(linkReadPath);
            StreamWriter linkFinder = new StreamWriter(linkWritePath);

            Regex urlFinder = new Regex(@"(ftp | https ?)://[^\s]+", RegexOptions.IgnoreCase);
            MatchCollection links = urlFinder.Matches(textReader.ReadToEnd());
            foreach (Match match in links)
            {
                linkFinder.WriteLine(match);
                Console.WriteLine(match);
            }
        }

        // constructor(s)
        public LinkFinder(string readPath, string writePath)
        {
            this.linkReadPath = readPath;
            this.linkWritePath = writePath;
        }
    }
}
