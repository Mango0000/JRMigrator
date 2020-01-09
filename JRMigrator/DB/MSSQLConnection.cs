using JRMigrator.beans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace JRMigrator.DB
{
    class MSSQLConnection
    {
        private static MSSQLConnection theInstance = null;
        public DBStringBuilder connectionString { get; set; } = null;
        private SqlConnection conn = null;
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
