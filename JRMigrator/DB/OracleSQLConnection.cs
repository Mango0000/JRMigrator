using JRMigrator.beans;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Windows.Forms;

namespace JRMigrator.DB
{
    public class OracleSQLConnection
    {
        private static OracleSQLConnection theInstance = null;
        private String colnames = "";
        public DBStringBuilder connectionString { get; set; } = null;
        private OracleConnection conn = null;

        public static OracleSQLConnection getConnection()
        {
            if (theInstance == null)
            {
                theInstance = new OracleSQLConnection();
            }
            return theInstance;
        }

        public Boolean OpenConnection()
        {
            if (connectionString != null)
            {
                conn = new OracleConnection(connectionString.getOracleConnectionString());
                conn.Open();
                return true;
            }
            return false;
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public List<String> GetAllTables()
        {
            String sqlString = "SELECT table_name FROM all_tables WHERE owner = user ";
            var tableList = new List<String>();
            using(OracleCommand omd = new OracleCommand(sqlString, conn))
            {
                using(DbDataReader reader = omd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Object[] values = new Object[reader.FieldCount];
                        int fieldCount = reader.GetValues(values);
                        foreach (Object obj in values)
                        {
                            tableList.Add(obj.ToString());
                        }
                    }
                }
            }
            return tableList;
        }

        public List<TableInfo> getInfo(String tablename)
        {
            String sqlstring = "SELECT DISTINCT atc.column_name, atc.data_type, atc.nullable, ar.constraint_type " +
            "FROM ALL_TAB_COLUMNS atc " +
            "LEFT OUTER JOIN(SELECT acc.table_name, column_name, ac.constraint_type " +
                "FROM ALL_CONS_COLUMNS acc " +
                "LEFT OUTER JOIN ALL_CONSTRAINTS ac ON acc.constraint_name = ac.constraint_name " +
                "WHERE ac.constraint_type = 'P') ar ON atc.column_name = ar.column_name AND atc.table_name = ar.table_name " +
            "WHERE atc.table_name = '" + tablename + "' AND atc.owner = USER";

            OracleCommand omd = new OracleCommand(sqlstring, conn);
            OracleDataReader reader = omd.ExecuteReader();
            List<TableInfo> tbinf = new List<TableInfo>();
            String column_name;
            Boolean is_nullable;
            String data_type;
            Boolean isPrimaryKey;

            DataType datatype;
            while (reader.Read())
            {
                column_name = reader.GetString(0);
                colnames += column_name + ",";
                is_nullable = reader.GetString(2) == "Y" ? true : false;
                data_type = reader.GetString(1);
                try
                {
                    isPrimaryKey = reader.GetString(3).Equals("P");
                }
                catch (System.InvalidCastException e)
                {
                    isPrimaryKey = false;
                }
                datatype = getDType(data_type);
                TableInfo ti = new TableInfo(column_name, is_nullable, datatype, isPrimaryKey);
                tbinf.Add(ti);
            }
            reader.Close();
            return tbinf;
        }

        public DataTable getDataFromTable(String tablename)
        {
            
            String sqlstring = "SELECT " +colnames.Substring(0,colnames.Length-1)+
                               " FROM " + tablename;
          //  MessageBox.Show(sqlstring);
            DataTable dtable = new DataTable();
            OracleCommand orcCommand = new OracleCommand(sqlstring, conn);
            OracleDataAdapter adapter = new OracleDataAdapter(orcCommand);
            adapter.Fill(dtable);
            colnames = "";
            return dtable;
        }

        public List<ConstraintInfo> getConstraintsFromTable(String tablename)
        {
            List<ConstraintInfo> listInfo = new List<ConstraintInfo>();

            String sqlstring = "SELECT ac.constraint_name, ac.constraint_type,ac.search_condition, acc.column_name" +
                "FROM ALL_CONSTRAINTS ac" +
                "LEFT OUTER JOIN ALL_CONS_COLUMNS acc ON ac.constraint_name = acc.constraint_name" +
                "WHERE ac.owner = user" +
                "AND ac.table_name = '"+tablename+"'" +
                "AND ac.constraint_type <> 'P'; ";

            OracleCommand omd = new OracleCommand(sqlstring, conn);
            OracleDataReader reader = omd.ExecuteReader();

            String constraint_name;
            String constraint_type;
            String condition;
            String column_name;
            

            DataType datatype;
            while (reader.Read())
            {
                constraint_name = reader.GetString(0);
                constraint_type = reader.GetString(1);
                condition = reader.GetString(2);
                column_name = reader.GetString(3);

             //   ConstraintInfo ci = new ConstraintInfo(constraint_type, constraint_name, condition, column_name);
              //  tbinf.Add(ti);
            }
            reader.Close();

            return listInfo;
        }

        private DataType getDType(String data)
        {
            data = data.ToLower();
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
            }
            else if (data.Equals("char"))
            {
                return DataType.CHAR;
            }
            else if (data.Equals("datetime"))
            {
                return DataType.DATE;
            }
            else if (data.Equals("date"))
            {
                return DataType.DATE;
            }
            else if (data.Equals("number"))
            {
                return DataType.NUMBER;
            }
            return DataType.NULL;
        }
    }
}
