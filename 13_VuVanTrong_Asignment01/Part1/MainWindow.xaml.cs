using Part1.Models;
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

namespace Part1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        VuVanTrongAssignment01RoomsManagementContext context = new VuVanTrongAssignment01RoomsManagementContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void loadRoom()
        {
            var dataRoom = context.Rooms.ToList();
            lvRooms.ItemsSource = dataRoom;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            loadRoom();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Room roomInsert = new Room() {
                RoomNumber = txtRoomNumber.Text,
                RoomType = txtRoomType.Text,
                Capacity = int.Parse(txtCapacity.Text),
                IsAvailable = chkIsAvailable.IsChecked ?? false
            };

            context.Rooms.Add(roomInsert);
            context.SaveChanges();
            loadRoom();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int roomIdUpdate = int.Parse(txtRoomId.Text);
            Room roomToUpdateExist = context.Rooms.FirstOrDefault(room => room.RoomId.Equals(roomIdUpdate));
            if (roomToUpdateExist != null )
            {
                roomToUpdateExist.RoomNumber = txtRoomNumber.Text;
                roomToUpdateExist.RoomType = txtRoomType.Text;
                roomToUpdateExist.Capacity = int.Parse(txtCapacity.Text);
                roomToUpdateExist.IsAvailable = chkIsAvailable.IsChecked ?? false;
                context.Rooms.Update(roomToUpdateExist);
                context.SaveChanges();
                loadRoom();
            } else
            {
                throw new Exception("Room not exist!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int roomIdDelete = int.Parse(txtRoomId.Text);
            Room roomToDelete = context.Rooms.Find(roomIdDelete);
            if (roomToDelete != null) { 
                context.Rooms.Remove(roomToDelete);
                context.SaveChanges();
                loadRoom();
            }
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rooms = context.Rooms.ToList();

                bool sortDescending = chkSortDescending.IsChecked ?? false;

                ComboBoxItem selectedSortByItem = cmbSortBy.SelectedItem as ComboBoxItem;
                string sortBy = selectedSortByItem?.Content.ToString();

                if (sortBy == null)
                    return;

                switch (sortBy)
                {
                    case "Room Number":
                        rooms = sortDescending ? rooms.OrderByDescending(r => r.RoomNumber).ToList() : rooms.OrderBy(r => r.RoomNumber).ToList();
                        break;
                    case "Room Type":
                        rooms = sortDescending ? rooms.OrderByDescending(r => r.RoomType).ToList() : rooms.OrderBy(r => r.RoomType).ToList();
                        break;
                    case "Capacity":
                        rooms = sortDescending ? rooms.OrderByDescending(r => r.Capacity).ToList() : rooms.OrderBy(r => r.Capacity).ToList();
                        break;
                    default:
                        rooms = sortDescending ? rooms.OrderByDescending(r => r.RoomNumber).ToList() : rooms.OrderBy(r => r.RoomNumber).ToList();
                        break;
                }

                lvRooms.ItemsSource = rooms;
                MessageBox.Show($"Rooms sorted by {sortBy} {(sortDescending ? "descending" : "ascending")}.", "Sort Rooms");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sorting rooms: {ex.Message}", "Sort Rooms Error");
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchRoomNumber = txtRoomNumber.Text;
                string searchRoomType = txtRoomType.Text;
                string searchCapacity = txtCapacity.Text;
                bool? searchIsAvailable = chkIsAvailable.IsChecked;

                if (!string.IsNullOrEmpty(searchRoomNumber))
                {
                    lvRooms.ItemsSource = context.Rooms.Where(r => r.RoomNumber.ToLower().Contains(searchRoomNumber.ToLower())).ToList();
                } else

                if (!string.IsNullOrEmpty(searchRoomType))
                {
                    lvRooms.ItemsSource = context.Rooms.Where(r => r.RoomType.ToLower().Contains(searchRoomType.ToLower())).ToList();
                }
                else

                if (int.TryParse(searchCapacity, out int capacity))
                {
                    lvRooms.ItemsSource = context.Rooms.Where(r => r.Capacity == capacity).ToList();
                }
                else

                if (searchIsAvailable.HasValue)
                {
                    lvRooms.ItemsSource = context.Rooms.Where(r => r.IsAvailable == searchIsAvailable.Value).ToList();
                }

                MessageBox.Show("Rooms searched.", "Search rooms");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search rooms");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}