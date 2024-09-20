using WinFormsApp1.Repositories;
using System.Data.OleDb;
using System.Data;
using WinFormsApp1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        #region Fields
        private BindingSource bindingSource = new();
        #endregion

        #region Ctor
        public Form1()
        {
            InitializeComponent();

        }
        #endregion

        #region Event Handlers
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Visible = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;  // Adjusts width to fill available space
            }

            RefreshDataGridView();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 form = new();
            form.ShowDialog(this);

            RefreshDataGridView();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                {

                    var obj = dataGridView1.Rows[e.RowIndex].DataBoundItem;
                    Student? selectedStudent = UnitOfWork.StudentRepository.Get().FirstOrDefault(s => s.ID == (int)obj.GetType().GetProperty("ID").GetValue(obj));
                    if (selectedStudent == null)
                    {
                        MessageBox.Show("Invalid selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Form2 form = new(selectedStudent);
                        form.ShowDialog(this);
                        RefreshDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region Private Methods
        public void RefreshDataGridView(IEnumerable<Student>? students = default)
        {
            students ??= UnitOfWork.StudentRepository.Get() ?? [];
            dataGridView1.DataSource = students.Select(s => new StudentModel(s.ID, s.Name, s.Section, s.Course)).ToArray();
        }

        #endregion


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.ToLower();

            var allStudents = UnitOfWork.StudentRepository.Get();

            var filteredStudents = allStudents
                .Where(s => s.Name.ToLower().Contains(searchTerm) ||
                            s.Section.ToLower().Contains(searchTerm) ||
                            s.Course.ToLower().Contains(searchTerm))
                .Select(s => new StudentModel(s.ID, s.Name, s.Section, s.Course))
                .ToArray();

            dataGridView1.DataSource = filteredStudents;

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var selectedStudentModel = (StudentModel)selectedRow.DataBoundItem;

                var result = MessageBox.Show($"Are you sure you want to delete {selectedStudentModel.Name}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    UnitOfWork.StudentRepository.Delete(selectedStudentModel.ID);

                    RefreshDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        record StudentModel(int ID, string Name, string Section, string Course);

    }
}
