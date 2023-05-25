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


namespace IOTProjectAT3
{
    /// <summary>
    /// Accessess functions of DBSystem to drive the main window
    /// </summary>
    public partial class MainWindow : Window
    {
        DBSystem dataBaseSystem = new DBSystem();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectTableButton_Click(object sender, RoutedEventArgs e)
        {
            IOTListBox.ItemsSource = dataBaseSystem.DisplayTables();
        }
    }
}
