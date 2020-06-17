using AgentieModell;
using AgentieServices;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using static AgentieNetworking.ResponseProtocol;
using static AgentieNetworking.RequestProtocol;

namespace AgentieNetworking
{
    public class ClientServerProxy : AgentieService
    {
        private string host;
        private int port;

        private RezervareObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public ClientServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            this.responses = new Queue<Response>();
        }

        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void sendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new Exception("Error sending object " + e);
            }
        }

        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    response = responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (response is UpdateNrLocuriResponse)
                    {
                        handleUpdate((UpdateNrLocuriResponse)response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }
            }
        }

        private void handleUpdate(UpdateNrLocuriResponse update)
        {
            if (update is UpdateNrLocuriResponse)
            {
                UpdateNrLocuriResponse vanzareBileteUpdateResponse = (UpdateNrLocuriResponse)update;
                try
                {
                    Console.WriteLine("Sunt in proxy!");
                    client.rezervareUpdate(vanzareBileteUpdateResponse.Ticket.Id,vanzareBileteUpdateResponse.Ticket.Nume,vanzareBileteUpdateResponse.Ticket.Telefon,vanzareBileteUpdateResponse.Ticket.Bilet,vanzareBileteUpdateResponse.Ticket.Ex,vanzareBileteUpdateResponse.Ticket.Ag);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }


        public void Adauga(int id, string nume, string telefon, int bilet, Excursie ex, Agent ag)
        {
            RezervareDTO1 i = new RezervareDTO1(id,nume,telefon,bilet,ex,ag);
            sendRequest(new AdaugaRequest(i));
            Response res = readResponse();

            if (res is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)res;
                throw new Exception(err.Message);
            }
        }

        public List<Excursie> Cauta(string nume, int intre1, int intre2)
        {
            IntervalDTO i = new IntervalDTO(nume, intre1, intre2);
            sendRequest(new CautaRequest(i));
            Response res = readResponse();

            if (res is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)res;
                throw new Exception(err.Message);
            }
            CautaResponse resp = (CautaResponse)res;
            CautaDTO meciuriDTO = (CautaDTO) resp.Cautat;

            return meciuriDTO.getExc();
        }

        public IEnumerable<Rezervare> GetAll()
        {
            sendRequest(new GetAllAvailableRequest());
            Response res = readResponse();

            if (res is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)res;
                throw new Exception(err.Message);
            }
            GetAllAvailableResponse resp = (GetAllAvailableResponse)res;
            RezervareDTO meciuriDTO = resp.Rezervare;

            return meciuriDTO.getRezv();
        }

        public IEnumerable<Excursie> GetAllE()
        {
            sendRequest(new GetAllRequest());
            Response res = readResponse();

            if (res is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)res;
                throw new Exception(err.Message);
            }
            GetAllResponse resp = (GetAllResponse)res;
            ExcursieDTO meciuriDTO = resp.Excursie;

            return meciuriDTO.getExc();
        }

        public bool Login(int id, string pass, RezervareObserver obs)
        {
            initializeConnection();
            Agent userDTO = new Agent(id,pass);
            sendRequest(new LoginRequest(userDTO));
            Response res = readResponse();
            if (res is OkResponse)
            {
                this.client = obs;
                return true;
            }
            if (res is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)res;
                closeConnection();
                throw new Exception(err.Message);
            }
            return false;
        }

        private void closeConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                connection.Close();
                _waitHandle.Close();
                //client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
