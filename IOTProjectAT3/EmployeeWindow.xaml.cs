﻿using MySql.Data.MySqlClient;
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
        public EmployeeWindow()
        {
            InitializeComponent();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            if (IdText.Text == "" || GivenNameText.Text == "" || FamilyNameText.Text == "" || DoBText.Text == "" || GenderText.Text == "" || SalaryText.Text == "" || SupervisorIDText.Text == "" || BranchIDText.Text == "") 
            {
                MessageBox.Show("Incorrect information.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(DBSystem.DbConnectionString))
            {
                string query = $"INSERT INTO `employees`(`id`, `given_name`, `family_name`, `date_of_birth`, `gender_identity`, `gross_salary`, `supervisor_id`, `branch_id`) VALUES " +
                    $"('{IdText.Text}','{GivenNameText.Text}','{FamilyNameText.Text}','{DoBText.Text}','{GenderText.Text}','{SalaryText.Text}','{SupervisorIDText.Text}','{BranchIDText.Text}');";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Success");
                    }
                    catch (Exception ex) 
                    { 
                        MessageBox.Show(ex.Message); 
                    }
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}