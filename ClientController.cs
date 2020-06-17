using AgentieModell;
using AgentieServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieClient
{
    public class ClientController : RezervareObserver
    {
        private AgentieService server;
        public event EventHandler<RezervareEventArgs> updateEvent;

        public ClientController(AgentieService server)
        {
            this.server = server;
        }

        public bool Login(int id, string password)
        {
            return server.Login(id, password, this);
        }

        public IEnumerable<Excursie> findAll()
        {
            return server.GetAllE();
        }

        public IEnumerable<Rezervare> getAll()
        {
            return server.GetAll();
        }

        public List<Excursie> cauta(string nume, int intre1, int intre2)
        {
            return server.Cauta(nume, intre1, intre2);
        }

        public void adauga(int id, string nume, string telefon, int bilet, Excursie ex, Agent ag)
        {
            Console.WriteLine();
            server.Adauga(id,nume,telefon,bilet,ex,ag);
        }

        public void rezervareUpdate(int id, string nume, string telefon, int bilet, Excursie ex, Agent ag)
        {
            RezervareEventArgs userArgs = new RezervareEventArgs(Event.VanzareBilet, new Rezervare(id,ex.Id,ag.Id,nume,telefon,bilet));
            Console.WriteLine("Message received");
            OnMeciEvent(userArgs);
        }

        protected virtual void OnMeciEvent(RezervareEventArgs e)
        {
            Console.WriteLine("Am ajuns in event");
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
    }
}
