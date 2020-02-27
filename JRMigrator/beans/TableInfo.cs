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
        public Boolean isPrimaryKey { get; set; }

        public TableInfo(String columnname, Boolean nullable, DataType datatype, Boolean isPrimaryKey)
        {
            this.columnname = columnname;
            this.nullable = nullable;
            this.datatype = datatype;
            this.isPrimaryKey = isPrimaryKey;
        }

        public override bool Equals(object obj)
        {
            return obj is TableInfo info &&
                   columnname == info.columnname &&
                   nullable == info.nullable &&
                   datatype == info.datatype;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(columnname, nullable, datatype);
        }
    }
}
