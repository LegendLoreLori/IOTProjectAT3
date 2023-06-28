using MySql.Data.MySqlClient;
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

namespace IOTProjectAT3
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        DBSystem dataBaseSystem;
        public EmployeeWindow(DBSystem dataBaseSystem, List<string>? data)
        {
            InitializeComponent();
            this.dataBaseSystem = dataBaseSystem;
            InsertButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Hidden;

            if (data != null)
            {
                InsertButton.Visibility = Visibility.Hidden;
                UpdateButton.Visibility = Visibility.Visible;
                IdText.Text = data[0];
                GivenNameText.Text = data[1];
                FamilyNameText.Text = data[2];
                DoBText.Text = data[3].Substring(0, 9);
                GenderText.Text = data[4];
                SalaryText.Text = data[5];
                SupervisorIDText.Text = data[6];
                BranchIDText.Text = data[7];
            }
        }


        //sends input data to dbsystem to perform select query
        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            if (IdText.Text == "" || GivenNameText.Text == "" || FamilyNameText.Text == "" || DoBText.Text == "" || GenderText.Text == "" || SalaryText.Text == "" || SupervisorIDText.Text == "" || BranchIDText.Text == "") 
            {
                MessageBox.Show("Incorrect information.");
                return;
            }

            List<string> inputData = new() { IdText.Text, GivenNameText.Text, FamilyNameText.Text, DoBText.Text, GenderText.Text, SalaryText.Text, SupervisorIDText.Text, BranchIDText.Text };
            dataBaseSystem.InsertRecord(inputData);
        }

        //sends input data to dbsystem to perform update query
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IdText.Text == "" || GivenNameText.Text == "" || FamilyNameText.Text == "" || DoBText.Text == "" || GenderText.Text == "" || SalaryText.Text == "" || SupervisorIDText.Text == "" || BranchIDText.Text == "")
            {
                MessageBox.Show("Incorrect information.");
                return;
            }

            List<string> inputData = new() { IdText.Text, GivenNameText.Text, FamilyNameText.Text, DoBText.Text, GenderText.Text, SalaryText.Text, SupervisorIDText.Text, BranchIDText.Text };
            dataBaseSystem.EditRecord(inputData);
        }
    }
}
