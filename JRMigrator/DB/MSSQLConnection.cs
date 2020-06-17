using JRMigrator.beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

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

        public List<String> getViews()
        {
            String sqlstring = "SELECT VIEW_DEFINITION " +
                               "FROM INFORMATION_SCHEMA.VIEWS ";
            sqlcommand = new SqlCommand(sqlstring, conn);
            SqlDataReader reader = sqlcommand.ExecuteReader();
            List<String> views = new List<String>();
            while (reader.Read())
            {
                String view = reader.GetString(0).ToLower();
                view = view.Substring(view.IndexOf(("create")));
                MessageBox.Show(view);
                views.Add(view);
            }
            reader.Close();
            return views;
        }

            public List<ConstraintInfo> getConstraintsFromTable(string tablename)
        {
            String sqlstring = "SELECT DISTINCT tc.CONSTRAINT_NAME, tc.CONSTRAINT_TYPE, cc.CHECK_CLAUSE, ic.COLUMN_NAME, tc2.TABLE_NAME, ccu.COLUMN_NAME " +
                                "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc "+
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.CHECK_CONSTRAINTS cc ON tc.CONSTRAINT_NAME = cc.CONSTRAINT_NAME "+
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ic ON tc.CONSTRAINT_NAME = ic.CONSTRAINT_NAME "+
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc ON tc.CONSTRAINT_NAME = rc.CONSTRAINT_NAME "+
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON rc.UNIQUE_CONSTRAINT_NAME = tc2.CONSTRAINT_NAME " +
                                "LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu ON tc2.CONSTRAINT_NAME = ccu.CONSTRAINT_NAME "+
                                "WHERE tc.TABLE_NAME = '"+tablename+"' AND tc.CONSTRAINT_TYPE != 'PRIMARY KEY'; ";

            sqlcommand = new SqlCommand(sqlstring, conn);
            SqlDataReader reader = sqlcommand.ExecuteReader();
            String column_name;
            String constraint_name;
            String condition;
            String type;
            List<ConstraintInfo> constraints = new List<ConstraintInfo>();
            List<String> constraintscol = new List<String>();

            while (reader.Read())
            {
                try
                {
                    type = reader.GetString(1);
                    if (type.Equals("FOREIGN KEY"))
                    {
                        if (!constraintscol.Contains(reader.GetString(3)+reader.GetString(5)+reader.GetString(4)))
                        {
                            constraintscol.Add(reader.GetString(3)+reader.GetString(5)+reader.GetString(4));
                            try
                            {
                                int index = constraints.FindIndex(ConstraintInfo =>
                                    ConstraintInfo.constraintName == reader.GetString(0));
                                ConstraintInfo ci = constraints[index];
                                constraints.RemoveAt(index);
                                if (!ci.columnName.Contains(reader.GetString(3)))
                                {
                                    ci.columnName = ci.columnName + "," + reader.GetString(3);
                                }

                                if (!ci.FKcolumnName.Contains(reader.GetString(5)))
                                {
                                    ci.FKcolumnName = ci.FKcolumnName + "," + reader.GetString(5);
                                }
                            }

                            catch (ArgumentOutOfRangeException)
                            {
                                constraints.Add(new ConstraintInfo(ConstraintType.ForeignKey, reader.GetString(0), "",
                                    reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                            }
                        }
                    }
                    else if (type.Equals("UNIQUE"))
                    {
                        constraints.Add(new ConstraintInfo(ConstraintType.UniqueKey, reader.GetString(0), "", reader.GetString(3),"",""));
                    }
                    else if (type.Equals("CHECK"))
                    {
                        constraints.Add(new ConstraintInfo(ConstraintType.Check, reader.GetString(0), reader.GetString(2), reader.GetString(3),"",""));
                    }
                }
                catch (System.Data.SqlTypes.SqlNullValueException)
                {
                    //break;
                }
            }
            reader.Close();
            return constraints;
            
        }

            private DataType getDType(String data)
        {

            switch (data)
            {
               case "varchar": return DataType.VARCHAR;
               case "varchar2": return DataType.VARCHAR;
               case "datetime": return DataType.DATETIME;
               case "date": return DataType.DATE;
               case "text": return DataType.VARCHAR;
               case "int": return DataType.INT;
               case "char": return DataType.CHAR;
               case "float": return DataType.NUMERIC;
               case "nvarchar": return DataType.VARCHAR;
               case "smallint": return DataType.SMALLINT;
               case "numeric": return DataType.NUMERIC;
               case "decimal": return DataType.DECIMAL;
               case "real": return DataType.NUMERIC;
               default: return DataType.NULL;
            }
           
            
          
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
