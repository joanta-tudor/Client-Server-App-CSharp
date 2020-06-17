using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace AgentiePersistance
{
    public class RezervareRepository : RepositoryEntity<int, Rezervare>
    {
        public Rezervare createEntity(SQLiteDataReader dr)
        {
            return new Rezervare(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetString(3), dr.GetString(4), dr.GetInt32(5));

        }

        public String deleteCommand()
        {
            return "delete from Rezervare where id=@id";
        }


        public String findAllCommand()
        {
            return "select * from Rezervare";
        }


        public String findCommand()
        {
            return "select * from Rezervare where id=@id";
        }


        public String insertCommand()
        {
            return "insert into Rezervare values (@id,@agent_id,@exc_id,@numeC,@telefonC,@bileteC)";
        }


        public void setId(SQLiteCommand com, int id)
        {
            com.Parameters.AddWithValue("@id", id);
        }

        public void setParams(SQLiteCommand com, Rezervare ent)
        {
            com.Parameters.AddWithValue("@id", ent.Id);
            com.Parameters.AddWithValue("@agent_id", ent.AgentId);
            com.Parameters.AddWithValue("@exc_id", ent.ExcId);
            com.Parameters.AddWithValue("@numeC", ent.ClientName);
            com.Parameters.AddWithValue("@telefonC", ent.PhoneNumber);
            com.Parameters.AddWithValue("@bileteC", ent.NumTickets);
        }

        public void setUpdate(SQLiteCommand com, Rezervare ent)
        {
            com.Parameters.AddWithValue("@id", ent.Id);
            com.Parameters.AddWithValue("@bilete", ent.NumTickets);
        }

        public String updateCommand()
        {
            return "update Punctaje  set  bilete=@bilete where id=@id";
        }
    }
}
