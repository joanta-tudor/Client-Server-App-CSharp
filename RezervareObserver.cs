using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieServices
{
    public interface RezervareObserver
    {
        void rezervareUpdate(int id, String nume, String telefon, int bilet, Excursie ex, Agent ag);
    }
}
