//using System;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;

//namespace FileServer
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            int port = 9000;
//            TcpListener server = new TcpListener(IPAddress.Any, port);
//            server.Start();
//            Console.WriteLine($"Server started on port {port}... (Press Ctrl+C to stop)");

//            // Xử lý tín hiệu dừng server
//            CancellationTokenSource cts = new CancellationTokenSource();
//            Console.CancelKeyPress += (s, e) => { cts.Cancel(); e.Cancel = true; };

//            try
//            {
//                while (!cts.Token.IsCancellationRequested)
//                {
//                    try
//                    {
//                        TcpClient client = server.AcceptTcpClient();
//                        Console.WriteLine("Client connected.");
//                        // Xử lý client trong luồng riêng để hỗ trợ nhiều kết nối
//                        ThreadPool.QueueUserWorkItem(HandleClient, new Tuple<TcpClient, CancellationToken>(client, cts.Token));
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine($"Error accepting client: {ex.Message}");
//                    }
//                }
//            }
//            finally
//            {
//                server.Stop();
//                Console.WriteLine("Server stopped.");
//            }
//        }

//        private static void HandleClient(object state)
//        {
//            var (client, token) = (Tuple<TcpClient, CancellationToken>)state;
//            try
//            {
//                using (client)
//                using (NetworkStream stream = client.GetStream())
//                {
//                    // Đọc độ dài tên file
//                    byte[] fileNameLenBytes = ReadExact(stream, 4);
//                    int nameLen = BitConverter.ToInt32(fileNameLenBytes, 0);
//                    if (nameLen <= 0 || nameLen > 255) // Giới hạn độ dài tên file
//                        throw new Exception("Invalid file name length.");

//                    // Đọc tên file
//                    byte[] fileNameBytes = ReadExact(stream, nameLen);
//                    string fileName = Encoding.UTF8.GetString(fileNameBytes);
//                    fileName = Path.GetFileName(fileName); // Làm sạch tên file

//                    // Đọc dung lượng file
//                    byte[] fileLengthBytes = ReadExact(stream, 8);
//                    long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
//                    if (fileLength < 0)
//                        throw new Exception("Invalid file length.");

//                    Console.WriteLine($"Receiving file: {fileName} ({fileLength} bytes)");

//                    // Lưu file
//                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "Received_" + fileName);
//                    using (FileStream output = new FileStream(savePath, FileMode.Create, FileAccess.Write))
//                    {
//                        byte[] buffer = new byte[8192];
//                        long totalRead = 0;
//                        while (totalRead < fileLength)
//                        {
//                            if (token.IsCancellationRequested)
//                                throw new OperationCanceledException("Server is shutting down.");

//                            int bytesToRead = (int)Math.Min(buffer.Length, fileLength - totalRead);
//                            int bytesRead = stream.Read(buffer, 0, bytesToRead);
//                            if (bytesRead == 0)
//                                throw new Exception("Connection closed before file fully received.");

//                            output.Write(buffer, 0, bytesRead);
//                            totalRead += bytesRead;
//                        }
//                    }

//                    // Gửi phản hồi cho client
//                    byte[] response = Encoding.UTF8.GetBytes("OK");
//                    stream.Write(response, 0, response.Length);

//                    Console.WriteLine($"File {fileName} received and saved at {savePath}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error handling client: {ex.Message}");
//            }
//        }

//        private static byte[] ReadExact(NetworkStream stream, int count)
//        {
//            byte[] buffer = new byte[count];
//            int offset = 0;
//            while (offset < count)
//            {
//                int read = stream.Read(buffer, offset, count - offset);
//                if (read == 0)
//                    throw new Exception("Connection closed while reading data.");
//                offset += read;
//            }
//            return buffer;
//        }
//    }
//}



//using System;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;

//namespace FileServer
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            int port = 9000;
//            TcpListener server = new TcpListener(IPAddress.Any, port);
//            server.Start();
//            Console.WriteLine($"Server started on port {port}... (Press Ctrl+C to stop)");

//            CancellationTokenSource cts = new CancellationTokenSource();
//            Console.CancelKeyPress += (s, e) => { cts.Cancel(); e.Cancel = true; };

