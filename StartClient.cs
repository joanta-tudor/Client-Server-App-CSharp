using AgentieNetworking;
using AgentieServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentieClient
{
    static class StartClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AgentieService server = new ClientServerProxy("127.0.0.1", 55599);
            ClientController ctrl = new ClientController(server);

            Application.Run(new Login(ctrl));
        }
    }
}
