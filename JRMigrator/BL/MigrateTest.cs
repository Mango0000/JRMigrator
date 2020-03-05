using System;
using System.Collections.Generic;
using System.Security.AccessControl;
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
        private List<TableInfo> infos;

        public void migrateTables(OracleSQLConnection os,MSSQLConnection ms, CUBRIDConnection cs)
        {
            String cols = "";
            if (ms == null)
            {
                tables = os.GetAllTables();
            }
            else
            {
                tables = ms.getTables();  
            }

            for (int i = 0; i < tables.Count; i++)
            {
                cols = "";
                //MessageBox.Show(cols);
                if (ms == null)
                {
                   
                    infos = os.getInfo(tables[i]);
                    
                }
                else
                {
                    infos = ms.getInfo(tables[i]);  
                }

                for (int j = 0; j < infos.Count; j++)
                {
                   // MessageBox.Show(cols);
                    String name = infos[j].columnname;
                    String type = infos[j].datatype + "";
                    if (type == DataType.NUMBER + "")
                    {
                        type = "numeric";
                    }

                    Boolean nullable = infos[j].nullable;
                    Boolean pk = infos[j].isPrimaryKey;
                    if (j == infos.Count - 1)
                    {
                        if (pk)
                        {
                            cols += name + " " + type + " primary key";
                        }
                        else
                        {
                            if (!nullable)
                            {
                                cols += name + " " + type + " not null";
                            }
                            else
                            {
                                cols += name + " " + type;
                            }
                        }
                    }
                    else
                    {
                        if (pk)
                        {
                            cols += name + " " + type + " primary key" + ", ";
                        }
                        else
                        {
                            if (!nullable)
                            {
                                cols += name + " " + type + " not null "+", ";
                            }
                            else
                            {
                                cols += name + " " + type+",";
                            }
                        }
                    }

                    //  MessageBox.Show(cols);
                }

                try
                {
                    String insert = "Create Table " + tables[i] + "( " + cols + ");";
                    CUBRIDCommand cmd = new CUBRIDCommand(insert, cs);
                  // MessageBox.Show(insert);
                  cmd.ExecuteNonQuery();
                    cols = "";
                    erfolgreich = "Migration of Tables successfully completed...";
                }
                catch (Exception e)
                {
                    erfolgreich = "Migration of tables failed...";
                    Console.Out.WriteLine(e.ToString());
                }

            }
        }


        public String getErfolgreich()
        {
            return erfolgreich;
        }
    }

}