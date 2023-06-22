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

        //display the tables inside the database
        private void ShowTablesButton_Click(object sender, RoutedEventArgs e)
        {
            IOTListBox.ItemsSource = null;
            IOTListBox.ItemsSource = dataBaseSystem.DisplayTables();
        }

        //populate list box with all records inside a table
        private void IOTListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IOTListBox.SelectedItem == null) return;
            IOTListBox.ItemsSource = dataBaseSystem.GetRecords(IOTListBox.SelectedItem.ToString());
        }

        private void SearchRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextbox.Text == null || SearchTextbox.Text == "") return;
        }
    }
}
