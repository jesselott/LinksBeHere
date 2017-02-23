using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LinksBeHere
{
    /// <summary>
    /// Interaction logic for LinksFound.xaml
    /// </summary>
    public partial class LinksFound : Window
    {
        public LinksFound()
        {
            InitializeComponent();
        }

        private void testerBtn_Click(object sender, RoutedEventArgs e)
        {
            LinkFinder foundLinksLF = new LinkFinder();
            linkList_rtb.Document.Blocks.Add(
                new Paragraph(new Run("Description: " + foundLinksLF.getLinkTitle("https://www.google.com"))));
        }
    }
}
