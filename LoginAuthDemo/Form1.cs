using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infrastructure;

namespace LoginAuthDemo
{
    public partial class Form1 : Form
    {
        static HttpClient clientApi = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string passWord = textBox2.Text;
            var result = await loginFromAPI(userName, passWord);
            MessageBox.Show(result.isValid.ToString());
            if (result.isValid)
            {
                if (Properties.Settings.Default.token == null)
                {
                    MessageBox.Show("Failed login!");
                }
                else
                {
                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.token = result.token.ToString();
                    Properties.Settings.Default.Save();
                    verifyToken();
                }
            }
            
        }

        public Task<loginResponse> loginFromAPI(string username, string password)
        {

            clientApi.BaseAddress = new Uri("http://128.199.124.231:8084/");
            clientApi.DefaultRequestHeaders.Accept.Clear();
            clientApi.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Login login = new Login(username, password);

            //Submit the user credentials to get a token from server
            Task<HttpResponseMessage> response = clientApi.PostAsJsonAsync("Auth/Login", login);
            var data = response.Result.Content.ReadAsAsync<loginResponse>();

            Console.WriteLine(data.Result.token);

            return data;
        }

        public void verifyToken()
        {
            //Call GET action using the token from local storage
            clientApi.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Settings.Default.token);
            Task<HttpResponseMessage> response = clientApi.GetAsync("Auth/TestTokenValid");

            //Write result from protected action
            Task<string> values = response.Result.Content.ReadAsStringAsync();

            if (response.Result.StatusCode.ToString() == "OK")
            {
                Form2 _form2 = new Form2();
                _form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed Login!");
            }
            Console.WriteLine(response.Result.StatusCode);
        }
    }
}
