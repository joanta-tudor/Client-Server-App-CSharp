using AgentieModell;
using AgentiePersistance;
using AgentieServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieServer
{
    public class ServerImplementation : AgentieService
    {
        RepositoryDB<int, Agent> repoAgent;
        RepositoryDB<int, Excursie> repoExc;
        RepositoryDB<int, Rezervare> repoRezv;
        private List<RezervareObserver> observers;

        public ServerImplementation()
        {
            repoAgent = new RepositoryDB<int, Agent>(new AgentRepository());
            repoExc = new RepositoryDB<int, Excursie>(new ExcursieRepository());
            repoRezv = new RepositoryDB<int, Rezervare>(new RezervareRepository());
            observers = new List<RezervareObserver>();
        }   

        public void Adauga(int id, string nume, string telefon, int bilet, Excursie ex, Agent ag)
        {
            if (ex.Locuri_disp < bilet)
            {
                throw new Exception("Not enough places available");
            }
            IEnumerable<Rezervare> l = new List<Rezervare>();
            l = GetAll();
            int I = 0;
            foreach (var x in l)
                I++;
            repoRezv.insert(new Rezervare(I + 1, ag.Id, ex.Id, nume, telefon, bilet));
            ex.Locuri_disp = ex.Locuri_disp - bilet;
            repoExc.update(ex);
            notify(id, nume, telefon, bilet, ex, ag);
        }

        public List<Excursie> Cauta(string nume, int intre1, int intre2)
        {
            List<Excursie> l = new List<Excursie>();
            foreach (var x in repoExc.findAll().ToList())
            {
                if (x.Nume.Equals(nume) && int.Parse(x.Plecare) >= intre1 && int.Parse(x.Plecare) <= intre2)
                    l.Add(x);
            }
            return l;
        }

        public IEnumerable<Rezervare> GetAll()
        {
            return repoRezv.findAll().ToList();
        }

        public IEnumerable<Excursie> GetAllE()
        {
            return repoExc.findAll().ToList();
        }

        public bool Login(int id, string pass, RezervareObserver obs)
        {
            Agent l = repoAgent.find(id);
            if (l == null)
                return false;
            if (l.Password == pass)
            {
                observers.Add(obs);
                return true;
            }
                
            return false;
        }

        public void notify(int id, String nume, String telefon, int bilet, Excursie ex, Agent ag)
        {
            foreach (RezervareObserver obs in observers)
            {
                //Task.Run(() => obs.rezervareUpdate(id,nume,telefon,bilet,ex,ag));
                obs.rezervareUpdate(id, nume, telefon, bilet, ex, ag);
            }
        }
    }
}