//            try
//            {
//                while (!cts.Token.IsCancellationRequested)
//                {
//                    try
//                    {
//                        TcpClient client = server.AcceptTcpClient();
//                        Console.WriteLine("Client connected.");
//                        ThreadPool.QueueUserWorkItem(HandleClient, new Tuple<TcpClient, CancellationToken>(client, cts.Token));
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine($"Error accepting client: {ex.Message}");
//                    }
//                }
//            }
//            finally
//            {
//                server.Stop();
//                Console.WriteLine("Server stopped.");
//            }
//        }

//        private static void HandleClient(object state)
//        {
//            var (client, token) = (Tuple<TcpClient, CancellationToken>)state;
//            try
//            {
//                using (client)
//                using (NetworkStream stream = client.GetStream())
//                {
//                    // Đọc độ dài tên file
//                    byte[] fileNameLenBytes = ReadExact(stream, 4);
//                    int nameLen = BitConverter.ToInt32(fileNameLenBytes, 0);
//                    if (nameLen <= 0 || nameLen > 255)
//                        throw new Exception("Invalid file name length.");

//                    // Đọc tên file
//                    byte[] fileNameBytes = ReadExact(stream, nameLen);
//                    string fileName = Encoding.UTF8.GetString(fileNameBytes);
//                    fileName = Path.GetFileName(fileName);

//                    // Đọc dung lượng file
//                    byte[] fileLengthBytes = ReadExact(stream, 8);
//                    long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
//                    if (fileLength < 0)
//                        throw new Exception("Invalid file length.");

//                    Console.WriteLine($"Receiving file: {fileName} ({fileLength} bytes)");

//                    // Lưu file
//                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "Received_" + fileName);
//                    using (FileStream output = new FileStream(savePath, FileMode.Create, FileAccess.Write))
//                    {
//                        byte[] buffer = new byte[8192];
//                        long totalRead = 0;
//                        while (totalRead < fileLength)
//                        {
//                            if (token.IsCancellationRequested)
//                                throw new OperationCanceledException("Server is shutting down.");

//                            int bytesToRead = (int)Math.Min(buffer.Length, fileLength - totalRead);
//                            int bytesRead = stream.Read(buffer, 0, bytesToRead);
//                            if (bytesRead == 0)
//                                throw new Exception("Connection closed before file fully received.");

//                            output.Write(buffer, 0, bytesRead);
//                            totalRead += bytesRead;
//                        }
//                    }

//                    // Đọc nội dung file vừa nhận và gửi lại cho client
//                    byte[] fileContent = File.ReadAllBytes(savePath);
//                    byte[] contentLengthBytes = BitConverter.GetBytes(fileContent.Length);
//                    stream.Write(contentLengthBytes, 0, contentLengthBytes.Length); // Gửi độ dài nội dung
//                    stream.Write(fileContent, 0, fileContent.Length); // Gửi nội dung file

//                    Console.WriteLine($"File {fileName} received and sent back to client.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error handling client: {ex.Message}");
//            }
//        }

//        private static byte[] ReadExact(NetworkStream stream, int count)
//        {
//            byte[] buffer = new byte[count];
//            int offset = 0;
//            while (offset < count)
//            {
//                int read = stream.Read(buffer, offset, count - offset);
//                if (read == 0)
//                    throw new Exception("Connection closed while reading data.");
//                offset += read;
//            }
//            return buffer;
//        }
//    }
//}




//using System;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Text.Json; // Thêm để validate JSON
//using System.Threading;

//namespace FileServer
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            int port = 9000;
//            TcpListener server = new TcpListener(IPAddress.Any, port);
//            server.Start();
//            Console.WriteLine($"Server started on port {port}... (Press Ctrl+C to stop)");

//            CancellationTokenSource cts = new CancellationTokenSource();
//            Console.CancelKeyPress += (s, e) => { cts.Cancel(); e.Cancel = true; };

