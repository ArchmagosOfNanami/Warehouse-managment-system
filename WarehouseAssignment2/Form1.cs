using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WarehouseAssignment2
{
    public partial class Form1 : Form
    {
        static int i = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void Login_Click(object sender, EventArgs e)
        {
            if (EmailtextBox.Text == "" || PasswordtextBox.Text == "")
            {
                MessageBox.Show("Please Enter valid Email or Password");
                return;
            }

            // Prepare fields for API call
            var fields = new Dictionary<string, string>
            {
                { "action", "admin_login" },          // must match api2.php case
                { "admin_email", EmailtextBox.Text }, // must match $_POST keys in api2.php
                { "admin_pass", PasswordtextBox.Text }
            };

            try
            {
                // Call the API
                var result = await ApiHelper.Post<LoginResponse>(fields);

                if (result.Success == 1)
                {
                    Myglob.email = result.Email;
                    Myglob.admin = result.Admin;

                    MessageBox.Show("Login successful");

                    this.Hide();
                    MainForm m = new MainForm();
                    m.ShowDialog();
                }
                else
                {
                    MessageBox.Show(result.Message ?? "Invalid login");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to API: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration r = new Registration();
            r.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                PasswordtextBox.UseSystemPasswordChar = false;
            else
                PasswordtextBox.UseSystemPasswordChar = true;
        }
    }

    public static class Myglob
    {
        public static int admin = 0;
        public static string email = "";
    }

    // Response model matching api2.php JSON
    public class LoginResponse
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public int Admin { get; set; }
    }
}
