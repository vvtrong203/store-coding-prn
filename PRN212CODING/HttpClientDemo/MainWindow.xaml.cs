using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace HttpClientDemo
{
    public partial class MainWindow : Window
    {
        static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string uri = txtUrl.Text.Trim();
                // Lấy dữ liệu từ URL dưới dạng chuỗi
                string responseBody = await client.GetStringAsync(uri);
                txtContent.Text = responseBody.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
