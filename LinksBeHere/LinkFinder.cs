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
            using (StreamReader textReader = new StreamReader(linkReadPath))
            {
                using (StreamWriter linkFinder = new StreamWriter(linkWritePath))
                {
                    Regex urlFinder = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase);
                    MatchCollection links = urlFinder.Matches(textReader.ReadToEnd());
                    foreach (Match match in links)
                    {
                        listOfLinks.Add(match.ToString());
                        linkFinder.WriteLine(match.ToString(), linkWritePath);
                        linkFinder.Flush();
                    }
                }
            }
        }

        // TODO: get html text for getLinkTitle and getLinkDescription
        
        public string getLinkTitle(string input)
        {
            using (WebClient retriever = new WebClient())
            {
                retriever.Proxy = null;
                string Title = "";
                HtmlDocument doc = new HtmlDocument();
                string source = retriever.DownloadString(input);
                doc.LoadHtml(source);
                HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//head/title");
                if (titleNode != null)
                {
                    Title = titleNode.InnerText;
                }
                return Title;
            }            
        }
        
        public string getLinkDescription(string input)
        {
            using (WebClient retriever = new WebClient())
            {

                retriever.Proxy = null;
                string description = "";
                HtmlDocument doc = new HtmlDocument();
                string source = retriever.DownloadString(input);
                doc.LoadHtml(source);
                HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//meta[@name='description']");
                if (descriptionNode != null)
                {
                    HtmlAttribute descriptionNodeText = descriptionNode.Attributes["content"];
                    description = descriptionNodeText.Value;
                }
                return description;
            }
        }
       
        public void populateTheRichBoxWithLinks(LinksFound linksFound, string inputItem, LinkFinder HyperFinder)
        {
            linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run($"{inputItem}")));
            string linkTitle = HyperFinder.getLinkTitle($"{inputItem}");
            string linkDescription = HyperFinder.getLinkDescription($"{inputItem}");
            if (linkTitle != "")
            {
                linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run("URL Title: " + linkTitle)));
            }
            if (linkDescription != "")
            {
                linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run("URL Description: " + linkDescription)));
            }
            linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run("\n")));
            removeLinkFromList(inputItem);
        }

        internal void removeLinkFromList(string input)
        {
            listOfLinks.Remove(input);
        }
        #endregion

        #region constructors
        public LinkFinder(string readPath, string writePath)
        {
            this.linkReadPath = readPath;
            this.linkWritePath = writePath;
        }

        public LinkFinder()
        {

        }
        #endregion
    }
}
