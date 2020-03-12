using System;
using System.Collections.Generic;
using System.Text;

namespace JRMigrator.beans
{
    public class ConstraintInfo
    {
        public ConstraintInfo(ConstraintType constraintType, string constraintName, string condition, string columnName)
        {
            this.constraintType = constraintType;
            this.constraintName = constraintName;
            Condition = condition;
            this.columnName = columnName;
        }

        public ConstraintType constraintType { get; set; }
        public String constraintName { get; set; }
        public String Condition { get; set; }
        public String columnName { get; set; }
    }
}
