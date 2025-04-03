using System.Net.Sockets;
using System.Text;

namespace Client_2
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

                // Gửi tên của client đến server
                byte[] nameData = Encoding.ASCII.GetBytes(clientName);
                stream.Write(nameData, 0, nameData.Length);

                // Tạo luồng để nhận tin nhắn
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
                    Console.Write("You: "); // Đưa con trỏ nhập liệu về dòng của người dùng
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

            string clientName = "Bob"; // Tên cố định cho Client 2
            ConnectServer(server, port, clientName);
        }
    }
}
