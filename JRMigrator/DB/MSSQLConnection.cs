using JRMigrator.beans;
using System;
using System.Collections.Generic;
using System.Data;
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
            String sqlstring = "SELECT f.COLUMN_NAME, f.IS_NULLABLE, f.DATA_TYPE, r.CONSTRAINT_TYPE " +
                               "FROM INFORMATION_SCHEMA.COLUMNS f LEFT OUTER JOIN(SELECT c.COLUMN_NAME, c.CONSTRAINT_NAME, t.CONSTRAINT_TYPE " +
                               "FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE c INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS t ON  c.CONSTRAINT_NAME = t.CONSTRAINT_NAME " +
                               "WHERE c.TABLE_NAME = '"+tablename+"' AND t.CONSTRAINT_TYPE = 'PRIMARY KEY') r ON f.COLUMN_NAME = r.COLUMN_NAME " +
                               "WHERE f.TABLE_NAME = '"+tablename+"';";
            sqlcommand = new SqlCommand(sqlstring, conn);
            SqlDataReader reader = sqlcommand.ExecuteReader();
            List<TableInfo> tbinf = new List<TableInfo>();
            String column_name;
            Boolean is_nullable;
            String data_type;
            Boolean is_primary_key;
            DataType datatype;
            while (reader.Read())
            {
                column_name = reader.GetString(0);
                is_nullable = reader.GetString(1)=="YES"?true:false;
                data_type = reader.GetString(2);
                try
                {
                    is_primary_key = reader.GetString(3).Equals("PRIMARY KEY");

                }
                catch (System.Data.SqlTypes.SqlNullValueException)
                {
                    is_primary_key = false;
                }
                datatype = getDType(data_type);
                tbinf.Add(new TableInfo(column_name, is_nullable, datatype, is_primary_key));
            }
            reader.Close();
            return tbinf;
        }

        public DataTable getDataFromTable(String tablename)
        {
            String sqlstring = "SELECT * " +
                               "FROM "+tablename;
            DataTable dtable = new DataTable();
            sqlcommand = new SqlCommand(sqlstring, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcommand);
            adapter.Fill(dtable);
            return dtable;
        }

        public List<ConstraintInfo> getConstraintsFromTable(string tablename)
        {
            String sqlstring = "SELECT tc.CONSTRAINT_NAME, tc.CONSTRAINT_TYPE, cc.CHECK_CLAUSE, COLUMN_NAME, tc2.TABLE_NAME"+
                                "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc"+
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.CHECK_CONSTRAINTS cc ON tc.CONSTRAINT_NAME = cc.CONSTRAINT_NAME"+
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ic ON tc.CONSTRAINT_NAME = ic.CONSTRAINT_NAME"+
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc ON tc.CONSTRAINT_NAME = rc.CONSTRAINT_NAME"+
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON rc.CONSTRAINT_NAME = tc2.CONSTRAINT_NAME"+
                                "WHERE tc.TABLE_NAME = 'employee' AND tc.CONSTRAINT_TYPE != 'PRIMARY KEY'; ";
            sqlcommand = new SqlCommand(sqlstring, conn);
            SqlDataReader reader = sqlcommand.ExecuteReader();
            String column_name;
            String constraint_name;
            String condition;
            String type;
            List<ConstraintInfo> constraints = new List<ConstraintInfo>();
            while (reader.Read())
            {
                type = reader.GetString(1);
                if (type.Equals("FOREIGN KEY"))
                {
                    constraints.Add(new ConstraintInfo(ConstraintType.ForeignKey, reader.GetString(0), null, reader.GetString(3), reader.GetString(4)));
                }else if(type.Equals("UNIQUE"))
                {
                    constraints.Add(new ConstraintInfo(ConstraintType.UniqueKey, reader.GetString(0), null, reader.GetString(3)));
                }
                else if (type.Equals("CHECK"))
                {
                    constraints.Add(new ConstraintInfo(ConstraintType.Check, reader.GetString(0), reader.GetString(2), reader.GetString(3)));
                }
            }
            return new List<ConstraintInfo>();
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
            }
            else if (data.Equals("varchar2"))
            {
                return DataType.VARCHAR;
            }
            else if (data.Equals("int"))
            {
                return DataType.NUMBER;
            }else if (data.Equals("char"))
            {
                return DataType.CHAR;
            }
            else if (data.Equals("date"))
            {
                return DataType.DATE;
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
