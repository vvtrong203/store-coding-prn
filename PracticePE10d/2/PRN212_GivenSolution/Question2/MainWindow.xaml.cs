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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Question2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        VuVanTrongAssignment01RoomsManagementContext context = new VuVanTrongAssignment01RoomsManagementContext();
        public void LoadRoomList()
        {
            lvRooms.ItemsSource = context.Rooms.ToList();
            lvRooms.Items.Refresh();
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadRoomList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load car list");
            }
        }
        public Room GetRoomObject()
        {
            Room room = null;
            try
            {
                room = new Room ()
                {
                    RoomId = int.Parse(txtRoomId.Text),
                    RoomNumber = txtRoomNumber.Text,
                    RoomType = txtRoomType.Text,
                    Capacity = int.Parse(txtCapacity.Text),
                    IsAvailable = chkIsAvailable.IsChecked ?? false
                    //Toán tử ?? là toán tử null-coalescing. Nó trả về toán hạng bên trái nếu toán hạng đó không phải là null; ngược lại, nó trả về toán hạng bên phải.
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get room");
            }
            return room;
        }

        public Room GetRoomObjectNoID()
        {
            Room room = null;
            try
            {
                room = new Room()
                {
                    RoomNumber = txtRoomNumber.Text,
                    RoomType = txtRoomType.Text,
                    Capacity = int.Parse(txtCapacity.Text),
                    IsAvailable = chkIsAvailable.IsChecked ?? false
                    //Toán tử ?? là toán tử null-coalescing. Nó trả về toán hạng bên trái nếu toán hạng đó không phải là null; ngược lại, nó trả về toán hạng bên phải.
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get room");
            }
            return room;
        }

        /*        public Room GetRoomByID(int roomID)
                {
                    Room room = null;
                    try
                    {
                        var myStockDB = new VuVanTrongAssignment01RoomsManagementContext();
                        room = myStockDB.Rooms.SingleOrDefault(room => room.RoomId == roomID);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return room;
                }*/

        public bool RoomIDExists(int roomID)
        {
            try
            {
                using (var myStockDB = new VuVanTrongAssignment01RoomsManagementContext())
                {
                    return myStockDB.Rooms.Any(r => r.RoomId == roomID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking the room ID.", ex);
            }
        }


        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    Room room = GetRoomObjectNoID();
                    context.Rooms.Add(room);
                    context.SaveChanges();
                    LoadRoomList();
                    MessageBox.Show($"{room.RoomNumber} inserted successfully.", "Insert room");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert room");
            }
        }

        public Room GetRoomByID(int roomID)
        {
            Room room = null;
            try
            {
                using (var myStockDB = new VuVanTrongAssignment01RoomsManagementContext())
                {
                    room = myStockDB.Rooms.SingleOrDefault(r => r.RoomId == roomID);
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                throw new Exception("An error occurred while retrieving the room by ID.", ex);
            }
            return room;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room room = GetRoomObject();
                Room roomToUpdate = context.Rooms.FirstOrDefault(e => e.RoomId == Convert.ToInt32(txtRoomId.Text));
                roomToUpdate.RoomNumber = txtRoomNumber.Text;
                roomToUpdate.RoomType = txtRoomType.Text;
                roomToUpdate.Capacity = int.Parse(txtCapacity.Text);
                roomToUpdate.IsAvailable = chkIsAvailable.IsChecked ?? false;

                if (RoomIDExists(room.RoomId))
                {
                    context.Rooms.Update(roomToUpdate);
                    context.SaveChanges();
                    LoadRoomList();
                    MessageBox.Show($"{room.RoomNumber} updated successfully.", "Update room");
                } else
                {
                    throw new Exception("The room already exists.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update room");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
/*            Employee employee = context.Employees.FirstOrDefault(e => e.Id == Convert.ToInt32(txtId.Text));
            context.Employees.Remove(employee);
            context.SaveChanges();
            LoadData();*/
            Room room = context.Rooms.FirstOrDefault(e => e.RoomId == Convert.ToInt32(txtRoomId.Text));
            if (room != null) { 
                context.Rooms.Remove(room);
                context.SaveChanges();
                LoadRoomList();
            }
        }


 /*       private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSortBy.SelectedItem != null)
            {
                string sortBy = cmbSortBy.SelectedItem.ToString();

                // Thực hiện các thao tác sắp xếp dữ liệu dựa trên giá trị đã chọn
                switch (sortBy)
                {
                    case "Room Number":
                        // Sắp xếp theo số phòng
                        break;
                    case "Room Type":
                        // Sắp xếp theo loại phòng
                        break;
                    case "Capacity":
                        // Sắp xếp theo sức chứa
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a sorting option.", "Sort");
            }
        }
*/


        /*private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rooms = roomRepository.GetRooms().ToList();

                bool sortDescending = chkSortDescending.IsChecked ?? false;
                bool sortAscending = chkSortAscending.IsChecked ?? false;

                if (sortDescending)
                {
                    switch (cmbSortBy.SelectedItem as string)
                    {
                        case "Room Number":
                            rooms = rooms.OrderByDescending(r => r.RoomNumber).ToList();
                            break;
                        case "Room Type":
                            rooms = rooms.OrderByDescending(r => r.RoomType).ToList();
                            break;
                        case "Capacity":
                            rooms = rooms.OrderByDescending(r => r.Capacity).ToList();
                            break;
                        default:
                            rooms = rooms.OrderByDescending(r => r.RoomNumber).ToList();
                            break;
                    }
                }
                else if (sortAscending)
                {
                    switch (cmbSortBy.SelectedItem as string)
                    {
                        case "Room Number":
                            rooms = rooms.OrderBy(r => r.RoomNumber).ToList();
                            break;
                        case "Room Type":
                            rooms = rooms.OrderBy(r => r.RoomType).ToList();
                            break;
                        case "Capacity":
                            rooms = rooms.OrderBy(r => r.Capacity).ToList();
                            break;
                        default:
                            rooms = rooms.OrderBy(r => r.RoomNumber).ToList();
                            break;
                    }
                }
                else
                {
                    switch (cmbSortBy.SelectedItem as string)
                    {
                        case "Room Number":
                            rooms = rooms.OrderBy(r => r.RoomNumber).ToList();
                            break;
                        case "Room Type":
                            rooms = rooms.OrderBy(r => r.RoomType).ToList();
                            break;
                        case "Capacity":
                            rooms = rooms.OrderBy(r => r.Capacity).ToList();
                            break;
                        default:
                            rooms = rooms.OrderBy(r => r.RoomNumber).ToList();
                            break;
                    }
                }

                lvRooms.ItemsSource = rooms;
                MessageBox.Show($"Rooms sorted by {cmbSortBy.SelectedItem} {(sortDescending ? "descending" : "ascending")}.", "Sort Rooms");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sorting rooms: {ex.Message}", "Sort Rooms Error");
            }
        }*/


        private void chkSortDescending_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkSortAscending_Checked(object sender, RoutedEventArgs e)
        {

        }
        public void Sort(bool check, int x)
        {
            if (check && x==0)
            {
                var data = context.Rooms.OrderByDescending(r => r.RoomNumber).ToList();
                lvRooms.ItemsSource = data;
            }
            else if (!check && x == 0)
            {
                var data = context.Rooms.OrderBy(r => r.RoomNumber).ToList();
                lvRooms.ItemsSource = data;
            }

        }
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            bool check;
            if(chkSortDescending.IsChecked == true)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            if(cmbSortBy.SelectedIndex == 0)
            {
                Sort(check, 0);
            }
        }

/*        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSortBy.SelectedIndex == 0)
            {
                // Sắp xếp theo Room Number
                SortByRoomNumber();
            }
            else if (cmbSortBy.SelectedIndex == 1)
            {
                // Sắp xếp theo Room Type
                SortByRoomType();
            }
            else if (cmbSortBy.SelectedIndex == 2)
            {
                // Sắp xếp theo Capacity
                SortByCapacity();
            }
            else
            {
                MessageBox.Show("Please select a sorting option.", "Sort");
            }
        }

        private void SortByRoomNumber()
        {
            // Thực hiện logic sắp xếp theo Room Number
        }

        private void SortByRoomType()
        {
            // Thực hiện logic sắp xếp theo Room Type
        }

        private void SortByCapacity()
        {
            // Thực hiện logic sắp xếp theo Capacity
        }
*/

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var data = context.Rooms.Where(r => r.RoomNumber.Contains(txtRoomNumber.Text)).ToList();
            lvRooms.ItemsSource = data;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}


/*Trong ngữ cảnh của một phần tử `RadioButton` trong ứng dụng WPF, `Checked="RadioButton_Checked"` là một cách để gán sự kiện xảy ra khi `RadioButton` được chọn. Nó chỉ định phương thức xử lý sự kiện mà bạn đã định nghĩa trong mã code của bạn.

### Giải thích chi tiết:

- **`Checked`**: Đây là một thuộc tính của `RadioButton` trong XAML của WPF. Nó xác định hành động nào sẽ xảy ra khi `RadioButton` được chọn.

- **`"RadioButton_Checked"`**: Đây là tên của phương thức xử lý sự kiện mà bạn đã định nghĩa trong mã code của bạn. Ví dụ:

  ```xml
  < RadioButton Content = "Option 1" Checked = "RadioButton_Checked" />
  ```

  Khi người dùng chọn `RadioButton` này, sự kiện `Checked` sẽ kích hoạt và gọi đến phương thức `RadioButton_Checked` trong code-behind của bạn.

### Ví dụ cụ thể:

Trong file XAML (`MainWindow.xaml`):

```xml
< RadioButton Content = "Option 1" Checked = "RadioButton_Checked" />
```

Trong file code-behind (`MainWindow.xaml.cs`):

```csharp
private void RadioButton_Checked(object sender, RoutedEventArgs e)
{
    // Xử lý khi RadioButton được chọn
    RadioButton radioButton = sender as RadioButton;
    if (radioButton != null && radioButton.IsChecked == true)
    {
        MessageBox.Show($"RadioButton {radioButton.Content} is checked.");
    }
}
```

Trong ví dụ này:

-Khi `RadioButton` có `Content` là "Option 1" được chọn, sự kiện `Checked` sẽ kích hoạt và gọi đến phương thức `RadioButton_Checked`.
- Trong phương thức `RadioButton_Checked`, bạn có thể thực hiện bất kỳ logic xử lý nào dựa trên việc `RadioButton` được chọn.

Điều này cho phép bạn tạo các hành động xử lý tùy*/ chỉnh khi người dùng tương tác với các `RadioButton` trên giao diện người dùng của bạn.