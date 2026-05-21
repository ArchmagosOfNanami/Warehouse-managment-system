using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WarehouseAssignment2
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private async void Register_Click(object sender, EventArgs e)
        {
            if (PasswordtextBox.Text != CPasswordtextBox.Text)
            {
                MessageBox.Show("Password Dont match");
                return;
            }

            try
            {
                // Prepare fields for API call
                var fields = new Dictionary<string, string>
                {
                    { "action", "add_admin_user" },
                    { "user_name", EmailtextBox.Text },
                    { "user_password", PasswordtextBox.Text },
                    { "user_role", comboBox2.SelectedIndex.ToString() } 
                    // comboBox2.SelectedIndex: 0 or 1 depending on your UI
                };

                var result = await ApiHelper.Post<AddAdminResponse>(fields);

                if (!string.IsNullOrEmpty(result.Message))
                {
                    MessageBox.Show(result.Message);
                    this.Hide();
                    MainForm m = new MainForm();
                    m.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Failed to add admin user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to API: " + ex.Message);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            // keep original event
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                Register.Enabled = true;
            else
                Register.Enabled = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
                label9.Visible = true;
            else
                label9.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // keep original event
        }

        private void EmailtextBox_TextChanged(object sender, EventArgs e)
        {
            // keep original event
        }

        private void label7_Click(object sender, EventArgs e)
        {
            // keep original event
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // keep original event
        }

        private void label8_Click(object sender, EventArgs e)
        {
            // keep original event
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // keep original event
        }

        private void CPasswordtextBox_TextChanged(object sender, EventArgs e)
        {
            // keep original event
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            // keep original event
        }
    }

    // Response model for add_admin_user
    public class AddAdminResponse
    {
        public string Message { get; set; }
    }
}
