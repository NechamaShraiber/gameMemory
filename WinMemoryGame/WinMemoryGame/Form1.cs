using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMemoryGame

{
    public partial class HmoePage : Form
    {

        HttpClient client = new HttpClient();
        string baseUrl = "http://localhost:58141/api/";
        public HmoePage()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnSignIn.Enabled = false;

            //validation
            if (txtName.Text == "")
            {
                lblMessage.Text = "Name is requier";
                return;
            }

            if (txtName.Text.Length < 2 || txtName.Text.Length > 10)
            {
                lblMessage.Text = "Name mast be between 2 to 10 letters";
                return;

            }
            if (txtAge.Text == "")
            {
                lblMessage.Text = "Age is requier";
                return;
            }
            int num;
            if (!int.TryParse(txtAge.Text, out num))
            {
                lblMessage.Text = "Age must be Number";
                return;
            }

            if (num < 18 || num > 120)
            {
                lblMessage.Text = "Age must be betweem 18 to 120 years";
                return;
            }
            else lblMessage.Text = "";
            btnSignIn.Enabled = true;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{baseUrl}login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            try
            {
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"userName\":\"" + txtName.Text + "\"," +
                  "\"age\":\"" + int.Parse(txtAge.Text) + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    if(result!="")
                    {

                      
                       dynamic message = JsonConvert.DeserializeObject(result);
                        lblMessage.Text = message;
                        return;
                    }
                    Properties.Settings.Default.userName = txtName.Text;
                    ChoosePartner c = new ChoosePartner();
                    c.Show();
                }
            }
            catch (WebException ex)
            {
                lblMessage.Text = "There are problems";
                Console.Write(ex.Message); 
            }
        }

        private void HmoePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Properties.Settings.Default.userName!=null)
            {
                Global.exit();
          
            }
           
        }


    }
}