//            try
//            {
//                while (!cts.Token.IsCancellationRequested)
//                {
//                    try
//                    {
//                        TcpClient client = server.AcceptTcpClient();
//                        Console.WriteLine("Client connected.");
//                        ThreadPool.QueueUserWorkItem(HandleClient, new Tuple<TcpClient, CancellationToken>(client, cts.Token));
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine($"Error accepting client: {ex.Message}");
//                    }
//                }
//            }
//            finally
//            {
//                server.Stop();
//                Console.WriteLine("Server stopped.");
//            }
//        }

//        private static void HandleClient(object state)
//        {
//            var (client, token) = (Tuple<TcpClient, CancellationToken>)state;
//            try
//            {
//                using (client)
//                using (NetworkStream stream = client.GetStream())
//                {
//                    // Đọc độ dài tên file
//                    byte[] fileNameLenBytes = ReadExact(stream, 4);
//                    int nameLen = BitConverter.ToInt32(fileNameLenBytes, 0);
//                    if (nameLen <= 0 || nameLen > 255)
//                        throw new Exception("Invalid file name length.");

//                    // Đọc tên file
//                    byte[] fileNameBytes = ReadExact(stream, nameLen);
//                    string fileName = Encoding.UTF8.GetString(fileNameBytes);
//                    fileName = Path.GetFileName(fileName);
//                    if (!fileName.EndsWith(".json"))
//                        throw new Exception("Only JSON files are supported.");

//                    // Đọc dung lượng file
//                    byte[] fileLengthBytes = ReadExact(stream, 8);
//                    long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
//                    if (fileLength < 0)
//                        throw new Exception("Invalid file length.");

//                    Console.WriteLine($"Receiving file: {fileName} ({fileLength} bytes)");

//                    // Lưu file
//                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "Received_" + fileName);
//                    using (FileStream output = new FileStream(savePath, FileMode.Create, FileAccess.Write))
//                    {
//                        byte[] buffer = new byte[8192];
//                        long totalRead = 0;
//                        while (totalRead < fileLength)
//                        {
//                            if (token.IsCancellationRequested)
//                                throw new OperationCanceledException("Server is shutting down.");

//                            int bytesToRead = (int)Math.Min(buffer.Length, fileLength - totalRead);
//                            int bytesRead = stream.Read(buffer, 0, bytesToRead);
//                            if (bytesRead == 0)
//                                throw new Exception("Connection closed before file fully received.");

//                            output.Write(buffer, 0, bytesRead);
//                            totalRead += bytesRead;
//                        }
//                    }

//                    // Đọc và validate file JSON
//                    string jsonContent = File.ReadAllText(savePath);
//                    try
//                    {
//                        JsonDocument.Parse(jsonContent); // Kiểm tra JSON hợp lệ
//                    }
//                    catch (JsonException ex)
//                    {
//                        throw new Exception($"Invalid JSON file: {ex.Message}");
//                    }

//                    // Gửi nội dung JSON lại cho client
//                    byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
//                    byte[] contentLengthBytes = BitConverter.GetBytes(jsonBytes.Length);
//                    stream.Write(contentLengthBytes, 0, contentLengthBytes.Length); // Gửi độ dài nội dung
//                    stream.Write(jsonBytes, 0, jsonBytes.Length); // Gửi nội dung JSON

//                    Console.WriteLine($"File {fileName} received and sent back to client as JSON.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error handling client: {ex.Message}");
//                try
//                {
//                    // Gửi thông báo lỗi cho client nếu có thể
//                    if (client.Connected)
//                    {
//                        using NetworkStream stream = client.GetStream();
//                        string errorMsg = $"Error: {ex.Message}";
//                        byte[] errorBytes = Encoding.UTF8.GetBytes(errorMsg);
//                        byte[] errorLengthBytes = BitConverter.GetBytes(errorBytes.Length);
//                        stream.Write(errorLengthBytes, 0, errorLengthBytes.Length);
//                        stream.Write(errorBytes, 0, errorBytes.Length);
//                    }
//                }
//                catch { /* Bỏ qua lỗi khi gửi thông báo */ }
//            }
//        }

//        private static byte[] ReadExact(NetworkStream stream, int count)
//        {
//            byte[] buffer = new byte[count];
//            int offset = 0;
//            while (offset < count)
//            {
//                int read = stream.Read(buffer, offset, count - offset);
//                if (read == 0)
//                    throw new Exception("Connection closed while reading data.");
//                offset += read;
//            }
//            return buffer;
//        }
//    }
//}


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Text.Json;
//using System.Threading;

