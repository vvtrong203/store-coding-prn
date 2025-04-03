using System;
using System.Linq;

namespace Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                var students = context.Students.ToList();

                Console.WriteLine($"ID\t|\tName\t|\tAge\t|\tAddress\t|\tGPA|");

                foreach (var student in students)
                {
                    Console.WriteLine($"{student.StudentId} \t|\t {student.FirstName} {student.LastName} \t|\t {student.Age} \t|\t {student.Address} \t|\t {student.GPA}|");
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
