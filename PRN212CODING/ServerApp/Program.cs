//using System.Net;
//using System.Net.Sockets;

//namespace ServerApp
//{
//    internal class Program
//    {
//        static void ProcessMessage(object parm)
//        {
//            string data;
//            int count;
//            try
//            {
//                TcpClient client = parm as TcpClient;
//                // Buffer for reading data
//                Byte[] bytes = new Byte[256];
//                // Get a stream object for reading and writing
//                NetworkStream stream = client.GetStream();
//                // Loop to receive all the data sent by the client.
//                while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
//                {
//                    // Translate data bytes to a ASCII string.
//                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
//                    Console.WriteLine($"Received: {data} at {DateTime.Now:t}");
//                    // Process the data sent by the client.
//                    data = $"{data.ToUpper()}";
//                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
//                    // Send back a response.
//                    stream.Write(msg, 0, msg.Length);
//                    Console.WriteLine($"Sent: {data}");
//                }
//                // Shutdown and end connection
//                client.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("{0}", ex.Message);
//                Console.WriteLine("Waiting message...");
//            }
//        } 
//        static void ExecuteServer(string host, int port)
//        {
//            int Count = 0;
//            TcpListener server = null;
//            try
//            {
//                Console.Title = "Server Application";
//                IPAddress localAddr = IPAddress.Parse(host);
//                server = new TcpListener(localAddr, port);
//                // Start listening for client requests.
//                server.Start();
//                Console.WriteLine(new string('*', 40));
//                Console.WriteLine("Waiting for a connection... ");
//                // Enter the listening loop.
//                while (true)
//                {
//                    // Perform a blocking call to accept requests.
//                    TcpClient client = server.AcceptTcpClient();
//                    Console.WriteLine($"Number of client connected: {++Count}");
//                    Console.WriteLine(new string('*', 40));
//                    // Create a thread to receive and send message
//                    Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
//                    thread.Start(client);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Exception: {0}", ex.Message);
//            }
//            finally
//            {
//                server.Stop();
//                Console.WriteLine("Server stopped. Press any key to exit !");
//                Console.Read();
//            }
//        }


//        static void Main(string[] args)
//        {
//            string host = "127.0.0.1";
//            //Set port TcpListener on port 13000
//            int port = 13000;
//            ExecuteServer(host, port);
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerApp
{
    internal class Program
    {
        private static List<TcpClient> clients = new List<TcpClient>();

        static void Main(string[] args)
        {
            int port = 13000;
            TcpListener server = null;
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                Console.WriteLine("Server started on port {0}", port);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    clients.Add(client);

                    Thread clientThread = new Thread(HandleClient);
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
            finally
            {
                server?.Stop();
            }
        }

        private static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead;

            try
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientName = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Client connected: {0}", clientName);

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("{0}: {1}", clientName, message);

                    BroadcastMessage($"{clientName}: {message}", client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client disconnected: {0}", ex.Message);
            }
            finally
            {
                clients.Remove(client);
                client.Close();
            }
        }

        private static void BroadcastMessage(string message, TcpClient sender)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);

            foreach (var client in clients)
            {
                if (client == sender) continue;

                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
