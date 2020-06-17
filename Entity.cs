using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieModell
{
    [Serializable]
    public class Entity<T>
    {
        T id;
        public T Id { get { return id; } set { id = value; } }
    }
}
