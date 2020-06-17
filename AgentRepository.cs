using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using AgentieModell;

namespace AgentiePersistance
{
    public class AgentRepository : RepositoryEntity<int, Agent>
    {
        public Agent createEntity(SQLiteDataReader dr)
        {
            return new Agent(dr.GetInt32(0), dr.GetString(1));
        }

        public string deleteCommand()
        {
            return "delete  from Agent where id=@id";
        }

        public string findAllCommand()
        {
            return "select * from Agent";
        }

        public string findCommand()
        {
            return "select * from Agent where id=@id";
        }

        public string insertCommand()
        {
            return "insert into Agent values(@id,@name)";
        }

        public void setId(SQLiteCommand com, int id)
        {
            com.Parameters.AddWithValue("@id", id);
        }

        public void setParams(SQLiteCommand com, Agent ent)
        {
            com.Parameters.AddWithValue("@id", ent.Id);
            com.Parameters.AddWithValue("@name", ent.Password);
        }

        public void setUpdate(SQLiteCommand com, Agent ent)
        {
            com.Parameters.AddWithValue("@id", ent.Id);
            com.Parameters.AddWithValue("@name", ent.Password);
        }

        public string updateCommand()
        {
            return "update Agent set name=@name where id=@id";
        }
    }
}
