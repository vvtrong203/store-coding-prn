using Question2.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Question2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        VuVanTrongMyStockContext context = new VuVanTrongMyStockContext();

        public void LoadCarList()
        {
            lvCars.ItemsSource = context.Cars.ToList();
            lvCars.Items.Refresh();
        }

        public Car GetCarObject()
        {
            Car car = null;
            try
            {
                car = new Car()
                {
                    CarId = int.Parse(txtCarId.Text),
                    CarName = txtCarName.Text,
                    Manufacturer = txtManufacturer.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    ReleasedYear = int.Parse(txtReleasedYear.Text)
                };
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Get car");
            }
            return car;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadCarList();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load car list");
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int carId = int.Parse(txtCarId.Text);

                Car carCheck = GetCarById(carId);

                if (carCheck == null)
                {
                    Car newCar = GetCarObject();
                    context.Cars.Add(newCar);
                    context.SaveChanges();
                    LoadCarList();

                    MessageBox.Show($"{newCar.CarName} inserted successfully", "Insert car"); // Display success message
                }
                else
                {
                    throw new Exception($"A car with ID {carId} already exists.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert car"); // Handle and display any exceptions that occur
            }
        }


        public Car GetCarById(int carID)
        {
            Car car = null;
            try
            {
/*                car = context.Cars.SingleOrDefault(car => car.CarId == carID);
*/                car = context.Cars.FirstOrDefault(car => car.CarId == carID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int carIdToUpdate = GetCarObject().CarId;
                Car existingCar = GetCarById(carIdToUpdate);

                if (existingCar != null)
                {
                    existingCar.CarName = txtCarName.Text;
                    existingCar.Manufacturer = txtManufacturer.Text;
                    existingCar.Price = decimal.Parse(txtPrice.Text);
                    existingCar.ReleasedYear = int.Parse(txtReleasedYear.Text);

                    context.Cars.Update(existingCar);
                    context.SaveChanges();
                    LoadCarList();
                    MessageBox.Show($"{existingCar.CarName} updated successfully.", "Update car");
                }
                else
                {
                    throw new Exception("The car does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update car");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int carIdToDelete = GetCarObject().CarId;
                Car existingCar = GetCarById(carIdToDelete);

                if (existingCar != null)
                {
                    context.Cars.Remove(existingCar);
                    context.SaveChanges();
                    LoadCarList();
                    MessageBox.Show($"{existingCar.CarName} deleted successfully.", "Delete car");
                }
                else
                {
                    throw new Exception("The car does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete car");
            }
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}