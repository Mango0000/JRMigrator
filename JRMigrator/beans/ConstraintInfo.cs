using System;
using System.Collections.Generic;
using System.Text;

namespace JRMigrator.beans
{
    public class ConstraintInfo
    {
        public ConstraintType constraintType { get; set; }
        public String constraintName { get; set; }
        public String Condition { get; set; }
        public String columnName { get; set; }
        public String tableName { get; set; }

        public ConstraintInfo(ConstraintType constraintType, String constraintName, String Condition, String columnName)
        {
            this.constraintType = constraintType;
            this.constraintName = constraintName;
            this.Condition = Condition;
            this.columnName = columnName;
        }

        public ConstraintInfo(ConstraintType constraintType, String constraintName, String Condition, String columnName, String tableName)
        {
            this.constraintType = constraintType;
            this.constraintName = constraintName;
            this.Condition = Condition;
            this.columnName = columnName;
            this.tableName = tableName;
        }

    }
}
