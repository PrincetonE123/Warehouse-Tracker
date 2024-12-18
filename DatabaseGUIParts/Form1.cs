using Accessibility;
using System.IO;

namespace DatabaseGUIParts
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=NEURALYNX;Initial Catalog=WHMngmntSystem;Integrated Security=True;";
        private char[] whitelist = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@%23456789".ToCharArray();

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        public void login()
        {
            string enteredUsername = textBox1.Text;
            string enteredPassword = textBox2.Text;
            SharedData.currentUser = enteredUsername; // Stores the username in the global variable after successful authentication

            // Create an instance of EmployeeDataDAO
            EmployeeDataDAO employeeDataDAO = new EmployeeDataDAO();

            if (enteredPassword.ToCharArray().All(x => enteredPassword.ToCharArray().Count(y => y == x) <= whitelist.Count(y => y == x))) {
                label5.Show(); }
            else {
                // Check if the username and password are valid
                if (employeeDataDAO.ValidateUserCredentials(enteredUsername, enteredPassword))
                {
                    enteredPassword = "";
                    // Proceed to the next action, e.g., open Form2
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide(); // Optionally hide Form1
                }
                else
                {
                    label5.Show();
                }
            }
        } //end of login()

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