//namespace FileServer
//{
//    public class Person
//    {
//        public string Name { get; set; }
//        public int Age { get; set; }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            int port = 9000;
//            TcpListener server = new TcpListener(IPAddress.Any, port);
//            server.Start();
//            Console.WriteLine($"Server started on port {port}... (Press Ctrl+C to stop)");

//            CancellationTokenSource cts = new CancellationTokenSource();
//            Console.CancelKeyPress += (s, e) => { cts.Cancel(); e.Cancel = true; };

//            try
//            {
//                while (!cts.Token.IsCancellationRequested)
//                {
//                    try
//                    {
//                        TcpClient client = server.AcceptTcpClient();
//                        Console.WriteLine("Client connected.");
//                        ThreadPool.QueueUserWorkItem(HandleClient, new Tuple<TcpClient, CancellationToken>(client, cts.Token));
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine($"Error accepting client: {ex.Message}");
//                    }
//                }
//            }
//            finally
//            {
//                server.Stop();
//                Console.WriteLine("Server stopped.");
//            }
//        }

//        private static void HandleClient(object state)
//        {
//            var (client, token) = (Tuple<TcpClient, CancellationToken>)state;
//            try
//            {
//                using (client)
//                using (NetworkStream stream = client.GetStream())
//                {
//                    // Đọc độ dài dữ liệu
//                    byte[] dataLengthBytes = ReadExact(stream, 4);
//                    int dataLength = BitConverter.ToInt32(dataLengthBytes, 0);
//                    if (dataLength <= 0)
//                        throw new Exception("Invalid data length.");

//                    // Đọc dữ liệu
//                    byte[] dataBytes = ReadExact(stream, dataLength);
//                    string jsonData = Encoding.UTF8.GetString(dataBytes);

//                    // Deserialize thành List<Person>
//                    List<Person> people = JsonSerializer.Deserialize<List<Person>>(jsonData);
//                    Console.WriteLine($"Received list with {people.Count} people.");

//                    // Gửi lại danh sách cho client
//                    byte[] responseBytes = JsonSerializer.SerializeToUtf8Bytes(people);
//                    byte[] responseLengthBytes = BitConverter.GetBytes(responseBytes.Length);
//                    stream.Write(responseLengthBytes, 0, responseLengthBytes.Length); // Gửi độ dài
//                    stream.Write(responseBytes, 0, responseBytes.Length); // Gửi dữ liệu

//                    Console.WriteLine("List sent back to client.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error handling client: {ex.Message}");
//                try
//                {
//                    if (client.Connected)
//                    {
//                        using NetworkStream stream = client.GetStream();
//                        string errorMsg = $"Error: {ex.Message}";
//                        byte[] errorBytes = Encoding.UTF8.GetBytes(errorMsg);
//                        byte[] errorLengthBytes = BitConverter.GetBytes(errorBytes.Length);
//                        stream.Write(errorLengthBytes, 0, errorLengthBytes.Length);
//                        stream.Write(errorBytes, 0, errorBytes.Length);
//                    }
//                }
//                catch { /* Bỏ qua lỗi khi gửi thông báo */ }
//            }
//        }

