//using Microsoft.Win32;
//using System;
//using System.IO;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace FileClientWpf
//{
//    public partial class MainWindow : Window
//    {
//        private string selectedFilePath;

//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void SelectFile_Click(object sender, RoutedEventArgs e)
//        {
//            OpenFileDialog ofd = new OpenFileDialog();
//            if (ofd.ShowDialog() == true)
//            {
//                selectedFilePath = ofd.FileName;
//                SelectedFileText.Text = $"Đã chọn: {Path.GetFileName(selectedFilePath)}";
//            }
//        }

//        private async void SendFile_Click(object sender, RoutedEventArgs e)
//        {
//            if (string.IsNullOrEmpty(selectedFilePath))
//            {
//                StatusText.Text = "Vui lòng chọn file trước.";
//                return;
//            }

//            try
//            {
//                await Task.Run(() => SendFile(selectedFilePath));
//                StatusText.Text = "Gửi file thành công!";
//            }
//            catch (Exception ex)
//            {
//                StatusText.Text = $"Lỗi: {ex.Message}";
//            }
//        }

//        private async void SendFile(string filePath)
//        {
//            try
//            {
//                using TcpClient client = new TcpClient();
//                await client.ConnectAsync("127.0.0.1", 9000);
//                using NetworkStream stream = client.GetStream();
//                using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

//                string fileName = Path.GetFileName(filePath);
//                byte[] fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);
//                byte[] fileNameLen = BitConverter.GetBytes(fileNameBytes.Length);
//                byte[] fileLengthBytes = BitConverter.GetBytes(fs.Length);

//                // Gửi: độ dài tên file
//                await stream.WriteAsync(fileNameLen, 0, fileNameLen.Length);
//                // Gửi: tên file
//                await stream.WriteAsync(fileNameBytes, 0, fileNameBytes.Length);
//                // Gửi: kích thước file (long)
//                await stream.WriteAsync(fileLengthBytes, 0, fileLengthBytes.Length);

//                // Gửi nội dung file
//                byte[] buffer = new byte[8192];
//                int bytesRead;
//                while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
//                {
//                    await stream.WriteAsync(buffer, 0, bytesRead);
//                }

//                MessageBox.Show("Gửi file thành công!");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Lỗi: {ex.Message}");
//            }
//        }

//    }
//}


//using Microsoft.Win32;
//using System;
//using System.IO;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace FileClientWpf
//{
//    public partial class MainWindow : Window
//    {
//        private string selectedFilePath;

//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void SelectFile_Click(object sender, RoutedEventArgs e)
//        {
//            OpenFileDialog ofd = new OpenFileDialog();
//            if (ofd.ShowDialog() == true)
//            {
//                selectedFilePath = ofd.FileName;
//                SelectedFileText.Text = $"Đã chọn: {Path.GetFileName(selectedFilePath)}";
//            }
//        }

//        private async void SendFile_Click(object sender, RoutedEventArgs e)
//        {
//            if (string.IsNullOrEmpty(selectedFilePath))
//            {
//                StatusText.Text = "Vui lòng chọn file trước.";
//                return;
//            }

//            try
//            {
//                string fileContent = await Task.Run(() => SendFile(selectedFilePath));
//                StatusText.Text = "Gửi file thành công!";
//                MessageBox.Show($"Nội dung file từ server:\n{fileContent}", "Thành công");
//            }
//            catch (Exception ex)
//            {
//                StatusText.Text = $"Lỗi: {ex.Message}";
//            }
//        }

//        private async Task<string> SendFile(string filePath)
//        {
//            try
//            {
//                using TcpClient client = new TcpClient();
//                await client.ConnectAsync("127.0.0.1", 9000);
//                using NetworkStream stream = client.GetStream();
//                using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

//                string fileName = Path.GetFileName(filePath);
//                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
//                byte[] fileNameLen = BitConverter.GetBytes(fileNameBytes.Length);
//                byte[] fileLengthBytes = BitConverter.GetBytes(fs.Length);

//                // Gửi: độ dài tên file
//                await stream.WriteAsync(fileNameLen, 0, fileNameLen.Length);
//                // Gửi: tên file
//                await stream.WriteAsync(fileNameBytes, 0, fileNameBytes.Length);
//                // Gửi: kích thước file (long)
//                await stream.WriteAsync(fileLengthBytes, 0, fileLengthBytes.Length);

