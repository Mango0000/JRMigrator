using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CUBRID.Data.CUBRIDClient;
using JRMigrator.beans;
using JRMigrator.DB;

namespace JRMigrator.BL
{
    public class MigrateTest
    {
        private List<String> tables;

        public void getOracleTables(OracleSQLConnection os, CUBRIDConnection cs)
        {
            MessageBox.Show("alright");
            /*tables=os.GetAllTables();
            for (int i = 0; i <tables.Count; i++)
            {
                String insert = "Create Table " + tables[i] + " ( test int primary key);";
                CUBRIDCommand cmd=new CUBRIDCommand(insert,cs);
               cmd.ExecuteNonQuery();
               //MessageBox.Show();
            }*/
        }

        public void getMSSqlTables(MSSQLConnection ms, CUBRIDConnection cs)
        {
            String ispk;
            tables = ms.getTables();
            String cols="";
              for (int i = 0; i <tables.Count; i++)
              {
                  List<TableInfo> infos= ms.getInfo(tables[i]);
                  for (int j = 0; j <infos.Count; j++)
                  {
                      String name=infos[j].columnname;
                      DataType type = infos[j].datatype;
                      Boolean nullable = infos[j].nullable;
                      String pk=infos[j].PrimaryKeyName;
                      if (j == infos.Count - 1)
                      {
                          if (pk != null)
                          {
                              cols += name + " " + type + " primary key";
                          }
                          else
                          {
                              cols += name + " " + type;
                          } 
                      }
                      else
                      {
                          if (pk != null)
                          {
                              cols += name + " " + type + " primary key" + ", ";
                          }
                          else
                          {
                              cols += name + " " + type + ", ";
                          }
                      }

                      MessageBox.Show(cols);
                  }
                  
                 String insert = "Create Table " + tables[i] + "( "+cols+");";
                  CUBRIDCommand cmd=new CUBRIDCommand(insert,cs);
                  cmd.ExecuteNonQuery();
                  cols = "";
                  //MessageBox.Show();
              }
          

        }
    }
}