using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinksBeHere
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region text Changing Depending On Focus
        private void outputLocTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(outputLocTextBox.Text == "c:\\")
            {
                outputLocTextBox.Text = "";
            }
            
        }

        private void outputLocTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (outputLocTextBox.Text == "")
            {
                outputLocTextBox.Text = "c:\\";
            }
        }

        private void fileLocTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (fileLocTextBox.Text == "c:\\")
            {
                fileLocTextBox.Text = "";
            }
        }

        private void fileLocTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (fileLocTextBox.Text == "")
            {
                fileLocTextBox.Text = "c:\\";
            }
        }

        #endregion

        #region hovering over the helper text
        private void helperTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            helperTextBlock.Foreground = new SolidColorBrush(Colors.Blue);
        }

        private void helperTextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            helperTextBlock.Foreground = new SolidColorBrush(Colors.Black);
        }

        #endregion

        private void helperTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("This tool simply finds all of the hyperlinks in a text file and prints the output to a .txt file in the designated output location", "This is neat!", MessageBoxButton.OK);
        }

        private void fileLocatorBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileLocator = new OpenFileDialog();
            fileLocator.Filter = "txt files (*.txt)|*.txt";
            fileLocator.FileName = "";
            if (fileLocator.ShowDialog() == true)
            {
                fileLocTextBox.Text = fileLocator.FileName;
            }
        }

        private void outLocatorBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileOut = new SaveFileDialog();
            fileOut.Title = "Save output location as";
            fileOut.Filter = "txt file (*.txt)|*.txt";
            fileOut.FileName = "";
            if (fileOut.ShowDialog() == true)
            {
                outputLocTextBox.Text = fileOut.FileName;
            }
        }

        private void hyperLinkFinderBtn_Click(object sender, RoutedEventArgs e)
        {
            LinksFound linksFound = new LinksFound();
            linksFound.Owner = this;

            try
            {
                LinkFinder HyperFinder = new LinkFinder(fileLocTextBox.Text, outputLocTextBox.Text);
                HyperFinder.FindLinks();
                
                foreach (var item in HyperFinder.listOfLinks)
                {
                    try
                    {
                        HyperFinder.populateTheRichBoxWithLinks(linksFound, item, HyperFinder);
                    }
                    catch (System.Net.WebException)
                    {
                        linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run("Description unavailable. Access was denied.\n")));
                    }
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Something went wrong. Please try again.", "Oops!", MessageBoxButton.OK);
                resetText();
            }

            // TODO: Make it so this window doesn't 'lock' focus
            openNewWindow(linksFound);
        }

        private void resetTextBtn_Click(object sender, RoutedEventArgs e)
        {
            resetText();
        }

        private void openNewWindow(LinksFound windowToOpen)
        {
            windowToOpen.ShowDialog();
            resetText();
        }

        private void resetText()
        {
            outputLocTextBox.Text = "c:\\";
            fileLocTextBox.Text = "c:\\";
        }

        
    }
}
