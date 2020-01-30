using JRMigrator.beans;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace JRMigrator.DB
{
    public class OracleSQLConnection
    {
        private static OracleSQLConnection theInstance = null;
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
            String sqlstring = "SELECT acc.column_name, utc.nullable, utc.data_type, ac.constraint_name, ac.constraint_type " +
                               "FROM ALL_CONS_COLUMNS acc INNER JOIN USER_TAB_COLUMNS utc ON acc.column_name = utc.column_name " +
                               "INNER JOIN ALL_CONSTRAINTS ac ON acc.constraint_name = ac.constraint_name " +
                               "WHERE acc.owner = user AND acc.table_name = '"+tablename+"'; ";
            OracleCommand omd = new OracleCommand(sqlstring, conn);
            OracleDataReader reader = omd.ExecuteReader();
            List<TableInfo> tbinf = new List<TableInfo>();
            String column_name;
            Boolean is_nullable;
            String data_type;
            String primary_key_name;
            DataType datatype;
            while (reader.Read())
            {
                column_name = reader.GetString(0);
                is_nullable = reader.GetString(1) == "Y" ? true : false;
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
                return DataType.DATETIME;
            }
            else if (data.Equals("date"))
            {
                return DataType.DATETIME;
            }
            else if (data.Equals("number"))
            {
                return DataType.NUMBER;
            }
            return DataType.NULL;
        }
    }
}
