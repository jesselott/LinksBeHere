using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LinksBeHere;

namespace LinksBeHere.Test
{
    [TestFixture]
    class LinksBeHereTest
    {
        [TestCase]
		public static void TestCheckBoxVisibility()
        {
            MainWindow newWindow = new MainWindow();
            newWindow.output_cb_Checked(newWindow.output_cb, new System.Windows.RoutedEventArgs());
            Assert.AreEqual(true, newWindow.outputLocTextBox.IsVisible);
		}
    }
}
