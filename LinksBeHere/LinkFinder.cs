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
using System.Windows.Documents;

namespace LinksBeHere
{
    class LinkFinder
    {
        #region fields
        private string linkReadPath = "";
        private string linkWritePath = "";
        public List<string> listOfLinks = new List<string>();
        #endregion

        #region properties
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
        #endregion

        #region methods
        public void FindLinks()
        {
            // TODO: make writing link to txt file optional
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
        
        public string getLinkTitle(string input)
        {
            using (WebClient retriever = new WebClient())
            {
                string source = retriever.DownloadString(input);
                string title = Regex.Match(source,
                    @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                    RegexOptions.IgnoreCase).Groups["Title"].Value;
                return title;
            }            
        }

        public string getLinkDescription(string input)
        {
            string description = "";
            HtmlDocument doc = new HtmlDocument();
            HtmlNode descriptionNode =  doc.DocumentNode.SelectSingleNode("//meta[@name='description']");
            if (descriptionNode != null)
            {
                HtmlAttribute descriptionNodeText = descriptionNode.Attributes["content"];
                description = descriptionNodeText.Value;
            }
            return description;
        }

        public void populateTheRichBoxWithLinks(LinksFound linksFound, string inputItem, LinkFinder HyperFinder)
        {
            linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run($"{inputItem}")));
            string newDescription = HyperFinder.getLinkDescription($"{inputItem}");
            if (newDescription != "")
            {
                linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run("URL Description: " + newDescription + "\n")));
            }
            else
            {
                linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run("Description not found")));
                string newTitle = HyperFinder.getLinkTitle($"{inputItem}");

                if (newTitle != "")
                {
                    linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run("URL Title: " + newTitle)));
                }
                linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run("\n")));
            }
        }
        #endregion

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
