using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;

namespace AgentiePersistance
{
    public static class ConnectDb
    {
        static SQLiteConnection con = null;
        public static SQLiteConnection getConnection()
        {
            if (con == null)
            {
                con = new SQLiteConnection(ConfigurationManager.AppSettings["url"]);
                con.Open();
            }
            return con;
        }
    }
}
