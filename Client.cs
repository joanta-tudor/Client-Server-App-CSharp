using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieModell
{
    [Serializable]
    public class Client :Entity<int>
    {
        private string nume;
        private string telefon;
        private int bilete;

        public Client(int id, string nume, string telefon, int bilete)
        {
            base.Id = id;
            this.nume = nume;
            this.telefon = telefon;
            this.bilete = bilete;
        }

        public string Nume { get => nume; set => nume = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public int Bilete { get => bilete; set => bilete = value; }
    }
}
