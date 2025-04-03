using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfTcpClient
{
    public partial class MainWindow : Window
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private bool _isListening = false;

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

                _isListening = true;
                _ = StartListening(); // fire and forget
            }
            catch (Exception ex)
            {
                LogMessage($"Error: {ex.Message}");
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (_stream == null || !_stream.CanWrite) return;

            try
            {
                string message = MessageInput.Text;
                byte[] data = Encoding.ASCII.GetBytes(message);
                await _stream.WriteAsync(data, 0, data.Length);
                LogMessage($"Sent: {message}");
                MessageInput.Clear();
            }
            catch (Exception ex)
            {
                LogMessage($"Send error: {ex.Message}");
            }
        }

        private async Task StartListening()
        {
            try
            {
                byte[] buffer = new byte[256];
                while (_isListening && _client.Connected)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        LogMessage("Server disconnected.");
                        break;
                    }

                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    LogMessage($"Received: {response}");
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Connection error: {ex.Message}");
            }
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            _isListening = false;

            try
            {
                _stream?.Close();
                _client?.Close();
            }
            catch (Exception ex)
            {
                LogMessage($"Disconnect error: {ex.Message}");
            }

            LogMessage("Disconnected from server.");
        }

        private void LogMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                ChatLog.Text += $"{DateTime.Now:T}: {message}\n";
                ChatLog.ScrollToEnd();
            });
        }
    }
}
