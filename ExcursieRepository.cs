using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace AgentiePersistance
{
    public class ExcursieRepository : RepositoryEntity<int, Excursie>
    {
        public Excursie createEntity(SQLiteDataReader dr)
        {
            return new Excursie(dr.GetInt32(0), dr.GetString(1), dr.GetDouble(2), dr.GetInt32(3), dr.GetString(4), dr.GetString(5));

        }

        public String deleteCommand()
        {
            return "delete from Excursie where id=@id";
        }


        public String findAllCommand()
        {
            return "select * from Excursie";
        }


        public String findCommand()
        {
            return "select * from Excursie where id=@id";
        }


        public String insertCommand()
        {
            return "insert into Excursie values (@id,@plecare,@pret,@locuri,@agentie,@nume)";
        }


        public void setId(SQLiteCommand com, int id)
        {
            com.Parameters.AddWithValue("@id", id);
        }

        public void setParams(SQLiteCommand com, Excursie ent)
        {
            com.Parameters.AddWithValue("@id", ent.Id);
            com.Parameters.AddWithValue("@nume", ent.Nume);
            com.Parameters.AddWithValue("@plecare", ent.Plecare);
            com.Parameters.AddWithValue("@pret", ent.Pret);
            com.Parameters.AddWithValue("@locuri", ent.Locuri_disp);
            com.Parameters.AddWithValue("@agentie", ent.Transport);
        }

        public void setUpdate(SQLiteCommand com, Excursie ent)
        {
            com.Parameters.AddWithValue("@id", ent.Id);
            com.Parameters.AddWithValue("@bilete", ent.Locuri_disp);

        }

        public String updateCommand()
        {
            return "update Excursie set locuri_disp=@bilete where id=@id";
        }
    }
}
