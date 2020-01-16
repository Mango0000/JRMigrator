extern alias CUBRID;
using CUBRID::CUBRID.Data.CUBRIDClient;
using JRMigrator.beans;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace JRMigrator.DB
{
    class CubridSQLConnection
    {
        private static CubridSQLConnection theInstance = null;
        public DBStringBuilder connectionString { get; set; } = null;
        private CUBRIDConnection conn = null;

        public static CubridSQLConnection getConnection()
        {
            if (theInstance == null)
            {
                theInstance = new CubridSQLConnection();
            }
            return theInstance;
        }

        public Boolean OpenConnection()
        {
            if (connectionString != null)
            {
                conn = new CUBRIDConnection(connectionString.getCubridConnectionString());
                conn.SetConnectionTimeout(3);
                conn.Open();
                return true;
            }
            return false;
        }

        public void CloseConnection()
        {
            conn.Close();
        }
        
        private static void test()
        {
            String ip = "192.168.0.1";
            String ConnectionString = "server=" + ip + ";database=demodb;port=33000;user=public;password=";
            using (CUBRIDConnection conn = new CUBRIDConnection(ConnectionString))
            {
                conn.Open();
                String sql = "SELECT * FROM record;";
                using (CUBRIDCommand cmd = new CUBRIDCommand(sql, conn))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Object[] values = new Object[reader.FieldCount];
                            int fieldCount = reader.GetValues(values);
                            foreach (Object obj in values)
                            {
                                Console.Out.Write(obj.ToString() + "," + obj.GetType() + "|");
                            }
                            Console.Out.WriteLine();

                            /*
                                EXPLAIN athlete;                    //same as Describe, basic information of table
                                SHOW TABLES;                        //get all table names
                                SHOW FULL COLUMNS FROM athlete;     //same as Describe but with comments
                                SHOW INDEXES FROM athlete;          //get table information like fk
                                SHOW CREATE TABLE athlete;          //get query string of creating the table
                             */
                        }
                        //(read the values using: reader.Get...() methods)
                    }
                }

                Console.Out.WriteLine("CUBRID SQL Connection");
                conn.Close();
            }
            /*using (CUBRIDConnection conn = new CUBRIDConnection(sb.GetConnectionString()))
            {
                conn.Open();
                String sql = "SELECT * FROM athlete;";
                using (CUBRIDCommand cmd = new CUBRIDCommand(sql, conn))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        Console.Out.WriteLine(reader.GetString(3));
                        //(read the values using: reader.Get...() methods)
                    }
                }
            }*/
        }
    }
}
