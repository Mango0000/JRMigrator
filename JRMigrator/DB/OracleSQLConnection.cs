using JRMigrator.beans;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace JRMigrator.DB
{
    class OracleSQLConnection
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

        public List<String> getAllTables()
        {
            String sqlString = "SELECT table_name FROM all_tables WHERE owner = user;";
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

        public List<String> getTableColumnTypes(String tableName)
        {
            var columnTypes = new List<String>();

            return columnTypes;
        }
    }
}
