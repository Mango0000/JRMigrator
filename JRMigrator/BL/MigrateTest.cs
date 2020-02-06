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
                                cols += name + " " + type + "not null";
                            }
                            else
                            {
                                
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
                                cols += name + " " + type + "not null"+", ";
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
                    cmd.ExecuteNonQuery();
                    cols = "";
                    erfolgreich = "Migration of Tables successfully completed...";
                }
                catch (Exception e)
                {
                    erfolgreich = "Migration of tables failed...";
                }

            }
        }


        public String getErfolgreich()
        {
            return erfolgreich;
        }
    }

}