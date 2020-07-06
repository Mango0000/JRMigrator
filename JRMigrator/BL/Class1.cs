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
        public String erfolgreich{ get; set; } =null;
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
                    MessageBox.Show(csql.OpenConnection() ? "erfolgreich" : "gescheitert");
                cs = csql.getConn();

            }

            else if (type == 2)
            {
                ms = MSSQLConnection.getConnection();
                ms.connectionString = dbfrom;
                    MessageBox.Show(ms.OpenConnection() ? "erfolgreich" : "gescheitert");  
            }
            else
            {
               // os.CloseConnection();
                os=OracleSQLConnection.getConnection();
                os.connectionString = dbfrom;
                    MessageBox.Show(os.OpenConnection() ? "erfolgreich" : "gescheitert"); 
               
             
                
            }
            Console.Out.WriteLine(dbfrom.ToString());
            

            }
            catch (Exception e)
            {
                if (e.Message.Contains("Failed to connect to"))
                {
                    MessageBox.Show("Invalid Databasename");
                }
              else  if (e.Message.Contains("Cannot connect to"))
                {
                    MessageBox.Show("Invalid port or address");
                }
               else if (e.Message.Contains("password"))
                {
                    MessageBox.Show("Invalid password");
                }
              else  if (e.Message.Contains("User"))
                {
                    MessageBox.Show("Invalid username");
                }

                else
                {
                    MessageBox.Show("An unexpected error occured");
                   // MessageBox.Show(e.ToString());
                }

             close();
            }

          
             
        }

        public static void closeConnection()
        {
            try
            {
                if (ms != null)
                {
                    ms.CloseConnection();
                    ms = null;
                }
                else if (os != null)
                {
                    os.CloseConnection();
                    os = null;
                }
                else if (csql != null)
                {
                    csql.CloseConnection();
                    csql = null;
                }
            }catch(Exception e)
            {

            }
        }

        public String getErfolgreich()
        {
           return mt.getErfolgreich();
        }

        public void migrate()
        {
            mt.migrateTables(os,ms,cs);
            os = null;
            ms = null;
            cs = null;
        }

        public static void close()
        {
            os = null;
            ms = null;
            cs = null;
        }

    }
}
