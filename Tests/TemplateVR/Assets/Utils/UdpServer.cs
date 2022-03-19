using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class UdpServer
    {
        private Socket server;

        public event Action<byte[]> ReceivedFrame;

        public bool IsRunning { get; private set; } = true;

        private int serverPort = 1337;
        private long packetSize = 1024 * 50;

        public UdpServer()
        {
            this.server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        private void ReceiveLoop()
        {
            Debug.Log("Started Receive Loop!");
            try
            {
                while (this.IsRunning)
                {
                    using (var mem = new MemoryStream())
                    {
                        while (true)
                        {
                            var buf = new byte[this.packetSize];
                            EndPoint ep = new IPEndPoint(IPAddress.Any, this.serverPort);
                            this.server.ReceiveFrom(buf, ref ep);

                            if (buf[0] == 'F')
                                if (Encoding.Default.GetString(buf).Contains("FRAME END"))
                                    break;

                            mem.Write(buf, 0, buf.Length);
                        }
                        Debug.Log("Received frame");

                        this.ReceivedFrame.Invoke(mem.ToArray());
                    }
                }

                this.server.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void Start()
        {
            this.server.Bind(new IPEndPoint(IPAddress.Any, this.serverPort));
            Task.Factory.StartNew(() =>
                this.ReceiveLoop());
        }
    }
}
