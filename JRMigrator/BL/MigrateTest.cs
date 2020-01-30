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
        private String erfolgreich;
        public void getOracleTables(OracleSQLConnection os, CUBRIDConnection cs)
        {
           /* String cols="";
            for (int i = 0; i <tables.Count; i++)
            {
                List<TableInfo> infos= os.getInfo(tables[i]);
                for (int j = 0; j <infos.Count; j++)
                {
                    String name=infos[j].columnname;
                    String type = infos[j].datatype+"";
                    if (type == DataType.NUMBER+"")
                    {
                        type = "numeric";
                    }
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

                    //  MessageBox.Show(cols);
                }
                  
                String insert = "Create Table " + tables[i] + "( "+cols+");";
                CUBRIDCommand cmd=new CUBRIDCommand(insert,cs);
                cmd.ExecuteNonQuery();
                cols = "";*/
        }

        public void getMSSqlTables(MSSQLConnection ms, CUBRIDConnection cs)
        {
            tables = ms.getTables();
            String cols="";
              for (int i = 0; i <tables.Count; i++)
              {
                  List<TableInfo> infos= ms.getInfo(tables[i]);
                  for (int j = 0; j <infos.Count; j++)
                  {
                      String name=infos[j].columnname;
                      String type = infos[j].datatype+"";
                      if (type == DataType.NUMBER+"")
                      {
                          type = "numeric";
                      }
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

                   //  MessageBox.Show(cols);
                  }

                  try
                  {
                      String insert = "Create Table " + tables[i] + "( " + cols + ");";
                      CUBRIDCommand cmd = new CUBRIDCommand(insert, cs);
                      cmd.ExecuteNonQuery();
                      cols = "";
                      erfolgreich = "Migration of MSSQL Tables complete...";
                  }
                  catch (Exception e)
                  {
                      erfolgreich = "Migration of MSSQL Tables failed...";  
                  }

                  //MessageBox.Show();
              }
          

        }

        public String getErfolgreich()
        {
            return erfolgreich;
        }
    }
}