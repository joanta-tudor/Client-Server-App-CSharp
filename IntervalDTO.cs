using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieNetworking
{
    [Serializable]
    public class IntervalDTO
    {
        string nume;
        int intre1;
        int intre2;

        public IntervalDTO(string nume,int int1,int int2)
        {
            this.nume = nume;
            this.intre1 = int1;
            this.intre2 = int2;
        }

        public string Nume { get => nume; set => nume = value; }
        public int Intre1 { get => intre1; set => intre1 = value; }
        public int Intre2 { get => intre2; set => intre2 = value; }
    }
}
