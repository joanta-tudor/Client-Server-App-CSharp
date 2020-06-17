using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentieClient
{
    public enum Event
    {
        VanzareBilet
    };
    public class RezervareEventArgs : EventArgs
    {
        private readonly Event vanzareEvent;
        private readonly Object data;

        public RezervareEventArgs(Event vanzareEvent, object data)
        {
            this.vanzareEvent = vanzareEvent;
            this.data = data;
        }

        public Event UserEventType
        {
            get { return vanzareEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }
}
