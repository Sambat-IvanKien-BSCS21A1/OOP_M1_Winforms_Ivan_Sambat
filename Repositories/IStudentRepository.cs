namespace WinFormsApp1.Repositories
{
    public interface IStudentRepository
    {
        void Add(string firstName, string lastName, string section, string course);
        void Update(int id, string firstName, string lastName, string section, string course);
        IEnumerable<Student> Get();
        void Delete(int iD);
    }

}
