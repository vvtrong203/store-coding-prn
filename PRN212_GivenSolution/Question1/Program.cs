

using Question1;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Requirement 1:");
        Student s = new Student(1, "Nguyen Van A", new DateTime(1999, 10, 20), "SE");
        Console.WriteLine("You have entered:");
        Console.WriteLine(s);

        Console.WriteLine(Environment.NewLine + "---------------");
        Console.WriteLine("Requirement 2:");
        Group<Student> group = new Group<Student>("SE1824");
        group.Add(new Student(2, "Nguyen Van B", new DateTime(1999, 10, 20), "SE"));
        group.Add(new Student(3, "Nguyen Van C", new DateTime(1989, 11, 15), "IA"));
        group.Add(new Student(4, "Nguyen Van D", new DateTime(2000, 4, 2), "GD"));
        group.Show(DisplaysFullInfoOfStudent);

        Console.WriteLine(Environment.NewLine + "---------------");
        Console.WriteLine("Requirement 3:");
        Student removeStudent = new Student(3, "Nguyen Van C", new DateTime(1989, 11, 15), "IA");
        group.Remove(removeStudent);
        group.Show(DisplaysFullInfoOfStudent);

        Console.WriteLine(Environment.NewLine + "---------------");
        Console.WriteLine("Requirement 4:");
        group.Show(DisplaysBriefInfoOfStudent);
    }

    private static void DisplaysFullInfoOfStudent(Student student)
    {
        Console.WriteLine($"{student.Id} - {student.Name} - {student.Dob.ToLongDateString()} - {student.Major}");
    }

    private static void DisplaysBriefInfoOfStudent(Student student)
    {
        Console.WriteLine($"{student.Id} - {student.Name}");
    }
}