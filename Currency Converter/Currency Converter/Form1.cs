using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Currency_Converter
{
    public partial class Form1 : Form
    {
        private Dictionary<string, double> _currencies = new Dictionary<string, double>();

        public Form1()
        {
            InitializeComponent();

            var currencies = GetLatestCurrencies();
            dynamic responseObject = JsonConvert.DeserializeObject(currencies);

            foreach (var item in responseObject.data)
            {
                comboBox2.Items.Add(item.First.code.ToString());
                comboBox1.Items.Add(item.First.code.ToString());
                _currencies.Add(item.First.code.ToString(), item.First.value.Value);
            }
        }

        private string GetLatestCurrencies()
        {
            var client = new RestClient("https://api.currencyapi.com/v3/latest");

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "cur_live_9PuIHkKCoElijM8xRmKbGAkmyQAGm7nFN3jhG9DA");
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedItem == null || comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select both currencies");
                    return;
                }


                var selectedFromCurrency = comboBox2.SelectedItem.ToString();
                var selectedToCurrency = comboBox1.SelectedItem.ToString();
                var amount = double.Parse(textBox1.Text);

                var fromExRate = _currencies[selectedFromCurrency];

                var toExRate = _currencies[selectedToCurrency];

                var calculate = (amount / fromExRate) * toExRate;

                label3.Text = $"RESULT: {calculate:F2} {selectedToCurrency}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during conversion: {ex.Message}");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            label3.Text = "RESULT:";
        }
    }
}