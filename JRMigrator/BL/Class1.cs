using JRMigrator.beans;
using JRMigrator.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace JRMigrator.BL
{
    class Class1
    {
        private static DBStringBuilder dbfrom;
        public static void start()
        {
            String ip = "10.151.62.24";
            String port = "33000";
            String databasename = "demodb";
            String username = "public";
            String password = "";
            DBType databasetype = DBType.CubridDB;
            dbfrom = new DBStringBuilder(databasetype, ip, port, databasename, username, password);
            CubridSQLConnection csql = CubridSQLConnection.getCubridConnection();
            csql.connectionString = dbfrom;

            Console.Out.WriteLine(dbfrom.ToString());
            Console.Out.WriteLine(csql.OpenConnection()?"erfolgreich":"gescheitert");
        }
    }
}
