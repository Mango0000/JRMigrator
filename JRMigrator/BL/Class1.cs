using JRMigrator.beans;
using JRMigrator.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using CUBRID.Data.CUBRIDClient;

namespace JRMigrator.BL
{
    class Class1
    {
        private static MigrateTest mt=new MigrateTest();
        private static DBStringBuilder dbfrom;
        private static OracleSQLConnection os { get; set; } =null;
       private static CubridSQLConnection csql { get; set; } =null;
       public static MSSQLConnection ms { get; set; }=null;
       private static CUBRIDConnection cs;
        public static void start(String ip,String port,String databasename,String username,String password,int type)
        {
            DBType databasetype = DBType.CubridDB;
            dbfrom = new DBStringBuilder(databasetype, ip, port, databasename, username, password);

            //MessageBox.Show(type+"");
            try
            {
            if (type == 1)
            {
               csql = CubridSQLConnection.getConnection();
                csql.connectionString = dbfrom;
                Console.Out.WriteLine(csql.OpenConnection() ? "erfolgreich" : "gescheitert");
                cs = csql.getConn();

            }

            else if (type == 2)
            {
                ms = MSSQLConnection.getConnection();
                ms.connectionString = dbfrom;
                Console.Out.WriteLine(ms.OpenConnection() ? "erfolgreich" : "gescheitert");  
            }
            else
            {
               // os.CloseConnection();
                os=OracleSQLConnection.getConnection();
                os.connectionString = dbfrom;
                Console.Out.WriteLine(os.OpenConnection() ? "erfolgreich" : "gescheitert"); 
               
             
                
            }
            Console.Out.WriteLine(dbfrom.ToString());
            

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
                    MessageBox.Show(e.ToString());
                }
            }

            if (csql != null)
            {
                csql.CloseConnection();
                cs.Close();
            } if (ms!=null)
            {
               ms.CloseConnection(); 
            }
             if (os!=null)
            {
                os.CloseConnection();
            }
             
        }

        public static void migrateOS()
        {
           mt.getOracleTables(os,cs); 
        }

        public static void migrateMS()
        {
            mt.getMSSqlTables(ms,cs);
        }
    }
}
