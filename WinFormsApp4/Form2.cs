using WinFormsApp1.Repositories;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        int? id = null;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Student student) : this()
        {
            id = student.ID;
            txtFirstName.Text = student.FirstName;
            txtLastName.Text = student.LastName;
            txtSection.Text = student.Section;
            txtCourse.Text = student.Course;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
         
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                UnitOfWork.StudentRepository.Update(
                   id: id.Value,
                   firstName: txtFirstName.Text,
                   lastName: txtLastName.Text,
                   course: txtCourse.Text,
                   section: txtSection.Text);
            }
            else
            {
               UnitOfWork.StudentRepository.Add(
                    firstName: txtFirstName.Text,
                   lastName: txtLastName.Text,
                   course: txtCourse.Text,
                   section: txtSection.Text);
            }
            this.Close();
        }
    }
}
