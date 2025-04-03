using System;

namespace Question1
{
    public delegate void Presentation<T>(T item);

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Major { get; set; }

        public Student(int id, string name, DateTime dob, string major)
        {
            Id = id;
            Name = name;
            Dob = dob;
            Major = major;
        }

        public override string ToString()
        {
            string date = $"{Dob.Month}/{Dob.Day}/{Dob.Year}";
            return $"Student: {Id} - {Name} - {date} - {Major}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Student other)
            {
                return Id == other.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
