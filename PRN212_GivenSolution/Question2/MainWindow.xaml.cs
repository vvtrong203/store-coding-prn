using Microsoft.VisualBasic.ApplicationServices;
using Question2.Models;
using System.ComponentModel;
using System.IO;
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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Question2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            cbuser.SelectedIndex = 0;
        }
        PePrn24sumB1Context context = new PePrn24sumB1Context();



        public void LoadData()
        {
            var dataUser = context.Users.ToList();
            cbuser.ItemsSource = dataUser;

            /*        SelectedValuePath: This property sets the path to the value on the bound data source that you want to use as the SelectedValue of the ComboBox. In this case, it is set to UserId.This means that when a user selects an item in the ComboBox, the SelectedValue property of the ComboBox will return the UserId of the selected item.
                    DisplayMemberPath: This property sets the path to the value on the bound data source that you want to display in the ComboBox. Here, it is set to Username.This means that the ComboBox will display the Username of each item in the list.
            */

           var dataIn = context.Instructors.ToList();
           cbin.ItemsSource = dataIn;

        }
        public void ReLoad(int x)
        {
            var data = context.Enrollments
                .Select(e => new
                {
                    CourseId = e.CourseId,
                    Title = e.Course.Title,
                    Description = e.Course.Description,
                    InstructorId = e.Course.InstructorId,
                    InstructorName = e.Course.Instructor.Name,
                    UserId = e.UserId,
                })
                .Where(e => e.UserId == x)
                .ToList();
            dgCourse.ItemsSource = data;
        }

        private void cbuser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = (int) cbuser.SelectedValue;
            ReLoad(index);
        }

        private void dgCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCourse.SelectedItem == null)
            {
                return;
            }
            dynamic checkCourse = dgCourse.SelectedItem;
            cbin.SelectedValue = checkCourse.InstructorId;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourseId.Text))
            {
/*                MessageBox.Show("Course ID cannot be empty.");
*/                return;
            }

            int courseId = int.Parse(txtCourseId.Text);
            var course = context.Courses.FirstOrDefault(course => course.CourseId == courseId);

            if (course != null)
            {
                course.Title = txtCourseTitle.Text;
                course.Description = txtCourseDescription.Text;
                course.InstructorId = (int) cbin.SelectedValue;

                context.Courses.Update(course);
                context.SaveChanges();


                cbin.SelectedValue = -1;
                int index = (int)cbuser.SelectedValue;
                ReLoad(index);
            }
            else
            {
                MessageBox.Show("Course not found!");
            }
        }


    }
}