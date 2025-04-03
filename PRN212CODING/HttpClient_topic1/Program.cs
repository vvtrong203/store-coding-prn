using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HttpClient_topic1
{
    internal class Program
    {
        class Product
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
        }

        static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            string uri = "https://dummyjson.com/products";
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Print raw JSON response (optional)
                Console.WriteLine(responseBody);

                // Deserialize the JSON response into a list of products
                var productData = JsonSerializer.Deserialize<JsonElement>(responseBody);
                List<Product> products = new List<Product>();
                Console.WriteLine(productData);
                foreach (var item in productData.GetProperty("products").EnumerateArray())
                {
                    // Create and populate Product objects
                    products.Add(new Product
                    {
                        Id = item.GetProperty("id").GetInt32(),
                        Title = item.GetProperty("title").GetString(),
                        Description = item.GetProperty("description").GetString(),
                        Price = item.GetProperty("price").GetDecimal().ToString("F2") // Formatting price
                    });
                }

                // Output the list of products
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Title: {product.Title}, Price: {product.Price} \n");
                }
                Console.ReadLine();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n Exception Caught!");
                Console.WriteLine("Message: {0} ", ex.Message);
            }



            ///////////////////////////////////
            ///

            string uri = "http://www.contoso.com/";
            try
            {
                // Gửi request và chờ nhận phản hồi
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode(); // Kiểm tra mã trạng thái

                // Đọc nội dung HTML dưới dạng string
                string responseBody = await response.Content.ReadAsStringAsync();

                // In kết quả ra console (chỉ in 500 ký tự đầu tiên cho gọn)
                Console.WriteLine(responseBody.Substring(0, 500));
                Console.WriteLine("\n... (Truncated output) ...");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message: {0}", e.Message);
            }


        }
    }
}
