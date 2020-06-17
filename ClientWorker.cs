using AgentieModell;
using AgentieServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static AgentieNetworking.RequestProtocol;
using static AgentieNetworking.ResponseProtocol;

namespace AgentieNetworking
{
    public class ClientWorker : RezervareObserver
    {
        private AgentieService server;
        private TcpClient connection;
        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

        public ClientWorker(AgentieService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public ClientWorker()
        {
        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private void sendResponse(Response response)
        {
            Console.WriteLine("Sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();
        }

        private object handleRequest(Request request)
        {
            Response response = null;
            if (request is LoginRequest)
            {
                Console.WriteLine("Login request ...");
                LoginRequest logReq = (LoginRequest)request;
                Agent userDTO = logReq.User;
                try
                {
                    bool ok = false;
                    lock (server)
                    {
                        ok = server.Login(userDTO.Id, userDTO.Password, this);
                    }
                    if (ok == true)
                    {
                        return new OkResponse();
                    }
                    connected = false;
                    return new ErrorResponse("Invalid connection...");
                }
                catch (Exception e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetAllRequest)
            {
                Console.WriteLine("GetAll request");
                try
                {
                    ExcursieDTO meciuriDTO = null;
                    lock (server)
                    {
                        List<Excursie> meciuri = new List<Excursie>();
                        foreach (Excursie meci in server.GetAllE())
                            meciuri.Add(meci);
                        meciuriDTO = new ExcursieDTO(meciuri);
                    }
                    return new GetAllResponse(meciuriDTO);
                }
                catch (Exception me)
                {
                    return new ErrorResponse(me.Message);
                }
            }
            if (request is GetAllAvailableRequest)
            {
                Console.WriteLine("GetAllAvailable request");
                try
                {
                    RezervareDTO meciuriDTO = null;
                    lock (server)
                    {
                        List<Rezervare> meciuri = new List<Rezervare>();
                        foreach (Rezervare meci in server.GetAll())
                            meciuri.Add(meci);
                        meciuriDTO = new RezervareDTO(meciuri);
                    }
                    return new GetAllAvailableResponse(meciuriDTO);
                }
                catch (Exception me)
                {
                    return new ErrorResponse(me.Message);
                }
            }
            if (request is CautaRequest)
            {
                Console.WriteLine("Cauta request");
                try
                {
                    List<Excursie> soldTickets = new List<Excursie>();
                    CautaDTO meciuriDTO;
                    CautaRequest saveRequest = (CautaRequest)request;
                    IntervalDTO req = saveRequest.Cautat;
                    lock (server)
                    {
                        soldTickets = server.Cauta(req.Nume, req.Intre1, req.Intre2);
                        meciuriDTO = new CautaDTO(soldTickets);
                    }
                    return new CautaResponse(meciuriDTO);
                }
                catch (Exception me)
                {
                    return new ErrorResponse(me.Message);
                }
            }
            if (request is AdaugaRequest)
            {
                Console.WriteLine("Adauga request");
                try
                {
                    AdaugaRequest updateMecirequest = (AdaugaRequest)request;
                    RezervareDTO1 req = updateMecirequest.Ticket;
                    lock (server)
                    {
                        server.Adauga(req.Id,req.Nume,req.Telefon,req.Bilet,req.Ex,req.Ag);
                    }
                    return new AdaugaResponse();
                }
                catch (Exception me)
                {
                    return new ErrorResponse(me.Message);
                }
            }
            return response;
        }

        public void rezervareUpdate(int id, string nume, string telefon, int bilet, Excursie ex, Agent ag)
        {
            try
            {
                sendResponse(new UpdateNrLocuriResponse(new RezervareDTO1(id,nume,telefon,bilet,ex,ag)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
