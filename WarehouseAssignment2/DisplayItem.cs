using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WarehouseAssignment2
{
    public partial class DisplayItem : Form
    {
        public DisplayItem()
        {
            InitializeComponent();
        }

        private async void DisplayItem_Load(object sender, EventArgs e)
        {
            try
            {
                // Prepare API call
                var fields = new Dictionary<string, string>
                {
                    { "action", "list_products" }
                };

                // Call API
                var result = await ApiHelper.Post<List<Product>>(fields);

                if (result != null && result.Count > 0)
                {
                    // Convert list of products to DataTable for binding
                    DataTable tb = new DataTable();
                    tb.Columns.Add("pdt_id");
                    tb.Columns.Add("pdt_name");
                    tb.Columns.Add("pdt_price");
                    tb.Columns.Add("pdt_des");
                    tb.Columns.Add("pdt_ctg");
                    tb.Columns.Add("pdt_img");
                    tb.Columns.Add("product_stock");
                    tb.Columns.Add("pdt_status");

                    foreach (var item in result)
                    {
                        tb.Rows.Add(item.pdt_id, item.pdt_name, item.pdt_price,
                                    item.pdt_des, item.pdt_ctg, item.pdt_img,
                                    item.product_stock, item.pdt_status);
                    }

                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.DataSource = tb;
                }
                else
                {
                    MessageBox.Show("No products found or API error.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to API: " + ex.Message);
            }
        }

        Bitmap bit;

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // preserved event
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int height = dataGridView2.Height;
            dataGridView2.Height = dataGridView2.RowCount * dataGridView2.RowTemplate.Height * 2;
            bit = new Bitmap(dataGridView2.Width, dataGridView2.Height);
            dataGridView2.DrawToBitmap(bit, new Rectangle(0, 0, dataGridView2.Width, dataGridView2.Height));
            e.Graphics.DrawImage(bit, 0, 0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }

    // Product model matching products table
    public class Product
    {
        public string pdt_id { get; set; }
        public string pdt_name { get; set; }
        public string pdt_price { get; set; }
        public string pdt_des { get; set; }
        public string pdt_ctg { get; set; }
        public string pdt_img { get; set; }
        public string product_stock { get; set; }
        public string pdt_status { get; set; }
    }
}