//                // Gửi nội dung file
//                byte[] buffer = new byte[8192];
//                int bytesRead;
//                while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
//                {
//                    await stream.WriteAsync(buffer, 0, bytesRead);
//                }

//                // Nhận nội dung file từ server
//                byte[] contentLengthBytes = new byte[4];
//                await stream.ReadAsync(contentLengthBytes, 0, 4);
//                int contentLength = BitConverter.ToInt32(contentLengthBytes, 0);

//                byte[] fileContent = new byte[contentLength];
//                int totalReceived = 0;
//                while (totalReceived < contentLength)
//                {
//                    int received = await stream.ReadAsync(fileContent, totalReceived, contentLength - totalReceived);
//                    if (received == 0)
//                        throw new Exception("Server closed connection unexpectedly.");
//                    totalReceived += received;
//                }

//                return Encoding.UTF8.GetString(fileContent);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error: {ex.Message}");
//            }
//        }
//    }
//}


//using Microsoft.Win32;
//using System;
//using System.IO;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace FileClientWpf
//{
//    public partial class MainWindow : Window
//    {
//        private string selectedFilePath;

//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void SelectFile_Click(object sender, RoutedEventArgs e)
//        {
//            OpenFileDialog ofd = new OpenFileDialog
//            {
//                Filter = "JSON Files (*.json)|*.json" // Chỉ cho phép chọn file JSON
//            };
//            if (ofd.ShowDialog() == true)
//            {
//                selectedFilePath = ofd.FileName;
//                SelectedFileText.Text = $"Đã chọn: {Path.GetFileName(selectedFilePath)}";
//            }
//        }

//        private async void SendFile_Click(object sender, RoutedEventArgs e)
//        {
//            if (string.IsNullOrEmpty(selectedFilePath))
//            {
//                StatusText.Text = "Vui lòng chọn file trước.";
//                return;
//            }

//            try
//            {
//                string jsonContent = await Task.Run(() => SendFile(selectedFilePath));
//                StatusText.Text = "Gửi file thành công!";
//                JsonContentText.Text = jsonContent; // Hiển thị nội dung JSON trong TextBox
//            }
//            catch (Exception ex)
//            {
//                StatusText.Text = $"Lỗi: {ex.Message}";
//                JsonContentText.Text = string.Empty;
//            }
//        }

//        private async Task<string> SendFile(string filePath)
//        {
//            try
//            {
//                using TcpClient client = new TcpClient();
//                await client.ConnectAsync("127.0.0.1", 9000);
//                using NetworkStream stream = client.GetStream();
//                using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

//                string fileName = Path.GetFileName(filePath);
//                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
//                byte[] fileNameLen = BitConverter.GetBytes(fileNameBytes.Length);
//                byte[] fileLengthBytes = BitConverter.GetBytes(fs.Length);

//                // Gửi: độ dài tên file
//                await stream.WriteAsync(fileNameLen, 0, fileNameLen.Length);
//                // Gửi: tên file
//                await stream.WriteAsync(fileNameBytes, 0, fileNameBytes.Length);
//                // Gửi: kích thước file (long)
//                await stream.WriteAsync(fileLengthBytes, 0, fileLengthBytes.Length);

//                // Gửi nội dung file
//                byte[] buffer = new byte[8192];
//                int bytesRead;
//                while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
//                {
//                    await stream.WriteAsync(buffer, 0, bytesRead);
//                }

//                // Nhận nội dung JSON từ server
//                byte[] contentLengthBytes = new byte[4];
//                await stream.ReadAsync(contentLengthBytes, 0, 4);
//                int contentLength = BitConverter.ToInt32(contentLengthBytes, 0);

//                byte[] jsonContentBytes = new byte[contentLength];
//                int totalReceived = 0;
//                while (totalReceived < contentLength)
//                {
//                    int received = await stream.ReadAsync(jsonContentBytes, totalReceived, contentLength - totalReceived);
//                    if (received == 0)
//                        throw new Exception("Server closed connection unexpectedly.");
//                    totalReceived += received;
//                }

