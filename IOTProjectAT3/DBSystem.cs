using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
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
        public List<string> GetRecords(string fieldName, string tableName, string condition="1=1")
        {
            if (!DisplayTables().Contains(tableName)) return new List<string>() {"Invalid Table Name"};

            List<string> records = new List<string>();
            using(MySqlConnection connection = new MySqlConnection(DbConnectionString))
            {
                string query = $"SELECT {fieldName} FROM `{tableName}` WHERE {condition}";
                using(MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            records.Add(BuildList(reader));
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

        //Perform like query on table 
        public List<string> SearchTable(string tableName, string fieldName, string searchText)
        {
            List<string> records = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(DbConnectionString))
            {
                //this is a bad implementation, assumes the name column will always be in index 1
                string query = $"SELECT {fieldName} FROM `{tableName}` WHERE `{GetSchema(tableName)[2]}` LIKE '%{searchText}%';";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                try
                {
                    connection.Open();
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        records.Add(BuildList(reader));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return records;
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

        //Return the unique branches in employees table
        public List<string> GetBranches()
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnectionString))
            {
                List<string> branches = new List<string>() { "*" };

                string query = $"SELECT branch_id FROM `employees` GROUP BY branch_id;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) 
                    {
                        branches.Add(BuildList(reader));
                    }
                }
                return branches;
            }
        }

        //Return a list of the field names inside a table
        public List<string> GetSchema(string tableName)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnectionString))
            {
                connection.Open();
                // Retrieve the table schema
                DataTable schemaTable = connection.GetSchema("Columns", new[] { "", "", tableName, "" });
                List<string> fieldNames = new List<string>() { "*" };
                foreach (DataRow row in schemaTable.Rows)
                {
                    string columnName = (string)row["COLUMN_NAME"];
                    fieldNames.Add(columnName);
                }
                return fieldNames;
            }
        }

        private string BuildList(MySqlDataReader reader)
        {
            StringBuilder recordBuilder = new StringBuilder();
            //Dynamically builds a string to the correct size
            for (int i = 0; i < reader.FieldCount; i++)
            {
                recordBuilder.Append(reader[i]);
                recordBuilder.Append("   ");
            }
            string record = recordBuilder.ToString().Trim();
            return record;
        }
    }
}
