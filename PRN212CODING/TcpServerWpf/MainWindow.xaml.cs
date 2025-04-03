//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Windows;

//namespace TcpServerWpf
//{
//    public partial class MainWindow : Window
//    {
//        private TcpListener _server;
//        private Thread _serverThread;
//        private int clientCount = 0;
//        private bool _isRunning = false;

//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void StartServer_Click(object sender, RoutedEventArgs e)
//        {
//            if (_isRunning) return;

//            IPAddress ip = IPAddress.Parse(HostInput.Text);
//            int port = int.Parse(PortInput.Text);

//            _server = new TcpListener(ip, port);
//            _server.Start();
//            _isRunning = true;

//            Log("Server started, waiting for clients...");

//            _serverThread = new Thread(ListenForClients);
//            _serverThread.IsBackground = true;
//            _serverThread.Start();
//        }

//        private void StopServer_Click(object sender, RoutedEventArgs e)
//        {
//            if (!_isRunning) return;

//            _isRunning = false;
//            try
//            {
//                _server?.Stop();
//            }
//            catch (Exception ex)
//            {
//                Log($"Stop error: {ex.Message}");
//            }

//            _serverThread?.Join();  // Đợi thread dừng hẳn
//            Log("Server stopped.");
//        }

//        private void ListenForClients()
//        {
//            try
//            {
//                while (_isRunning)
//                {
//                    if (_server.Pending())
//                    {
//                        TcpClient client = _server.AcceptTcpClient();
//                        clientCount++;
//                        Log($"Client {clientCount} connected.");

//                        Thread clientThread = new Thread(HandleClient);
//                        clientThread.IsBackground = true;
//                        clientThread.Start(client);
//                    }
//                    else
//                    {
//                        Thread.Sleep(100); // Giảm CPU khi không có kết nối chờ
//                    }
//                }
//            }
//            catch (SocketException ex)
//            {
//                if (_isRunning)  // Chỉ log nếu không phải do dừng server
//                    Log($"Socket error: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                Log($"Server error: {ex.Message}");
//            }
//        }

//        private void HandleClient(object clientObj)
//        {
//            try
//            {
//                var client = clientObj as TcpClient;
//                var stream = client.GetStream();
//                byte[] buffer = new byte[256];
//                int count;

//                while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
//                {
//                    string received = Encoding.ASCII.GetString(buffer, 0, count);
//                    Log($"Received: {received}");

//                    string response = received.ToUpper();
//                    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
//                    stream.Write(responseBytes, 0, responseBytes.Length);
//                    Log($"Sent: {response}");
//                }

//                client.Close();
//            }
//            catch (Exception ex)
//            {
//                Log($"Client error: {ex.Message}");
//            }
//        }

//        private void Log(string message)
//        {
//            Dispatcher.Invoke(() =>
//            {
//                LogOutput.AppendText($"{DateTime.Now:T}: {message}\n");
//                LogOutput.ScrollToEnd();
//            });
//        }
//    }
//}




using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace TcpServerWpf
{
    public partial class MainWindow : Window
    {
        private TcpListener _server;
        private Thread _serverThread;
        private bool _isRunning = false;
        private List<TcpClient> _connectedClients = new List<TcpClient>();
        private object _lock = new object();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartServer_Click(object sender, RoutedEventArgs e)
        {
            if (_isRunning) return;

            IPAddress ip = IPAddress.Parse(HostInput.Text);
            int port = int.Parse(PortInput.Text);

            _server = new TcpListener(ip, port);
            _server.Start();
            _isRunning = true;

            Log("Server started, waiting for clients...");

            _serverThread = new Thread(ListenForClients);
            _serverThread.IsBackground = true;
            _serverThread.Start();
        }

        private void StopServer_Click(object sender, RoutedEventArgs e)
        {
            if (!_isRunning) return;

            _isRunning = false;
            try
            {
                lock (_lock)
                {
                    foreach (var client in _connectedClients)
                    {
                        client.Close();
                    }
                    _connectedClients.Clear();
                }
                _server?.Stop();
            }
            catch (Exception ex)
            {
                Log($"Stop error: {ex.Message}");
            }

            _serverThread?.Join();
            Log("Server stopped.");
        }

        private void ListenForClients()
        {
            try
            {
                while (_isRunning)
                {
                    if (_server.Pending())
                    {
                        TcpClient client = _server.AcceptTcpClient();
                        lock (_lock)
                        {
                            _connectedClients.Add(client);
                        }
                        Log($"New client connected. Total clients: {_connectedClients.Count}");

                        Thread clientThread = new Thread(HandleClient);
                        clientThread.IsBackground = true;
                        clientThread.Start(client);
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                if (_isRunning)
                    Log($"Server error: {ex.Message}");
            }
        }

        private void HandleClient(object clientObj)
        {
            TcpClient client = clientObj as TcpClient;
            var stream = client.GetStream();
            byte[] buffer = new byte[256];

            try
            {
                int count;
                while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, count);
                    Log($"Received: {message}");

                    // Broadcast cho tất cả client
                    BroadcastMessage(message, client);
                }
            }
            catch (Exception ex)
            {
                Log($"Client error: {ex.Message}");
            }
            finally
            {
                lock (_lock)
                {
                    _connectedClients.Remove(client);
                }
                client.Close();
                Log($"Client disconnected. Total clients: {_connectedClients.Count}");
            }
        }

        private void BroadcastMessage(string message, TcpClient senderClient)
        {
            lock (_lock)
            {
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                foreach (var client in _connectedClients)
                {
                    if (client.Connected && client != senderClient)
                    {
                        try
                        {
                            client.GetStream().Write(messageBytes, 0, messageBytes.Length);
                        }
                        catch (Exception ex)
                        {
                            Log($"Broadcast error: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void Log(string message)
        {
            Dispatcher.Invoke(() =>
            {
                LogOutput.AppendText($"{DateTime.Now:T}: {message}\n");
                LogOutput.ScrollToEnd();
            });
        }
    }
}
