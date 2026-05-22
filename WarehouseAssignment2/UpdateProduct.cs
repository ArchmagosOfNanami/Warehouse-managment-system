using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WarehouseAssignment2
{
    public partial class UpdateProduct : Form
    {
        public UpdateProduct()
        {
            InitializeComponent();
        }

        private async void UpdateProduct_Load(object sender, EventArgs e)
        {
            try
            {
                var fields = new Dictionary<string, string>
                {
                    { "action", "list_products" }
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
                    MessageBox.Show("No products found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Select a product row to update.");
                    return;
                }

                var row = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row;

                var fields = new Dictionary<string, string>
                {
                    { "action", "update_product" },
                    { "pdt_id", row["pdt_id"].ToString() },
                    { "pdt_name", row["pdt_name"].ToString() },
                    { "pdt_price", row["pdt_price"].ToString() },
                    { "pdt_des", row["pdt_des"].ToString() },
                    { "pdt_ctg", row["pdt_ctg"].ToString() },
                    { "pdt_stock", row["product_stock"].ToString() },
                    { "pdt_status", row["pdt_status"].ToString() }
                };

                var result = await ApiHelper.Post<Dictionary<string, string>>(fields);

                if (result.ContainsKey("message"))
                {
                    MessageBox.Show(result["message"]);
                }
                else if (result.ContainsKey("error"))
                {
                    MessageBox.Show("Error: " + result["error"]);
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
        }
    }
}
