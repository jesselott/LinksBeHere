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
            MessageBox.Show("This tool simply finds all of the hyperlinks in a text file and prints the output as .txt in the designated output location", "This is neat!", MessageBoxButton.OK);
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
            LinkFinder HyperFinder = new LinkFinder(fileLocTextBox.Text, outputLocTextBox.Text);
            HyperFinder.FindLinks();
            
        }
    }
}
