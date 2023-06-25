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
        string all = "*";

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
            if (IOTListBox.SelectedItem == null || IOTListBox.SelectedItem.ToString() == "") return;
            //validates if selected item is a table name
            if (!dataBaseSystem.DisplayTables().Contains(IOTListBox.SelectedItem.ToString())) return;

            TableNameBlock.Text = IOTListBox.SelectedItem.ToString();
            FieldNamesCombo.ItemsSource = dataBaseSystem.GetSchema(IOTListBox.SelectedItem.ToString());
            IOTListBox.ItemsSource = dataBaseSystem.GetRecords(all, IOTListBox.SelectedItem.ToString());
        }

        //searched records with like query and populates list box with result
        private void SearchRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableNameBlock.Text == "Table Name:" ||  TableNameBlock.Text == null) return;
            if (FieldNamesCombo.Text == null || FieldNamesCombo.Text == "") FieldNamesCombo.Text = all;

            IOTListBox.ItemsSource = dataBaseSystem.SearchTable(TableNameBlock.Text, FieldNamesCombo.Text, SearchTextBox.Text);
            SearchTextBox.Text = "";
        }

        private void BranchRecordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (BranchesCombo.SelectedItem == null || BranchesCombo.SelectedItem == "") BranchesCombo.SelectedItem = all;
            if (TableNameBlock.Text == null || TableNameBlock.Text != "employees") return;
            //TODO: implement getrecords function

        }

        private void BranchesCombo_Initialized(object sender, EventArgs e)
        {
            BranchesCombo.ItemsSource = dataBaseSystem.GetBranches();
        }
    }
}
