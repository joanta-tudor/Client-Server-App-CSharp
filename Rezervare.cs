using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieModell
{
    [Serializable]
    public class Rezervare : Entity<int>
    {
        private int agent_id;
        private int exc_id;
        private String numeC;
        private String telefeonC;
        private int bileteC;

        public Rezervare()
        {
        }

        public Rezervare(int id, int exc_id, int agent_id, string numeC, string telefonC, int bileteC)
        {
            base.Id = id;
            this.exc_id = exc_id;
            this.agent_id = agent_id;
            this.numeC = numeC;
            this.telefeonC = telefonC;
            this.bileteC = bileteC;
        }

        public override string ToString()
        {
            return Id + " " + agent_id + " " + exc_id + " " + numeC + " " + telefeonC + " " + bileteC;
        }

        public int ExcId { get => exc_id; set => exc_id = value; }
        public int AgentId { get => agent_id; set => agent_id = value; }
        public string ClientName { get => numeC; set => numeC = value; }
        public string PhoneNumber { get => telefeonC; set => telefeonC = value; }
        public int NumTickets { get => bileteC; set => bileteC = value; }
    }
}
