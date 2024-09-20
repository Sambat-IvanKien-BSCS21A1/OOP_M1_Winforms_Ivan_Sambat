namespace WinFormsApp1.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ICollection<Student> _students;

        public StudentRepository()
        {
            _students = [];
        }
        public void Add(string firstName, string lastName, string section, string course)
        {
            _students.Add(new Student { FirstName = firstName, LastName = lastName, Course = course, Section = section, ID = _students.Count + 1 });
        }

        public IEnumerable<Student> Get() => [.. _students];

        public void Update(int id, string firstName, string lastName, string section, string course)
        {
            var student = _students.FirstOrDefault(s => s.ID == id);
            if (student != null)
            {
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Section = section;
                student.Course = course;
            }
        }

        public void Delete(int studentId)
        {
            var studentToRemove = _students.FirstOrDefault(s => s.ID == studentId);
            if (studentToRemove != null)
            {
                _students.Remove(studentToRemove);
            }
        }
    }
}