//                string jsonContent = Encoding.UTF8.GetString(jsonContentBytes);
//                if (jsonContent.StartsWith("Error:"))
//                    throw new Exception(jsonContent); // Xử lý lỗi từ server
//                return jsonContent;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error: {ex.Message}");
//            }
//        }
//    }
//}

//using System;
//using System.Collections.Generic;
//using System.Net.Sockets;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace FileClientWpf
//{
//    public class Person
//    {
//        public string Name { get; set; }
//        public int Age { get; set; }
//    }

//    public partial class MainWindow : Window
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private async void SendList_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                // Tạo danh sách mẫu để gửi
//                List<Person> people = new List<Person>
//                {
//                    new Person { Name = "Alice", Age = 25 },
//                    new Person { Name = "Bob", Age = 30 },
//                    new Person { Name = "Charlie", Age = 35 }
//                };

//                List<Person> receivedPeople = await Task.Run(() => SendList(people));
//                StatusText.Text = "Gửi danh sách thành công!";

//                // Hiển thị danh sách nhận được trong TextBox
//                string displayText = "Danh sách nhận được từ server:\n";
//                foreach (var person in receivedPeople)
//                {
//                    displayText += $"- {person.Name}, {person.Age} tuổi\n";
//                }
//                ListContentText.Text = displayText;
//            }
//            catch (Exception ex)
//            {
//                StatusText.Text = $"Lỗi: {ex.Message}";
//                ListContentText.Text = string.Empty;
//            }
//        }

//        private async Task<List<Person>> SendList(List<Person> people)
//        {
//            try
//            {
//                using TcpClient client = new TcpClient();
//                await client.ConnectAsync("127.0.0.1", 9000);
//                using NetworkStream stream = client.GetStream();

//                // Serialize danh sách thành JSON
//                byte[] dataBytes = JsonSerializer.SerializeToUtf8Bytes(people);
//                byte[] dataLengthBytes = BitConverter.GetBytes(dataBytes.Length);

//                // Gửi độ dài dữ liệu
//                await stream.WriteAsync(dataLengthBytes, 0, dataLengthBytes.Length);
//                // Gửi dữ liệu
//                await stream.WriteAsync(dataBytes, 0, dataBytes.Length);

//                // Nhận danh sách từ server
//                byte[] responseLengthBytes = new byte[4];
//                await stream.ReadAsync(responseLengthBytes, 0, 4);
//                int responseLength = BitConverter.ToInt32(responseLengthBytes, 0);

//                byte[] responseBytes = new byte[responseLength];
//                int totalReceived = 0;
//                while (totalReceived < responseLength)
//                {
//                    int received = await stream.ReadAsync(responseBytes, totalReceived, responseLength - totalReceived);
//                    if (received == 0)
//                        throw new Exception("Server closed connection unexpectedly.");
//                    totalReceived += received;
//                }

//                string responseData = Encoding.UTF8.GetString(responseBytes);
//                if (responseData.StartsWith("Error:"))
//                    throw new Exception(responseData);

//                return JsonSerializer.Deserialize<List<Person>>(responseBytes);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error: {ex.Message}");
//            }
//        }
//    }
//}


//using System;
//using System.Collections.Generic;
//using System.Net.Sockets;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace FileClientWpf
//{
//    public class Person
//    {
//        public string Name { get; set; }
//        public int Age { get; set; }
//    }

//    public partial class MainWindow : Window
//    {
//        private List<Person> peopleList = new List<Person>();

//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void AddToList_Click(object sender, RoutedEventArgs e)
//        {
//            string name = NameTextBox.Text.Trim();
//            if (string.IsNullOrEmpty(name))
//            {
//                StatusText.Text = "Vui lòng nhập tên.";
//                return;
//            }

//            if (!int.TryParse(AgeTextBox.Text, out int age) || age < 0)
//            {
//                StatusText.Text = "Vui lòng nhập tuổi hợp lệ.";
//                return;
//            }

//            peopleList.Add(new Person { Name = name, Age = age });
//            UpdateCurrentListDisplay();

