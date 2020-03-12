using System;
using System.Collections.Generic;
using System.Text;

namespace JRMigrator.beans
{
    class Constraint
    {
        public ConstraintType constraintType { get; set; }
        public String constraintName { get; set; }
        public String Condition { get; set; }
        public String columnName { get; set; }
    }
}
