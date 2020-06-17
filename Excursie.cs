using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieModell
{
    [Serializable]
    public class Excursie : Entity<int>
    {
        private String plecare;
        private double pret;
        private int locuri_disp;
        private string transport;
        private string nume;

        public Excursie()
        {
        }

        public Excursie(int id, string plecare, double pret, int locuri_disp, string transport, string nume)
        {
            base.Id = id;
            this.plecare = plecare;
            this.pret = pret;
            this.locuri_disp = locuri_disp;
            this.transport = transport;
            this.nume = nume;
        }


        public string Plecare { get => plecare; set => plecare = value; }
        public double Pret { get => pret; set => pret = value; }
        public int Locuri_disp { get => locuri_disp; set => locuri_disp = value; }
        public string Transport { get => transport; set => transport = value; }
        public string Nume { get => nume; set => nume = value; }

        public override string ToString()
        {
            return Id + " " + plecare + " " + pret + " " + locuri_disp + " " + transport + " " + nume;
        }
    }
}
