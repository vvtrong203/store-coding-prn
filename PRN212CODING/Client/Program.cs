﻿using System;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void ConnectServer(string server, int port)
        {
            string message, responseData;
            int bytes;
            try
            {
                // Create a TcpClient
                TcpClient client = new TcpClient(server, port);
                Console.Title = "TCP Client Application";
                NetworkStream stream = null;

                while (true)
                {
                    Console.Write("Input message <press Enter to exit>: ");
                    message = Console.ReadLine();
                    if (string.IsNullOrEmpty(message))
                        break;

                    // Translate the message into ASCII bytes
                    Byte[] data = Encoding.ASCII.GetBytes(message);

                    // Get stream for reading and writing
                    stream = client.GetStream();

                    // Send the message to the connected server
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", message);

                    // Receive the server response
                    data = new Byte[256];
                    bytes = stream.Read(data, 0, data.Length);
                    responseData = Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);
                }

                // Shutdown and end connection
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }

        static void Main(string[] args)
        {
            string server = "127.0.0.1";
            int port = 13000;
            ConnectServer(server, port);
        }
    }
}
