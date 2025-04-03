//using System.Diagnostics;
//using System.Net.Sockets;

//namespace ClientApp
//{
//    internal class Program
//    {
//        static void ConnectServer(String server, int port)
//        {
//            string message, responseData;
//            int bytes;
//            try
//            {
//                //Create a TcpClient
//                TcpClient client = new TcpClient(server, port);
//                Console.Title = "Client Application";
//                NetworkStream stream = null;
//                while (true)
//                {
//                    Console.Write("Input message <press Enter to Exit>:");
//                    message = Console.ReadLine();
//                    if (message == string.Empty)
//                    {
//                        break;
//                    }
//                    //Translate the passed message into ASCII and store it as a byte array
//                    Byte[] data = System.Text.Encoding.ASCII.GetBytes($"{message}");
//                    //Get a client stream for reading and writing
//                    stream = client.GetStream();
//                    //Send the message to the connected TcpServer
//                    stream.Write(data, 0, data.Length);
//                    Console.WriteLine("Sent: {0}", message);
//                    //Receive the TcpServer response
//                    //Use buffe to store the response bytes
//                    data = new Byte[256];
//                    //Reade the first batch of the TcpServer response bytes.
//                    bytes = stream.Read(data, 0, data.Length);
//                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
//                    Console.WriteLine("Receive: {0}", responseData);
//                }
//                //Shutdown and end connection
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Exception: {0}", ex.Message);
//            } //end ConnectServer
//        }
//        static void Main(string[] args)
//        {
//            string server = "127.0.0.1";
//            //Set the TcpListener on port 13000
//            int port = 13000;
//            ConnectServer(server, port);
//        }
//    }
//}


using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientApp
{
    internal class Program
    {
        static void ConnectServer(string server, int port, string clientName)
        {
            try
            {
                TcpClient client = new TcpClient(server, port);
                Console.Title = clientName;
                NetworkStream stream = client.GetStream();

                byte[] nameData = Encoding.ASCII.GetBytes(clientName);
                stream.Write(nameData, 0, nameData.Length);

                Thread receiveThread = new Thread(() => ReceiveMessages(stream));
                receiveThread.Start();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now:T}] Welcome to the chat, {clientName}!");
                Console.ResetColor();

                while (true)
                {
                    Console.Write("You: ");
                    string message = Console.ReadLine();

                    if (string.IsNullOrEmpty(message))
                        break;

                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }

                client.Close();
                receiveThread.Join();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void ReceiveMessages(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n[{DateTime.Now:T}] {message}");
                    Console.ResetColor();
                    Console.Write("You: ");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Disconnected from server: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void Main(string[] args)
        {
            string server = "127.0.0.1";
            int port = 13000;

            string clientName = "Alice"; 
            ConnectServer(server, port, clientName);
        }
    }
}
