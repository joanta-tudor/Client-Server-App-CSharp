using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieNetworking
{
    [Serializable]
    class RequestProtocol
    {
        public interface Request
        {
        }

        [Serializable]
        public class LoginRequest : Request
        {
            private Agent userDTO;

            public LoginRequest(Agent userDTO)
            {
                this.userDTO = userDTO;
            }

            public virtual Agent User
            {
                get
                {
                    return userDTO;
                }
            }
        }

        [Serializable]
        public class GetAllRequest : Request
        {
            private ExcursieDTO meciuriDTO;

            public GetAllRequest(ExcursieDTO meciuriDTO)
            {
                this.meciuriDTO = meciuriDTO;
            }

            public GetAllRequest()
            {

            }
            public virtual ExcursieDTO Excursie
            {
                get
                {
                    return meciuriDTO;
                }
            }
        }

        [Serializable]
        public class GetAllAvailableRequest : Request
        {
            private RezervareDTO rezv;

            public GetAllAvailableRequest(RezervareDTO availableMeci)
            {
                this.rezv = availableMeci;
            }

            public GetAllAvailableRequest()
            {
            }

            public virtual RezervareDTO Rezervare
            {
                get
                {
                    return rezv;
                }
            }
        }

        [Serializable]
        public class CautaRequest : Request
        {
            private IntervalDTO cautat;

            public CautaRequest(IntervalDTO soldTicket)
            {
                this.cautat = soldTicket;
            }

            public CautaRequest()
            {
            }

            public virtual IntervalDTO Cautat
            {
                get
                {
                    return cautat;
                }
            }
        }

        [Serializable]
        public class AdaugaRequest : Request
        {
            private RezervareDTO1 rezv;

            public AdaugaRequest(RezervareDTO1 soldTicket)
            {
                this.rezv = soldTicket;
            }

            public AdaugaRequest()
            {
            }

            public virtual RezervareDTO1 Ticket
            {
                get
                {
                    return rezv;
                }
            }
        }

        [Serializable]
        public class UpdateNrLocuriRequest : Request
        {
            private RezervareDTO1 soldTicket;

            public UpdateNrLocuriRequest(RezervareDTO1 soldTicket)
            {
                this.soldTicket = soldTicket;
            }

            public UpdateNrLocuriRequest()
            {
            }

            public virtual RezervareDTO1 Ticket
            {
                get
                {
                    return soldTicket;
                }
            }
        }

    }
}
