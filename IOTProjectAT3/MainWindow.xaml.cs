﻿using System;
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
        DBSystem dataBaseSystem = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        //display the tables inside the database
        private void ShowTablesButton_Click(object sender, RoutedEventArgs e)
        {
            TableNameBlock.Text = "Table Name:";
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
            IOTListBox.ItemsSource = dataBaseSystem.GetRecords("*", IOTListBox.SelectedItem.ToString());
        }

        //searched records with like query and populates list box with result
        private void SearchRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableNameBlock.Text == "Table Name:" ||  TableNameBlock.Text == null) return;
            if (FieldNamesCombo.Text == null || FieldNamesCombo.Text == "") FieldNamesCombo.Text = "*";

            IOTListBox.ItemsSource = dataBaseSystem.SearchTable(TableNameBlock.Text, FieldNamesCombo.Text, SearchTextBox.Text);
            SearchTextBox.Text = "";
        }

        //populates listbox with employees filtered by branch
        private void BranchRecordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (BranchesCombo.SelectedItem == null || BranchesCombo.SelectedItem == "") BranchesCombo.SelectedItem = "*";
            if (TableNameBlock.Text == null || TableNameBlock.Text != "employees") return;
            
            List<string> records = new();
            if (BranchesCombo.SelectedItem == "*") records = dataBaseSystem.GetRecords("*", TableNameBlock.Text);
            else records = dataBaseSystem.GetRecords("*", TableNameBlock.Text, $"branch_id = {BranchesCombo.SelectedItem}");
            IOTListBox.ItemsSource = records;
        }

        //populates branch combobox with branch numbers, man it's so over engineered
        private void BranchesCombo_Initialized(object sender, EventArgs e)
        {
            BranchesCombo.ItemsSource = dataBaseSystem.GetBranches();
        }

        //populates list with employees whose salary are higher than a threshold
        private void SalaryRecordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableNameBlock.Text == null || TableNameBlock.Text != "employees") return;
            if (SalaryTextBox.Text == null || SalaryTextBox.Text == "") return;
            if (!int.TryParse(SalaryTextBox.Text, out int value)) return;

            IOTListBox.ItemsSource = dataBaseSystem.GetRecords("*", TableNameBlock.Text, $"gross_salary > {SalaryTextBox.Text}");
        }


        //calls dbsystem to perform an insert query with data provided by employeewindow
        private void InsertRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableNameBlock.Text != "employees") return;

            EmployeeWindow insertWindow = new(dataBaseSystem, null);
            insertWindow.ShowDialog();
            IOTListBox.ItemsSource = dataBaseSystem.GetRecords("*", TableNameBlock.Text);
        }

        //calls dbsystem to perform an update query with data provided by employeewindow
        private void EditRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableNameBlock.Text != "employees" || IOTListBox.SelectedItem == null || (string)IOTListBox.SelectedItem == "")
            {
                MessageBox.Show("no item");
                return;
            }
            List<string> employeeData = IOTListBox.SelectedItem.ToString().Split(new string[] { "   " }, StringSplitOptions.RemoveEmptyEntries).ToList(); //creates a list of strings
            EmployeeWindow editWindow = new(dataBaseSystem, employeeData);
            editWindow.ShowDialog();
            IOTListBox.ItemsSource = dataBaseSystem.GetRecords("*", TableNameBlock.Text);
        }

        //calls dbsystem to delete employee record
        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableNameBlock.Text != "employees" || IOTListBox.SelectedItem == null || (string)IOTListBox.SelectedItem == "")
            {
                MessageBox.Show("no item");
                return;
            }

            string employeeID = IOTListBox.SelectedItem.ToString().Substring(0, 3);
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;
            dataBaseSystem.DeleteRecord(employeeID);
            IOTListBox.ItemsSource = dataBaseSystem.GetRecords("*", TableNameBlock.Text);
        }

        //queries the database to return records of the name of an employee and their total sales to each client and displays the results
        private void EmployeeSalesButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "" || !int.TryParse(SearchTextBox.Text, out int result)) 
            {
                MessageBox.Show("Enter employee ID");
                return;
            }

            IOTListBox.ItemsSource = dataBaseSystem.GetSales(SearchTextBox.Text);
            SearchTextBox.Clear();
        }
    }
}
