using AgentieModell;
using AgentieNetworking;
using AgentiePersistance;
using AgentieServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace AgentieServer
{
    class StartServer
    {
        static void Main(string[] args)
        {
            RepositoryDB<int, Agent>  repoAgent = new RepositoryDB<int, Agent>(new AgentRepository());
            RepositoryDB<int, Excursie>  repoExc = new RepositoryDB<int, Excursie>(new ExcursieRepository());
            RepositoryDB<int, Rezervare>  repoRezv = new RepositoryDB<int, Rezervare>(new RezervareRepository());
            AgentieService serviceImpl = new ServerImplementation();
            SerialServer server = new SerialServer("127.0.0.1", 55599, serviceImpl);
            server.Start();
        }

    }

    public class SerialServer : ConcurrentServer

    {
        private AgentieService server;
        private ClientWorker worker;
        public SerialServer(string host, int port, AgentieService server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("SerialServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new ClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
}
