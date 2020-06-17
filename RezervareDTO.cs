using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieNetworking
{
    [Serializable]
    public class RezervareDTO
    {
        IEnumerable<Rezervare> exc;

        public RezervareDTO(IEnumerable<Rezervare> meciuri)
        {
            this.exc = meciuri;
        }

        public IEnumerable<Rezervare> getRezv()
        {
            return exc;
        }
    }
}
