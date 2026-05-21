using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WarehouseAssignment2
{
    public partial class AddNew : Form
    {
        public AddNew()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Prepare fields for API call
                var fields = new Dictionary<string, string>
                {
                    { "action", "add_product" },
                    { "pdt_name", textBox1.Text },               // Name
                    { "pdt_ctg", comboBox1.SelectedValue?.ToString() ?? "1" }, // Category ID (must be numeric)
                    { "pdt_stock", textBox2.Text },              // Available
                    { "pdt_des", textBox4.Text },                // Company/Description
                    { "pdt_price", textBox3.Text },              // Price
                    { "pdt_status", "1" }                        // Published by default
                };

                var result = await ApiHelper.Post<AddProductResponse>(fields);

                if (!string.IsNullOrEmpty(result.Message))
                {
                    MessageBox.Show(result.Message);
                }
                else if (!string.IsNullOrEmpty(result.Error))
                {
                    MessageBox.Show("Error: " + result.Error);
                }
                else
                {
                    MessageBox.Show("Failed to add product. Sent values: " +
                                    $"Name={textBox1.Text}, Category={comboBox1.SelectedValue}, Stock={textBox2.Text}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to API: " + ex.Message);
            }
        }

        private void zzz(object sender, EventArgs e)
        {
            // Ideally refactor to call list_categories API to populate comboBox1
        }

        private void AddNew_Load(object sender, EventArgs e)
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Keep original event
        }
    }

    // Response model for add_product
    public class AddProductResponse
    {
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
