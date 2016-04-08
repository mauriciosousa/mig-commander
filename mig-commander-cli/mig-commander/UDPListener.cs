using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace mig_commander
{
    internal class UDPListener
    {
        private UdpClient _client;
        private int _port;

        public UDPListener(int port)
        {
            _port = port;
            _client = new UdpClient(_port);

            try
            {
                _client.BeginReceive(new AsyncCallback(recv), null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void recv(IAsyncResult res)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, _port);
            byte[] received = _client.EndReceive(res, ref ep);

            MigCommander.Instance.Receive(Encoding.UTF8.GetString(received));

            _client.BeginReceive(new AsyncCallback(recv), null);

        }
    }
}