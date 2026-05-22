using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WarehouseAssignment2
{
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
        }

        private async void Category_Load(object sender, EventArgs e)
        {
            if (Myglob.admin != 1)
            {
                button1.Hide();
                button2.Hide();
                label1.Hide();
                textBox1.Hide();
            }

            try
            {
                var fields = new Dictionary<string, string>
                {
                    { "action", "list_categories" }
                };

                var rows = await ApiHelper.Post<List<Dictionary<string, object>>>(fields);

                if (rows != null && rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    foreach (var key in rows[0].Keys)
                        dt.Columns.Add(key);

                    foreach (var row in rows)
                    {
                        var dr = dt.NewRow();
                        foreach (var key in row.Keys)
                            dr[key] = row[key];
                        dt.Rows.Add(dr);
                    }

                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No categories found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fields = new Dictionary<string, string>
                {
                    { "action", "add_category" },
                    { "ctg_name", textBox1.Text },
                    { "ctg_des", "New category description" }, // add another textbox if needed
                    { "ctg_status", "1" }
                };

                var result = await ApiHelper.Post<Dictionary<string, string>>(fields);

                if (result.ContainsKey("message"))
                {
                    MessageBox.Show(result["message"]);
                }
                else
                {
                    MessageBox.Show("Add failed.");
                }

                Category_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding category: " + ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Select a category row to update.");
                    return;
                }

                var row = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                var fields = new Dictionary<string, string>
                {
                    { "action", "update_category" },
                    { "u_ctg_id", row["ctg_id"].ToString() },
                    { "u_ctg_name", row["ctg_name"].ToString() },
                    { "u_ctg_des", row["ctg_des"].ToString() },
                    { "u_ctg_status", row["ctg_status"].ToString() }
                };

                var result = await ApiHelper.Post<Dictionary<string, string>>(fields);

                if (result.ContainsKey("message"))
                {
                    MessageBox.Show(result["message"]);
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }

                Category_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating category: " + ex.Message);
            }
        }
    }
}
