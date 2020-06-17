using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieNetworking
{
    [Serializable]
    class RezervareDTO1
    {
        int id;
        String nume;
        String telefon;
        int bilet;
        Excursie ex;
        Agent ag;

        public RezervareDTO1(int id, string nume, string telefon, int bilet, Excursie ex, Agent ag)
        {
            this.id = id;
            this.nume = nume;
            this.telefon = telefon;
            this.bilet = bilet;
            this.ex = ex;
            this.ag = ag;
        }

        public string Nume { get => nume; set => nume = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public int Id { get => id; set => id = value; }
        public int Bilet { get => bilet; set => bilet = value; }
        public Excursie Ex { get => ex; set => ex = value; }
        public Agent Ag { get => ag; set => ag = value; }
    }
}
