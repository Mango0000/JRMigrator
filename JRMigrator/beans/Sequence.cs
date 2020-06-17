using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace JRMigrator.beans
{
    public class Sequence
    {

        public String sequenceName { get; set; }
        public Int64 startNumber { get; set; }
        public Int64 increment { get; set; }
        public Int64 minValue { get; set; }
        public Int64 maxValue { get; set; }
        public Boolean cycle { get; set; }
        public Int64 cache { get; set; }

        public Sequence(String sequenceName, Int64 startNumber, Int64 increment, Int64 minValue, Int64 maxValue, Boolean cycle, Int64 cache)
        {
            this.sequenceName = sequenceName;
            this.startNumber = startNumber;
            this.increment = increment;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.cycle = cycle;
            this.cache = cache;
        }

        public Sequence(String sequenceName, Int64 startNumber, Int64 increment, Int64 minValue, Int64 maxValue, Boolean cycle)
        {
            this.sequenceName = sequenceName;
            this.startNumber = startNumber;
            this.increment = increment;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.cycle = cycle;
        }

        public override string ToString()
        {
            return sequenceName + ", " + startNumber + ", " + increment + ", " + minValue + ", " + maxValue + ", " + cycle + ", " + cache;
        }
    }
}
