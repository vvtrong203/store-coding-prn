using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public double GPA { get; set; }

        // Parameterless constructor required by EF
        public Student() { }

        // Parameterized constructor for your custom use
        public Student(int studentId, string firstName, string lastName, int age, string address, double gpa)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Address = address;
            GPA = gpa;
        }
    }


}
