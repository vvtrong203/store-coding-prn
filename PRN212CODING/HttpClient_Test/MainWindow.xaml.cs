using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace HttpClient_Test
{
    public class Posts
    {
        public int UserId { get; set; }
        public int Id { get; set; } // Changed to int
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public partial class MainWindow : Window
    {
        static readonly HttpClient _http = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var posts = await CallApi();
                lvPosts.ItemsSource = posts.ToList();
                lvPosts.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetUri()
        {
            if (string.IsNullOrWhiteSpace(txtURI.Text))
            {
                return "https://jsonplaceholder.typicode.com/posts";
            }

            return txtURI.Text.Trim();
        }

        private async Task<List<Posts>> CallApi()
        {
            string apiUrl = GetUri(); // Use the instance method to get the URI

            var response = await _http.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch data: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var posts = JsonSerializer.Deserialize<List<Posts>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return posts ?? new List<Posts>();
        }
    }
}
