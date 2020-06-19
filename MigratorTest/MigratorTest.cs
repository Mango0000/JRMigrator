using Microsoft.VisualStudio.TestTools.UnitTesting;
using JRMigrator;
using JRMigrator.DB;
using System.CodeDom;
using JRMigrator.beans;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;

namespace MigratorTest
{
    [TestClass]
    public class MSSQLTest
    {
        private MSSQLConnection mssql;
        private readonly DBStringBuilder mssqlconnecitonString = new DBStringBuilder(DBType.MSSQL, "84.115.153.150", "1433", "testdb", "SA", "Migrate01");

        [TestInitialize]
        public void TestInitialize()
        {
            mssql = MSSQLConnection.getConnection();
            mssql.connectionString = mssqlconnecitonString;
            mssql.OpenConnection();
        }

        [TestMethod]
        public void VerifyTables()
        {
            List<string> expectedTables = new List<String>(new[] { "Birds", "Books", "Employee", "EmployeeDetails" });
            List<string> tables = mssql.getTables();
            foreach(String table in expectedTables)
            {
                Assert.AreEqual(true, tables.Contains(table));
            }
        }

        [TestMethod]
        public void CountTables()
        {
            int countTables = mssql.getTables().Count;
            int expectedTables = 4;
            Assert.AreEqual(expectedTables, countTables);
            
        }

        [TestMethod]
        public void VerifyInfo1()
        {
            List<TableInfo> tableinfo = mssql.getInfo("Birds");
            Assert.AreEqual(tableinfo[0].isPrimaryKey, true);
            Assert.AreEqual(tableinfo[0].nullable, false);
            Assert.AreEqual(tableinfo[0].datatype, DataType.INT);
            Assert.AreEqual(tableinfo[0].columnname, "ID");
        }

        [TestMethod]
        public void VerifyInfo2()
        {
            List<TableInfo> tableinfo = mssql.getInfo("Birds");
            Assert.AreEqual(tableinfo[1].isPrimaryKey, false);
            Assert.AreEqual(tableinfo[1].nullable, true);
            Assert.AreEqual(tableinfo[1].datatype, DataType.VARCHAR);
            Assert.AreEqual(tableinfo[1].columnname, "BirdName");
        }

        [TestMethod]
        public void VerifyData()
        {
            DataTable dt = mssql.getDataFromTable("Employee");
            Object cellValue = dt.Rows[0][0];
            Assert.AreEqual(cellValue, 1);
            cellValue = dt.Rows[1][1];
            Assert.AreEqual(cellValue, "MIKE PEARL");
            cellValue = dt.Rows[2][2];
            Assert.AreEqual(cellValue, "ACCOUNTANT");
            cellValue = dt.Rows[3][3];
            Assert.AreEqual(cellValue, "IT");
        }

        [TestMethod]
        public void VerifyViews()
        {
            List<String> views = mssql.getViews();
            String view = "create view birdsname as select birdname from birds";
            Assert.AreEqual(views[0].Trim('\r', '\n').Replace(System.Environment.NewLine, " "), view.Trim('\r', '\n'));
        }

        [TestMethod]
        public void CountViews()
        {
            int countViews = mssql.getViews().Count;
            int expected = 1;
            Assert.AreEqual(countViews, expected);
        }

        [TestMethod]
        public void VerifyConstraintsForeignKey()
        {
            List<ConstraintInfo> constraininfo = mssql.getConstraintsFromTable("EmployeeDetails");
            Assert.AreEqual(constraininfo[0].constraintType, ConstraintType.ForeignKey);
            Assert.AreEqual(constraininfo[0].columnName, "EmpID");
            Assert.AreEqual(constraininfo[0].FKcolumnName, "EmpID");
            Assert.AreEqual(constraininfo[0].FKtableName, "Employee");
            Assert.AreEqual(constraininfo[0].constraintName, "FK_EmployeeDetails_Employee");
            Assert.AreEqual(constraininfo[0].Condition, "");

        }

        [TestMethod]
        public void VerifyConstraintsCheck()
        {
            List<ConstraintInfo> constraininfo = mssql.getConstraintsFromTable("Birds");
            Assert.AreEqual(constraininfo[0].constraintType, ConstraintType.Check);
            Assert.AreEqual(constraininfo[0].constraintName, "CK__Birds__TypeOfBir__34C8D9D1");
            Assert.AreEqual(constraininfo[0].Condition, "([typeofbird]<>'langeweile')");
            Assert.AreEqual(constraininfo[0].columnName, "TypeOfBird");
            Assert.AreEqual(constraininfo[0].FKtableName, "");
            Assert.AreEqual(constraininfo[0].FKcolumnName, "");
        }

        [TestMethod]
        public void VerifyConstraintsEmpty()
        {
            Assert.AreEqual(!mssql.getConstraintsFromTable("Employee").Any(), true);
        }

        [TestMethod]
        public void VerifySequences()
        {
            Sequence sequence = mssql.GetSequences()[0];
            Assert.AreEqual(sequence.cycle, false);
            Assert.AreEqual(sequence.increment, 1);
            Assert.AreEqual(sequence.sequenceName, "testsequence");
            Assert.AreEqual(sequence.startNumber, 1);
            Assert.AreEqual(sequence.minValue, long.MinValue);
            Assert.AreEqual(sequence.maxValue, long.MaxValue);
            Assert.AreEqual(sequence.cache, 0);
        }

        [TestMethod]
        public void CountSequence()
        {
            int sequenceCount = mssql.GetSequences().Count;
            Assert.AreEqual(sequenceCount, 2);
        }

        [TestCleanup]
        public void PostInitialize()
        {
            mssql.CloseConnection();
        }
    }
}
