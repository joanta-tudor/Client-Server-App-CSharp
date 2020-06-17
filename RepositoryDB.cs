using AgentieModell;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentiePersistance
{
    public class RepositoryDB<ID, T> : Repository<ID, T> where T : Entity<ID>
    {
        SQLiteConnection con;
        RepositoryEntity<ID, T> config;
        public RepositoryDB(RepositoryEntity<ID, T> config)
        {
            this.config = config;
            con = ConnectDb.getConnection();
        }

        public void delete(T ent)
        {
            SQLiteCommand com = new SQLiteCommand(con);
            com.CommandText = config.deleteCommand();
            config.setId(com, ent.Id);
            com.ExecuteNonQuery();
        }

        public T find(ID id)
        {
            SQLiteCommand com = new SQLiteCommand(con);
            com.CommandText = config.findCommand();
            config.setId(com, id);
            SQLiteDataReader dr = com.ExecuteReader();
            T ent = null;
            if (dr.Read())
                ent = config.createEntity(dr);
            dr.Close();
            return ent;

        }

        public IEnumerable<T> findAll()
        {
            List<T> l = new List<T>();
            SQLiteCommand com = new SQLiteCommand(con);
            com.CommandText = config.findAllCommand();
            SQLiteDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                l.Add(config.createEntity(dr));
            }
            dr.Close();
            return l;
        }

        public void insert(T ent)
        {
            SQLiteCommand com = new SQLiteCommand(con);
            com.CommandText = config.insertCommand();
            config.setParams(com, ent);
            com.ExecuteNonQuery();
        }

        public void update(T ent)
        {
            SQLiteCommand com = new SQLiteCommand(con);
            com.CommandText = config.updateCommand();
            config.setUpdate(com, ent);
            com.ExecuteNonQuery();
        }
    }
}
