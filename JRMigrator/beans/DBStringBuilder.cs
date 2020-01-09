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

        public String getConnectionString()
        {
            return String.Format("server={0};database={1};port={2};user={3};password={4};Connection Timeout=3", ip, databasename, port, username, password);
        }
    }
}