//        private static byte[] ReadExact(NetworkStream stream, int count)
//        {
//            byte[] buffer = new byte[count];
//            int offset = 0;
//            while (offset < count)
//            {
//                int read = stream.Read(buffer, offset, count - offset);
//                if (read == 0)
//                    throw new Exception("Connection closed while reading data.");
//                offset += read;
//            }
//            return buffer;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace FileServer
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int port = 9000;
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine($"Server started on port {port}... (Press Ctrl+C to stop)");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) => { cts.Cancel(); e.Cancel = true; };

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        TcpClient client = server.AcceptTcpClient();
                        Console.WriteLine("Client connected.");
                        ThreadPool.QueueUserWorkItem(HandleClient, new Tuple<TcpClient, CancellationToken>(client, cts.Token));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error accepting client: {ex.Message}");
                    }
                }
            }
            finally
            {
                server.Stop();
                Console.WriteLine("Server stopped.");
            }
        }

        private static void HandleClient(object state)
        {
            var (client, token) = (Tuple<TcpClient, CancellationToken>)state;
            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    // Đọc mã lệnh (1 = List, 2 = File)
                    byte[] commandBytes = ReadExact(stream, 4);
                    int command = BitConverter.ToInt32(commandBytes, 0);

                    List<Person> people;
                    if (command == 1) // Nhận List<Person>
                    {
                        byte[] dataLengthBytes = ReadExact(stream, 4);
                        int dataLength = BitConverter.ToInt32(dataLengthBytes, 0);
                        if (dataLength <= 0)
                            throw new Exception("Invalid data length.");

                        byte[] dataBytes = ReadExact(stream, dataLength);
                        string jsonData = Encoding.UTF8.GetString(dataBytes);
                        people = JsonSerializer.Deserialize<List<Person>>(jsonData);
                        Console.WriteLine($"Received list with {people.Count} people.");
                    }
                    else if (command == 2) // Nhận file JSON
                    {
                        byte[] fileNameLenBytes = ReadExact(stream, 4);
                        int nameLen = BitConverter.ToInt32(fileNameLenBytes, 0);
                        if (nameLen <= 0 || nameLen > 255)
                            throw new Exception("Invalid file name length.");

                        byte[] fileNameBytes = ReadExact(stream, nameLen);
                        string fileName = Encoding.UTF8.GetString(fileNameBytes);

                        byte[] fileLengthBytes = ReadExact(stream, 8);
                        long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
                        if (fileLength < 0)
                            throw new Exception("Invalid file length.");

                        Console.WriteLine($"Receiving JSON file: {fileName} ({fileLength} bytes)");

                        string tempPath = Path.Combine(Path.GetTempPath(), fileName);
                        using (FileStream fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                        {
                            byte[] buffer = new byte[8192];
                            long totalRead = 0;
                            while (totalRead < fileLength)
                            {
                                int bytesToRead = (int)Math.Min(buffer.Length, fileLength - totalRead);
                                int bytesRead = stream.Read(buffer, 0, bytesToRead);
                                if (bytesRead == 0)
                                    throw new Exception("Connection closed before file fully received.");
                                fs.Write(buffer, 0, bytesRead);
                                totalRead += bytesRead;
                            }
                        }

                        string jsonContent = File.ReadAllText(tempPath);
                        people = JsonSerializer.Deserialize<List<Person>>(jsonContent);
                        File.Delete(tempPath); // Xóa file tạm
                        Console.WriteLine($"Processed JSON file with {people.Count} people.");
                    }
                    else
                    {
                        throw new Exception("Unknown command.");
                    }

                    // Gửi lại danh sách cho client
                    byte[] responseBytes = JsonSerializer.SerializeToUtf8Bytes(people);
                    byte[] responseLengthBytes = BitConverter.GetBytes(responseBytes.Length);
                    stream.Write(responseLengthBytes, 0, responseLengthBytes.Length);
                    stream.Write(responseBytes, 0, responseBytes.Length);

                    Console.WriteLine("List sent back to client.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
                try
                {
                    if (client.Connected)
                    {
                        using NetworkStream stream = client.GetStream();
                        string errorMsg = $"Error: {ex.Message}";
                        byte[] errorBytes = Encoding.UTF8.GetBytes(errorMsg);
                        byte[] errorLengthBytes = BitConverter.GetBytes(errorBytes.Length);
                        stream.Write(errorLengthBytes, 0, errorLengthBytes.Length);
                        stream.Write(errorBytes, 0, errorBytes.Length);
                    }
                }
                catch { /* Bỏ qua lỗi khi gửi thông báo */ }
            }
        }

        private static byte[] ReadExact(NetworkStream stream, int count)
        {
            byte[] buffer = new byte[count];
            int offset = 0;
            while (offset < count)
            {
                int read = stream.Read(buffer, offset, count - offset);
                if (read == 0)
                    throw new Exception("Connection closed while reading data.");
                offset += read;
            }
            return buffer;
        }
    }
}