//            // Xóa dữ liệu nhập để nhập tiếp
//            NameTextBox.Text = string.Empty;
//            AgeTextBox.Text = string.Empty;
//            StatusText.Text = $"Đã thêm {name} vào danh sách.";
//        }

//        private async void SendList_Click(object sender, RoutedEventArgs e)
//        {
//            if (peopleList.Count == 0)
//            {
//                StatusText.Text = "Danh sách trống. Vui lòng thêm ít nhất một người.";
//                return;
//            }

//            try
//            {
//                List<Person> receivedPeople = await Task.Run(() => SendList(peopleList));
//                StatusText.Text = "Gửi danh sách thành công!";

//                // Hiển thị danh sách nhận được từ server
//                string displayText = "Danh sách nhận được từ server:\n";
//                foreach (var person in receivedPeople)
//                {
//                    displayText += $"- {person.Name}, {person.Age} tuổi\n";
//                }
//                ListContentText.Text = displayText;
//            }
//            catch (Exception ex)
//            {
//                StatusText.Text = $"Lỗi: {ex.Message}";
//                ListContentText.Text = string.Empty;
//            }
//        }

//        private void UpdateCurrentListDisplay()
//        {
//            string displayText = "Danh sách hiện tại:\n";
//            foreach (var person in peopleList)
//            {
//                displayText += $"- {person.Name}, {person.Age} tuổi\n";
//            }
//            CurrentListText.Text = displayText;
//        }

//        private async Task<List<Person>> SendList(List<Person> people)
//        {
//            try
//            {
//                using TcpClient client = new TcpClient();
//                await client.ConnectAsync("127.0.0.1", 9000);
//                using NetworkStream stream = client.GetStream();

//                // Serialize danh sách thành JSON
//                byte[] dataBytes = JsonSerializer.SerializeToUtf8Bytes(people);
//                byte[] dataLengthBytes = BitConverter.GetBytes(dataBytes.Length);

//                // Gửi độ dài dữ liệu
//                await stream.WriteAsync(dataLengthBytes, 0, dataLengthBytes.Length);
//                // Gửi dữ liệu
//                await stream.WriteAsync(dataBytes, 0, dataBytes.Length);

//                // Nhận danh sách từ server
//                byte[] responseLengthBytes = new byte[4];
//                await stream.ReadAsync(responseLengthBytes, 0, 4);
//                int responseLength = BitConverter.ToInt32(responseLengthBytes, 0);

//                byte[] responseBytes = new byte[responseLength];
//                int totalReceived = 0;
//                while (totalReceived < responseLength)
//                {
//                    int received = await stream.ReadAsync(responseBytes, totalReceived, responseLength - totalReceived);
//                    if (received == 0)
//                        throw new Exception("Server closed connection unexpectedly.");
//                    totalReceived += received;
//                }

//                string responseData = Encoding.UTF8.GetString(responseBytes);
//                if (responseData.StartsWith("Error:"))
//                    throw new Exception(responseData);

//                return JsonSerializer.Deserialize<List<Person>>(responseBytes);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error: {ex.Message}");
//            }
//        }
//    }
//}


