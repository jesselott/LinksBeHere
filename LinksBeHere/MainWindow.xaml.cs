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

        private void helperTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            helperTextBlock.Foreground = new SolidColorBrush(Colors.Blue);
        }

        private void helperTextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            helperTextBlock.Foreground = new SolidColorBrush(Colors.Black);
        }

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
            try
            {
                // call the helper class to utilize a streamreader / writer
                LinkFinder HyperFinder = new LinkFinder(fileLocTextBox.Text, outputLocTextBox.Text);
                HyperFinder.FindLinks();

                // instantiate and open the LinksFound window
                LinksFound linksFound = new LinksFound();
                linksFound.Owner = this;
                

                foreach (var item in HyperFinder.listOfLinks)
                {
                    linksFound.linkList_rtb.Document.Blocks.Add(new Paragraph(new Run($"{item}")));
                }

                linksFound.ShowDialog();

                // reset the text fields
                // TODO: put this in a separate button and method
                outputLocTextBox.Text = "c:\\";
                fileLocTextBox.Text = "c:\\";
                

                // reset the fields
                //MessageBox.Show("Links have been written to the specified location.", "Operation(s) complete", MessageBoxButton.OK);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong. Pleae enter a valid file path for both entries.", "Oops!", MessageBoxButton.OK);
                outputLocTextBox.Text = "c:\\";
                fileLocTextBox.Text = "c:\\";
            }           
            
        }
    }
}
