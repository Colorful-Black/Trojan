using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TrojanClient
{
    class Client
    {
        TcpClient client;
        long fileLength;
        BinaryReader reader;
        byte[] data = new byte[8192];

        //准备工作
        public Client(IPAddress serverIp)
        {
            int port = 6000;
            client = new TcpClient();
            client.Connect(serverIp, port);
            Console.WriteLine("连上了");
            NetworkStream stream = client.GetStream();
            reader = new BinaryReader(stream);
        }

        //接收数据
        public void recv()
        {
            try
            {
                Image bmp = Bitmap.FromStream(reader.BaseStream);
                bmp.Save(DateTime.Now.Ticks + ".gif");
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据传输异常或者服务端已离线:" + ex.Message);
            }
            reader.Close();
            client.Close();
        }
    }
}
