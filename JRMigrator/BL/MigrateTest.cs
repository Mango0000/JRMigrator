using System;
using System.Collections.Generic;
using System.Data;
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
        private String pks = "";
        private String insert = "";
        private DataTable dt=new DataTable();
        private LinkedList<DataRow> row;
        private String altertablestatement = "";
        private List<ConstraintInfo> constraints;
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
                    constraints = ms.getConstraintsFromTable(tables[i]);
                    
                    for (int j = 0; j < constraints.Count; j++)
                    {
                        if ((constraints[j].constraintType+"").Equals("ForeignKey"))
                        {
                            
                            String stat = "Alter table " + tables[i] + " add foreign key(" + constraints[j].columnName +
                                          ")" +
                                          " references " + constraints[j].tableName+ "("+ constraints[j].columnNameConstraint+");\n";
                            altertablestatement += stat;
                          
                        }else if((constraints[j].constraintType+"").Contains("Unique"))
                        {
                            String stat = "Alter table " + tables[i] + " add unique(" + constraints[j].columnName + ");"; 
                            altertablestatement += stat;  
                        }
                        else
                        {
                            String stat = "Alter table " + tables[i] + " add check( " + constraints[j].columnName +constraints[j].Condition+ ");"; 
                            altertablestatement += stat;  
                        }
                    }
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
                    
                    
                        if (pk)
                        {
                            pks += name + ",";
                            cols += name + " " + type + ",";
                        }
                        else
                        {
                            if (!nullable)
                            {
                                cols += name + " " + type + " not null "+",";
                            }
                            else
                            {
                                cols += name + " " + type+",";
                            }
                        
                    }

                    //  MessageBox.Show(cols);
                }

                try
                {
               
                    if (pks.Length == 0)
                    {
                       insert  += "Create Table " + tables[i] + "( " + cols.Substring(0,cols.Length-1)+ ");\n";
                       
                    }
                    if (pks.Length >0)
                    {
                        insert  += "Create Table " + tables[i] + "( " +  cols+"Primary key (" +
                                  pks.Substring(0, pks.Length - 1) + ")" + ");\n";
                    }
                    pks = "";
                  cols = "";
                    erfolgreich = "Migration of Tables successfully completed...";
                }
                catch (Exception e)
                {
                    erfolgreich = "Migration of tables failed...";
                    Console.Out.WriteLine(e.ToString());
                }

            }
            CUBRIDCommand cmd = new CUBRIDCommand(insert, cs);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < tables.Count; i++)
            {
                gettableData(tables[i],ms,os,cs);
            }
            CUBRIDCommand cmd2 = new CUBRIDCommand(altertablestatement, cs);
            cmd2.ExecuteNonQuery();
        }
        public  void gettableData(String tablename,MSSQLConnection ms,OracleSQLConnection os,CUBRIDConnection cs)
        {
            String data = "";
            if (os == null)
            {
                dt = ms.getDataFromTable(tablename);
            }
            else
            {
                dt = os.getDataFromTable(tablename); 
            }

            foreach (DataRow row in dt.Rows)
            {
                Object[] array = row.ItemArray;
                for (int i = 0; i <array.Length; i++)
                {
                 //   MessageBox.Show(array[i].GetType()+"");
                    if ((array[i].GetType() + "").Contains("Date"))
                    {
                        
                        data += "to_date("+"'" + (array[i]+"").Substring(0,10)+"'"+","+"'dd.mm.yyyy'"+")"+",";
                    }

                   else if ((array[i].GetType() + "").Contains("String"))
                    {
                        data += "'" + array[i]+"'"+",";  
                    }
                    else
                    {
                        
                            if ((array[i]+"").Equals(""))
                            {
                                data += "null,";
                            }
                        
                        
                        else
                        {
                            if ((array[i] + "").Contains(","))
                            {
                                array[i] = (array[i] + "").Replace(",", ".");
                            }
                            data += array[i] + ",";
                        }
                    }
                  
                    
                }
                data=data.Substring(0, data.Length - 1);
                String insert = "Insert into " + tablename + " Values(" + data + ")";
                CUBRIDCommand cmd = new CUBRIDCommand(insert, cs);
                cmd.ExecuteNonQuery();
               
                data="";
                 

            }

           
            //String insert = "Insert into " + tablename + " Values(" + ")";
        }


        public String getErfolgreich()
        {
            return erfolgreich;
        }
    }

}