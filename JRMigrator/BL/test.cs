using JRMigrator.beans;
using JRMigrator.DB;
using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace JRMigrator.BL
{
    class test
    {
        static void Main(string[] args)
        {
            test te = new test();
            te.testViews();
        }

        public  void testSequence()
        {
            DBStringBuilder mssqlconnecitonString = new DBStringBuilder(DBType.MSSQL, "84.115.153.150", "1433", "testdb", "SA", "Migrate01");

            MSSQLConnection mssql = MSSQLConnection.getConnection();
            mssql.connectionString = mssqlconnecitonString;
            mssql.OpenConnection();
            Sequence sequence = mssql.GetSequences()[0];
            Console.Out.WriteLine(sequence.ToString());
        }

        public void testViews()
        {
            DBStringBuilder oracleSQL = new DBStringBuilder(DBType.OracleSQL, "db2.htl-kaindorf.at", "1521", "", "hofmad16", "Manuelh0");
            OracleSQLConnection oracleS = OracleSQLConnection.getConnection();
            oracleS.connectionString = oracleSQL;
            oracleS.OpenConnection();
            List<String> llist = oracleS.getViews();
            foreach(String l in llist)
            {
                Console.Out.WriteLine(l);
            }
            oracleS.CloseConnection();
        }


    }
}
