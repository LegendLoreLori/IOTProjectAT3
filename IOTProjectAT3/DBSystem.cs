﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Parameters;

namespace IOTProjectAT3
{
    /// <summary>
    /// Handle all the SQL queries and directly interface with the DB
    /// </summary>
    public class DBSystem
    {
        public const string DbServer = "localhost";
        public const string DbUser = "root";
        public const string DbName = "lmm_ictprg402";
        public const int DbPort = 3306;
        public const string DbPassword = "";
        public static readonly string DbConnectionString = $"server={DbServer}; user={DbUser}; database={DbName}; port={DbPort}; password={DbPassword}";

        //TODO: figure out if I need to do anything with a constructor
        public DBSystem() 
        {
        }

        //Return a list of tables to be displayed and interacted with in the list box 
        public List<string> DisplayTables()
        {
            List<string> tables = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(DbConnectionString))
            {
                string query = "SHOW TABLES";
                try
                {
                    connection.Open();
                    using MySqlCommand command = new MySqlCommand(query, connection);
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex) 
                { 
                    MessageBox.Show(ex.Message);
                }
            }
            return tables;
        }

        //Return the records of a selected table
        public List<string> GetRecords(string tableName)
        {
            if (!DisplayTables().Contains(tableName)) return new List<string>() {"Invalid Table Name"};

            List<string> records = new List<string>();
            using(MySqlConnection connection = new MySqlConnection(DbConnectionString))
            {
                string query = $"SELECT * FROM `{tableName}`";
                using(MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            StringBuilder recordBuilder = new StringBuilder();
                            //dynamically builds a string to the correct size
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                recordBuilder.Append(reader[i]);
                                recordBuilder.Append("   ");
                            }
                            records.Add(recordBuilder.ToString().Trim());
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return records;
        }

        public List<string> GetSchema(string tableName)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnectionString))
            {
                connection.Open();
                // Retrieve the table schema
                DataTable schemaTable = connection.GetSchema("Columns", new[] { "", "", tableName, "" });
                List<string> fieldNames = new List<string>();
                foreach (DataRow row in schemaTable.Rows)
                {
                    string columnName = (string)row["COLUMN_NAME"];
                    fieldNames.Add(columnName);
                }
                return fieldNames;
            }
        }

        //Perform like query on table 
        public void SearchTable(string tableName, string searchText)
        {
            List<string> records = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(DbConnectionString))
            {
                string query = $"SELECT * FROM `{tableName}` WHERE {tableName[1]} LIKE %{searchText}%";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                try
                {
                    connection.Open();
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        StringBuilder recordBuilder = new StringBuilder();
                        //dynamically builds a string to the correct size
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            recordBuilder.Append(reader[i]);
                            recordBuilder.Append("   ");
                        }
                        records.Add(recordBuilder.ToString().Trim());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Perform update query on record
        public void EditRecord()
        {

        }
        
        //Perform Join query on table
        public void JoinTable()
        {

        }

        //Insert new record into table
        public void InsertRecord()
        {

        }

        //Delete currently selected record
        public void DeleteRecord() 
        {

        }

    }
}
