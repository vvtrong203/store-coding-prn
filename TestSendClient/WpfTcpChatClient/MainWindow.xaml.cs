using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfTcpChatClient
{
    public partial class MainWindow : Window
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private bool _listening = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(ServerInput.Text, int.Parse(PortInput.Text));
                _stream = _client.GetStream();
                LogMessage($"Connected to {ServerInput.Text}:{PortInput.Text}");
                _listening = true;
                StartListening();
            }
            catch (Exception ex)
            {
                LogMessage($"Connection error: {ex.Message}");
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (_stream == null || !_stream.CanWrite) return;

            try
            {
                string message = MessageInput.Text.Trim();
                if (string.IsNullOrEmpty(message)) return;

                byte[] data = Encoding.ASCII.GetBytes(message);
                await _stream.WriteAsync(data, 0, data.Length);
                LogMessage($"You: {message}");
                MessageInput.Clear();
            }
            catch (Exception ex)
            {
                LogMessage($"Send error: {ex.Message}");
            }
        }

        private async void StartListening()
        {
            try
            {
                byte[] buffer = new byte[256];
                while (_listening && _client.Connected)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        LogMessage($"Server: {response}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Disconnected or error: {ex.Message}");
            }
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _listening = false;
                _stream?.Close();
                _client?.Close();
                LogMessage("Disconnected from server.");
            }
            catch (Exception ex)
            {
                LogMessage($"Disconnection error: {ex.Message}");
            }
        }

        private void LogMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                ChatLog.Text += $"{DateTime.Now:HH:mm:ss} - {message}\n";
            });
        }
    }
}
