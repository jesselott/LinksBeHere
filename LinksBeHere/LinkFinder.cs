using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;

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


        // list to store the matches
        public List<string> listOfLinks = new List<string>();

        // Method(s)
        public void FindLinks()
        {
            StreamReader textReader = new StreamReader(linkReadPath);
            StreamWriter linkFinder = new StreamWriter(linkWritePath);
            

            Regex urlFinder = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase);
            MatchCollection links = urlFinder.Matches(textReader.ReadToEnd());
            foreach (Match match in links)
            {
                listOfLinks.Add(match.ToString());
                linkFinder.WriteLine(match.ToString(), linkWritePath);
                linkFinder.Flush();
            }
            
        }
        
        // go get images and titles
        public string getLinkTitle(string input)
        {
            WebClient retriever = new WebClient();
            string source = retriever.DownloadString(input);
            string title = Regex.Match(source,
                @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;
            retriever.Dispose();
            return title;
        }

        public string getLinkDescription(string input)
        {
            WebClient retriever = new WebClient();
            string source = retriever.DownloadString(input);
            string description = "";

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(source);
            HtmlNode descriptNode =  doc.DocumentNode.SelectSingleNode("//meta[@name='description']");
            if (descriptNode != null)
            {
                HtmlAttribute desc = descriptNode.Attributes["content"];
                description = desc.Value;
            }

            return description;
        }

        // constructor(s)
        public LinkFinder(string readPath, string writePath)
        {
            this.linkReadPath = readPath;
            this.linkWritePath = writePath;
        }

        public LinkFinder()
        {

        }
    }
}
