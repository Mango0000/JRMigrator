using JRMigrator.beans;
using JRMigrator.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace JRMigrator.BL
{
    class Class1
    {
        private static DBStringBuilder dbfrom;
        public static void start(String ip,String port,String databasename,String username,String password)
        {
            DBType databasetype = DBType.CubridDB;
            dbfrom = new DBStringBuilder(databasetype, ip, port, databasename, username, password);
            OracleSQLConnection csql = OracleSQLConnection.getCubridConnection();
            csql.connectionString = dbfrom;

            Console.Out.WriteLine(dbfrom.ToString());
            try
            {
                Console.Out.WriteLine(csql.OpenConnection() ? "erfolgreich" : "gescheitert");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("USER") || e.Message.Contains("user"))
                {
                    MessageBox.Show("Invalid Username");
                }
                if (e.Message.Contains("PASSWORD") || e.Message.Contains("password"))
                {
                    MessageBox.Show("Invalid password");
                }
                else
                {
                    MessageBox.Show("Invalid Port or Address");
                }
            }
            csql.CloseConnection();
        }
    }
}
