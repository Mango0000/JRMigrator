using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace JRMigrator.DB
{
    class MSSQLConnection
    {
        private static void test()
        {
            string ip="192.168.0.1";
            string connectionString;
            SqlConnection conn = new SqlConnection();
            SqlConnection cnn;
            connectionString = "Data Source="+ip+",1433;Initial Catalog=testdatabase;User ID=SA;Password=Manuelh0";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            Console.Out.WriteLine("Microsoft SQL Connection");
            cnn.Close();
        }
        
    }
}
