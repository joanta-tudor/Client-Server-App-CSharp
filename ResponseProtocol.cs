using AgentieModell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieNetworking
{
    class ResponseProtocol
    {
        public interface Response
        {

        }

        [Serializable]
        public class OkResponse : Response
        {

        }

        public interface UpdateResponse : Response
        {

        }

        [Serializable]
        public class ErrorResponse : Response
        {
            private string message;

            public ErrorResponse(string message)
            {
                this.message = message;
            }

            public virtual string Message
            {
                get
                {
                    return message;
                }
            }
        }

        [Serializable]
        public class LoginResponse : Response
        {
            private Agent userDTO;

            public LoginResponse(Agent userDTO)
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
        public class GetAllResponse : Response
        {
            private ExcursieDTO meciuriDTO;

            public GetAllResponse(ExcursieDTO meciuriDTO)
            {
                this.meciuriDTO = meciuriDTO;
            }

            public GetAllResponse()
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
        public class GetAllAvailableResponse : Response
        {
            private RezervareDTO rezv;

            public GetAllAvailableResponse(RezervareDTO availableMeci)
            {
                this.rezv = availableMeci;
            }

            public GetAllAvailableResponse()
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
        public class CautaResponse : Response
        {
            private CautaDTO cautat;

            public CautaResponse(CautaDTO soldTicket)
            {
                this.cautat = soldTicket;
            }

            public CautaResponse()
            {
            }

            public virtual CautaDTO Cautat
            {
                get
                {
                    return cautat;
                }
            }
        }

        [Serializable]
        public class AdaugaResponse : Response
        {
            private RezervareDTO1 rezv;

            public AdaugaResponse(RezervareDTO1 soldTicket)
            {
                this.rezv = soldTicket;
            }

            public AdaugaResponse()
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
        public class UpdateNrLocuriResponse : Response
        {
            private RezervareDTO1 soldTicket;

            public UpdateNrLocuriResponse(RezervareDTO1 soldTicket)
            {
                this.soldTicket = soldTicket;
            }

            public UpdateNrLocuriResponse()
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
