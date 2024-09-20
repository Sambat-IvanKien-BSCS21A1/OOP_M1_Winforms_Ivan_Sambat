namespace WinFormsApp1.Repositories
{
    public class Student
    {
        public string? Name => $"{FirstName} {LastName}";
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Section { get; set; }
        public string? Course { get; set; }
        public int ID { get; set; }

    }
}
