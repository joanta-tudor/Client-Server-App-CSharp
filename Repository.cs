using System;
using System.Collections.Generic;
using AgentieModell;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentiePersistance
{
    public interface Repository<ID, T> where T : Entity<ID>
    {
        void insert(T ent);
        T find(ID id);
        IEnumerable<T> findAll();
        void update(T ent);
        void delete(T ent);

    }
}
