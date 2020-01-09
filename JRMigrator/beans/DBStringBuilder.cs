using System;
using System.Collections.Generic;
using System.Text;

namespace JRMigrator.beans
{
    
    class DBStringBuilder
    {
        public DBType databasetype { get; set; }
        public String ip { get; set; }
        public String port { get; set; }
        public String databasename { get; set; }
        public String username { get; set; }
        public String password { get; set; }

        public DBStringBuilder(DBType databasetype, string ip, string port, string databasename, string username, string password)
        {
            this.databasetype = databasetype;
            this.ip = ip;
            this.port = port;
            this.databasename = databasename;
            this.username = username;
            this.password = password;
        }

        internal string getOracleConnectionString()
        {
            return String.Format("Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {2}))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl)));Password = {4};User ID = {3};", ip, databasename, port, username, password);
            //string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
           // +host + ")(PORT = " + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
            //+ sid + ")));Password=" + password + ";User ID=" + user;
        }

        public String getCubridConnectionString()
        {
            return String.Format("server={0};database={1};port={2};user={3};password={4}", ip, databasename, port, username, password);
        }

        public String getMSConnectionString()
        {
            return String.Format("Data Source = {0}, {2}; Initial Catalog = {1}; User ID = {3}; Password = {4}; Connection Timeout=3", ip, databasename, port, username, password);
        }
    }
}
