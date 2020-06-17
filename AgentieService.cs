using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieServices
{
    public interface AgentieService
    {
        IEnumerable<Rezervare> GetAll();

        IEnumerable<Excursie> GetAllE();

        List<Excursie> Cauta(String nume, int intre1, int intre2) ;

        void Adauga(int id, String nume, String telefon, int bilet, Excursie ex, Agent ag);

        bool Login(int id, String pass, RezervareObserver obs) ;
    }
}
