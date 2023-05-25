using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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

        //Select from all tables 
        public void SelectTable()
        {

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
