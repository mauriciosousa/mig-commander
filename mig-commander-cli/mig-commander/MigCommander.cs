using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mig_commander
{
    public delegate void MigCommanderEventHandler(object sender, MigCommanderReceivedEventArgs e);

    public class MigCommanderReceivedEventArgs : EventArgs
    {
        public Command command;
        public MigCommanderReceivedEventArgs(Command c)
        {
            command = c;
        }
    }

    public class MigCommander
    {
        private static volatile MigCommander instance;
        private static object syncRoot = new Object();

        public static MigCommander Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MigCommander();
                    }
                }

                return instance;
            }
        }

        private static int UDP_PORT = 57743;
        private UDPBroadcast udpBroadcast;
        private UDPListener udpListener;

        public event MigCommanderEventHandler NewCommand;

        private MigCommander()
        {
            udpBroadcast = new UDPBroadcast(UDP_PORT);
            udpListener = new UDPListener(UDP_PORT + 1);
        }

        public void Broadcast(Command c)
        {
            udpBroadcast.send(c.getPDU());
        }

        internal void Receive(string r)
        {
            Command c = new Command(r);
            OnNewCommand(new MigCommanderReceivedEventArgs(c));
        }

        private void OnNewCommand(MigCommanderReceivedEventArgs e)
        {
            if (NewCommand != null)
                NewCommand(this, e);
        } 
    }
}
