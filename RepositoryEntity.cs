using AgentieModell;
using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentiePersistance
{
    public interface RepositoryEntity<ID, T> where T : Entity<ID>
    {
        String insertCommand();
        void setParams(SQLiteCommand com, T ent);
        String findCommand();
        void setId(SQLiteCommand com, ID id);
        T createEntity(SQLiteDataReader dr);
        String findAllCommand();
        String updateCommand();
        String deleteCommand();
        void setUpdate(SQLiteCommand com, T ent);
    }
}
