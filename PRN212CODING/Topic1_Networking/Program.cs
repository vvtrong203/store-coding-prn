using System;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

Uri info = new Uri("http://www.domain.com:80/info?id=123#fragment");
Uri page = new Uri("http://www.domain.com/info/page.html");

Console.WriteLine($"Host: {info.Host}");               // www.domain.com
Console.WriteLine($"Port: {info.Port}");               // 80
Console.WriteLine($"PathAndQuery: {info.PathAndQuery}"); // /info?id=123
Console.WriteLine($"Query: {info.Query}");             // ?id=123
Console.WriteLine($"Fragment: {info.Fragment}");       // #fragment
Console.WriteLine($"Default HTTP port: {page.Port}");  // 80
Console.WriteLine($"IsBaseOf: {info.IsBaseOf(page)}"); // Kiểm tra info có phải là base của page không (trả về False)
Uri relative = info.MakeRelativeUri(page);
Console.WriteLine($"IsAbsoluteUri: {relative.IsAbsoluteUri}"); // False (vì đây là uri tương đối)
Console.WriteLine($"RelativeUri: {relative.ToString()}");      // ../info/page.html
Console.ReadLine();
// MakeRelativeUri tạo ra URI tương đối từ info đến page.



// Demo URL
Uri url = new Uri("https://www.example.com/products/item?id=567");
Console.WriteLine("=== URL Demo ===");
Console.WriteLine($"AbsoluteUri: {url.AbsoluteUri}");
Console.WriteLine($"Host: {url.Host}");
Console.WriteLine($"Scheme (protocol): {url.Scheme}");
Console.WriteLine($"PathAndQuery: {url.PathAndQuery}");
Console.WriteLine();

// Demo URN (C# không có Uri trực tiếp hỗ trợ URN theo chuẩn, nhưng ta có thể parse)
Uri urn = new Uri("urn:isbn:978-3-16-148410-0");
Console.WriteLine("=== URN Demo ===");
Console.WriteLine($"Original URN: {urn.OriginalString}");
Console.WriteLine($"Scheme: {urn.Scheme}");  // urn
Console.WriteLine($"Path (identifier): {urn.AbsolutePath}");
Console.WriteLine();

// Demo URI (tổng quát)
Uri uri1 = new Uri("https://www.google.com/search?q=uri");
Uri uri2 = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174000");

Console.WriteLine("=== URI (URL Example) ===");
Console.WriteLine($"IsAbsoluteUri: {uri1.IsAbsoluteUri}");
Console.WriteLine($"URI: {uri1.AbsoluteUri}");
Console.WriteLine();

Console.WriteLine("=== URI (URN Example) ===");
Console.WriteLine($"IsAbsoluteUri: {uri2.IsAbsoluteUri}");
Console.WriteLine($"URI: {uri2.OriginalString}");
Console.WriteLine();

// Kết hợp demo Relative Uri
Uri baseUri = new Uri("https://www.example.com/folder/");
Uri targetUri = new Uri("https://www.example.com/folder/page.html");
Uri relativeUri = baseUri.MakeRelativeUri(targetUri);

Console.WriteLine("=== Relative Uri Demo ===");
Console.WriteLine($"Relative Uri: {relativeUri}");
Console.WriteLine($"Is Absolute Uri? {relativeUri.IsAbsoluteUri}");

Console.ReadLine();



//=== URL Demo ===
//AbsoluteUri: https://www.example.com/products/item?id=567
//Host: www.example.com
//Scheme(protocol): https
//PathAndQuery: / products / item ? id = 567

//=== URN Demo ===
//Original URN: urn: isbn: 978 - 3 - 16 - 148410 - 0
//Scheme: urn
//Path(identifier): isbn: 978 - 3 - 16 - 148410 - 0

//=== URI(URL Example) ===
//IsAbsoluteUri: True
//URI: https://www.google.com/search?q=uri

//=== URI(URN Example) ===
//IsAbsoluteUri: True
//URI: urn: uuid: 123e4567 - e89b - 12d3 - a456 - 426614174000

//=== Relative Uri Demo ===
//Relative Uri: page.html
//Is Absolute Uri? False




//Create a request for the URL
WebRequest request = WebRequest.Create("https://www.w3schools.com/django/index.php");
//If required by the server, set the credentials.
request.Credentials = CredentialCache.DefaultCredentials;
//Get the response
HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//Display the status
Console.WriteLine($"Status: " + response.StatusDescription);
Console.WriteLine(new String('*', 50));
//Get the stream containing content returned bu the server.
Stream dataStream = response.GetResponseStream();
// Open the stream using a StreamReader for easy access.
StreamReader reader = new StreamReader(dataStream);
//Read the content
string responseFromServer = reader.ReadToEnd();
//Display the context
Console.WriteLine(responseFromServer);
Console.WriteLine(new String('*', 50));
//Cleanup the streams and the response.
reader.Close();
dataStream.Close();
response.Close();




Console.WriteLine("=== WebRequest & WebResponse (GET Request) Demo ===");
WebRequestGetDemo();

Console.WriteLine("\n=== WebRequest & WebResponse (POST Request) Demo ===");
WebRequestPostDemo();

Console.ReadLine();


static void WebRequestGetDemo()
{
    try
    {
        // Create a request for the URL
        WebRequest request = WebRequest.Create("https://www.w3schools.com/django/index.php");
        // Set default credentials (if required)
        request.Credentials = CredentialCache.DefaultCredentials;

        // Get the response
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            Console.WriteLine($"Status: {response.StatusDescription}");
            Console.WriteLine(new String('*', 50));

            // Get stream containing content returned by server
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using StreamReader
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    // Read content
                    string responseFromServer = reader.ReadToEnd();
                    // Display content
                    Console.WriteLine(responseFromServer.Substring(0, 500)); // Hiển thị 500 ký tự đầu tiên
                    Console.WriteLine("\n... (Truncated Output) ...");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void WebRequestPostDemo()
{
    try
    {
        // Create a request to a testing service
        WebRequest request = WebRequest.Create("https://httpbin.org/post");
        request.Method = "POST";

        // Form data to send
        string postData = "name=JohnDoe&age=30";
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        // Set request properties
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;

        // Write data to request stream
        using (Stream dataStream = request.GetRequestStream())
        {
            dataStream.Write(byteArray, 0, byteArray.Length);
        }

        // Get the response
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            Console.WriteLine($"POST Status: {response.StatusDescription}");
            using (Stream dataStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(dataStream))
            {
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer.Substring(0, 500)); // Hiển thị 500 ký tự đầu tiên
                Console.WriteLine("\n... (Truncated Output) ...");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"POST Error: {ex.Message}");
    }
}

//WebRequest.Create(url)  Tạo một request đến URL
//request.Credentials	Cung cấp thông tin đăng nhập (nếu server yêu cầu)
//request.GetResponse()	Lấy phản hồi (response) từ server
//StreamReader + GetResponseStream()	Đọc dữ liệu nội dung từ phản hồi của server
//Method = "POST"	Chuyển request từ GET sang POST
//ContentType và ContentLength	Thiết lập kiểu dữ liệu gửi lên và độ dài dữ liệu khi POST
//request.GetRequestStream()	Ghi dữ liệu lên request khi gửi POST