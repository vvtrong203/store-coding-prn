namespace Topic_2_APS_DOTNET_CORE_Empty.Model;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Dob { get; set; }

    public Student()
    {
    }

    public Student(int id, string name, DateTime dob)
    {
        Id = id;
        Name = name;
        Dob = dob; 
    }
}
