using JRMigrator.beans;
using JRMigrator.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace JRMigrator.BL
{
    class test
    {
        static void Main(string[] args)
        {
            test te = new test();
            te.testSequence();
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


    }
}
