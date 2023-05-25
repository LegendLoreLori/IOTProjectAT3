using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;



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
        public const string DbPassword = "password";
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
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            { 
                                tables.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                catch (Exception ex) 
                { 
                    MessageBox.Show(ex.Message);
                }
            }
            return tables;
        }

        //Populate list box automatically
        public void FillList()
        {
        }

        //Perform like query on table 
        public void SearchList()
        {

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