using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FileClientWpf
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public partial class MainWindow : Window
    {
        private List<Person> peopleList = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Thêm Person vào danh sách từ giao diện
        private void AddToList_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                StatusText.Text = "Vui lòng nhập tên.";
                return;
            }

            if (!int.TryParse(AgeTextBox.Text, out int age) || age < 0)
            {
                StatusText.Text = "Vui lòng nhập tuổi hợp lệ.";
                return;
            }

            peopleList.Add(new Person { Name = name, Age = age });
            UpdateCurrentListDisplay();

            NameTextBox.Text = string.Empty;
            AgeTextBox.Text = string.Empty;
            StatusText.Text = $"Đã thêm {name} vào danh sách.";
        }

        // Import từ file JSON
        private void ImportJson_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    string jsonContent = File.ReadAllText(ofd.FileName);
                    peopleList = JsonSerializer.Deserialize<List<Person>>(jsonContent);
                    UpdateCurrentListDisplay();
                    StatusText.Text = $"Đã import danh sách từ {Path.GetFileName(ofd.FileName)}.";
                }
                catch (Exception ex)
                {
                    StatusText.Text = $"Lỗi khi import JSON: {ex.Message}";
                }
            }
        }

        // Gửi List<Person> lên server
        private async void SendList_Click(object sender, RoutedEventArgs e)
        {
            if (peopleList.Count == 0)
            {
                StatusText.Text = "Danh sách trống. Vui lòng thêm hoặc import dữ liệu.";
                return;
            }

            try
            {
                List<Person> receivedPeople = await Task.Run(() => SendList(peopleList));
                StatusText.Text = "Gửi danh sách thành công!";
                DisplayReceivedList(receivedPeople);
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Lỗi: {ex.Message}";
                ListContentText.Text = string.Empty;
            }
        }

        // Gửi file JSON trực tiếp lên server
        private async void SendJsonFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    List<Person> receivedPeople = await Task.Run(() => SendJsonFile(ofd.FileName));
                    StatusText.Text = "Gửi file JSON thành công!";
                    DisplayReceivedList(receivedPeople);
                }
                catch (Exception ex)
                {
                    StatusText.Text = $"Lỗi: {ex.Message}";
                    ListContentText.Text = string.Empty;
                }
            }
        }

        private void UpdateCurrentListDisplay()
        {
            string displayText = "Danh sách hiện tại:\n";
            foreach (var person in peopleList)
            {
                displayText += $"- {person.Name}, {person.Age} tuổi\n";
            }
            CurrentListText.Text = displayText;
        }

        private void DisplayReceivedList(List<Person> people)
        {
            string displayText = "Danh sách nhận được từ server:\n";
            foreach (var person in people)
            {
                displayText += $"- {person.Name}, {person.Age} tuổi\n";
            }
            ListContentText.Text = displayText;
        }

        private async Task<List<Person>> SendList(List<Person> people)
        {
            using TcpClient client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 9000);
            using NetworkStream stream = client.GetStream();

            // Gửi mã lệnh: 1 = Gửi List<Person>
            await stream.WriteAsync(BitConverter.GetBytes(1), 0, 4);

            byte[] dataBytes = JsonSerializer.SerializeToUtf8Bytes(people);
            byte[] dataLengthBytes = BitConverter.GetBytes(dataBytes.Length);
            await stream.WriteAsync(dataLengthBytes, 0, dataLengthBytes.Length);
            await stream.WriteAsync(dataBytes, 0, dataBytes.Length);

            return await ReceiveResponse(stream);
        }

        private async Task<List<Person>> SendJsonFile(string filePath)
        {
            using TcpClient client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 9000);
            using NetworkStream stream = client.GetStream();
            using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Gửi mã lệnh: 2 = Gửi file JSON
            await stream.WriteAsync(BitConverter.GetBytes(2), 0, 4);

            string fileName = Path.GetFileName(filePath);
            byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
            byte[] fileNameLen = BitConverter.GetBytes(fileNameBytes.Length);
            byte[] fileLengthBytes = BitConverter.GetBytes(fs.Length);

            await stream.WriteAsync(fileNameLen, 0, fileNameLen.Length);
            await stream.WriteAsync(fileNameBytes, 0, fileNameBytes.Length);
            await stream.WriteAsync(fileLengthBytes, 0, fileLengthBytes.Length);

            byte[] buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await stream.WriteAsync(buffer, 0, bytesRead);
            }

            return await ReceiveResponse(stream);
        }

        private async Task<List<Person>> ReceiveResponse(NetworkStream stream)
        {
            byte[] responseLengthBytes = new byte[4];
            await stream.ReadAsync(responseLengthBytes, 0, 4);
            int responseLength = BitConverter.ToInt32(responseLengthBytes, 0);

            byte[] responseBytes = new byte[responseLength];
            int totalReceived = 0;
            while (totalReceived < responseLength)
            {
                int received = await stream.ReadAsync(responseBytes, totalReceived, responseLength - totalReceived);
                if (received == 0)
                    throw new Exception("Server closed connection unexpectedly.");
                totalReceived += received;
            }

            string responseData = Encoding.UTF8.GetString(responseBytes);
            if (responseData.StartsWith("Error:"))
                throw new Exception(responseData);

            return JsonSerializer.Deserialize<List<Person>>(responseBytes);
        }
    }
}