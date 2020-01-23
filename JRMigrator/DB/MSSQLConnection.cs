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
            reader.Close();
            return tables;
        }

        public List<TableInfo> getInfo(String tablename)
        {
            String sqlstring = "SELECT f.COLUMN_NAME, f.IS_NULLABLE, f.DATA_TYPE, r.CONSTRAINT_NAME, r.CONSTRAINT_TYPE FROM INFORMATION_SCHEMA.COLUMNS f LEFT OUTER JOIN(SELECT c.COLUMN_NAME, c.CONSTRAINT_NAME, t.CONSTRAINT_TYPE FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE c INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS t ON  c.CONSTRAINT_NAME = t.CONSTRAINT_NAME WHERE c.TABLE_NAME = '"+ tablename+"') r ON f.COLUMN_NAME = r.COLUMN_NAME WHERE f.TABLE_NAME = '"+ tablename+"'";
            sqlcommand = new SqlCommand(sqlstring, conn);
            SqlDataReader reader = sqlcommand.ExecuteReader();
            List<TableInfo> tbinf = new List<TableInfo>();
            String column_name;
            Boolean is_nullable;
            String data_type;
            String primary_key_name;
            DataType datatype;
            while (reader.Read())
            {
                column_name = reader.GetString(0);
                is_nullable = reader.GetString(1)=="YES"?true:false;
                data_type = reader.GetString(2);
                try
                {
                    primary_key_name = reader.GetString(3);
                }
                catch (System.Data.SqlTypes.SqlNullValueException)
                {
                    primary_key_name = null;
                }
                datatype = getDType(data_type);
                tbinf.Add(new TableInfo(column_name, is_nullable, datatype, primary_key_name));
            }
            reader.Close();
            return tbinf;
        }

        private DataType getDType(String data)
        {
            if (data == null)
            {
                return DataType.NULL;
            }
            else if (data.Equals("varchar"))
            {
                return DataType.VARCHAR;
            }else if (data.Equals("int"))
            {
                return DataType.NUMBER;
            }else if (data.Equals("char"))
            {
                return DataType.CHAR;
            }
            else if (data.Equals("datetime"))
            {
                return DataType.DATETIME;
            }else if (data.Equals("numeric"))
            {
                return DataType.NUMBER;
            }
            return DataType.NULL;
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
