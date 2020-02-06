using System;
using System.Collections.Generic;
using System.Text;

namespace JRMigrator.beans
{
    public class TableInfo
    {
        public String columnname { get; set; }
        public Boolean nullable { get; set; }
        public DataType datatype { get; set; }
        public String ConstraintName { get; set; }
        public Boolean isPrimaryKey { get; set; }

        public TableInfo(String columnname, Boolean nullable, DataType datatype, String ConstraintName, Boolean isPrimaryKey)
        {
            this.columnname = columnname;
            this.nullable = nullable;
            this.datatype = datatype;
            this.ConstraintName = ConstraintName;
            this.isPrimaryKey = isPrimaryKey;
        }
    }
}
