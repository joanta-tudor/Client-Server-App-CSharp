using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieNetworking
{
    [Serializable]
    public class ExcursieDTO
    {
        IEnumerable<Excursie> exc;

        public ExcursieDTO(IEnumerable<Excursie> meciuri)
        {
            this.exc = meciuri;
        }

        public IEnumerable<Excursie> getExc()
        {
            return exc;
        }
    }
}
