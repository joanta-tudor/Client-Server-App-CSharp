using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieNetworking
{
    [Serializable]
    public class CautaDTO
    {
        List<Excursie> l;

        public CautaDTO(List<Excursie> meciuri)
        {
            this.l = meciuri;
        }

        public List<Excursie> getExc()
        {
            return l;
        }
    }
}
