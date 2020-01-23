using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CUBRID.Data.CUBRIDClient;
using JRMigrator.DB;

namespace JRMigrator.BL
{
    public class MigrateTest
    {
        private List<String> tables;
        public void tablesTest(OracleSQLConnection os,CUBRIDConnection cs)
        {
           
           tables=os.GetAllTables();
           for (int i = 0; i <tables.Count; i++)
           {
               String insert = "Create Table " + tables[i] + " ( test int primary key);";
               CUBRIDCommand cmd=new CUBRIDCommand(insert,cs);
              cmd.ExecuteNonQuery();
              //MessageBox.Show();
           }
        }
     
    }
}