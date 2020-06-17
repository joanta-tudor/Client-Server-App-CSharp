using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentieModell;
using System.Data.SQLite;

namespace AgentiePersistance
{
    class ClientRepository : RepositoryEntity<int, Client>
    {
        public Client createEntity(SQLiteDataReader dr)
        {
            return new Client(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetInt32(3));

        }

        public String deleteCommand()
        {
            return "delete from Clienti where id=@id";
        }


        public String findAllCommand()
        {
            return "select * from Clienti";
        }


        public String findCommand()
        {
            return "select * from Clienti where id=@id";
        }


        public String insertCommand()
        {
            return "insert into Clienti values (@id,@nume,@telefon,@bilete)";
        }


        public void setId(SQLiteCommand com, int id)
        {
            com.Parameters.AddWithValue("@id", id);
        }

        public void setParams(SQLiteCommand com, Client ent)
        {
            com.Parameters.AddWithValue("@id", ent.Id);
            com.Parameters.AddWithValue("@nume", ent.Nume);
            com.Parameters.AddWithValue("@telefon", ent.Telefon);
            com.Parameters.AddWithValue("@bilete", ent.Bilete);
        }

        public void setUpdate(SQLiteCommand com, Client ent)
        {
            com.Parameters.AddWithValue("@id", ent.Id);
            com.Parameters.AddWithValue("@bilete", ent.Bilete);

        }

        public String updateCommand()
        {
            return "update Punctaje  set  bilete=@bilete where id=@id";
        }
    }
}
