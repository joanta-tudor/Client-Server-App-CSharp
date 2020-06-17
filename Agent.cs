using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieModell
{
    [Serializable]
    public class Agent : Entity<int>
    {
        private String password;

        public override string ToString()
        {
            return Id + " " + password;
        }

        public Agent()
        {
        }

        public Agent(int id, string password)
        {
            base.Id = id;
            this.Password = password;
        }

        public string Password { get => password; set => password = value; }
    }
}
