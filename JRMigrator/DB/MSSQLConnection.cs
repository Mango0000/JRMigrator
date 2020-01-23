using JRMigrator.beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace JRMigrator.DB
{
    public class MSSQLConnection
    {
        private static MSSQLConnection theInstance = null;
        public DBStringBuilder connectionString { get; set; } = null;
        private SqlConnection conn = null;
        private String sqlGetTables = "SELECT TABLE_NAME "+"FROM INFORMATION_SCHEMA.TABLES";
        SqlCommand sqlcommand;

        public static MSSQLConnection getConnection()
        {
            if (theInstance == null)
            {
                theInstance = new MSSQLConnection();
            }
            return theInstance;
        }

        public Boolean OpenConnection()
        {
            if (connectionString != null)
            {
                conn = new SqlConnection(connectionString.getMSConnectionString());
                conn.Open();
                return true;
            }
            return false;
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public List<String> getTables()
        {
            sqlcommand = new SqlCommand(sqlGetTables, conn);
            SqlDataReader reader = sqlcommand.ExecuteReader();
            List<String> tables = new List<String>();
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }
            return tables;
        }

        private static void test()
        {
            string ip="192.168.0.1";
            string connectionString;
            connectionString = "Data Source="+ip+",1433;Initial Catalog=testdatabase;User ID=SA;Password=Manuelh0";
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            Console.Out.WriteLine("Microsoft SQL Connection");
            cnn.Close();
        }
        
    }
}
