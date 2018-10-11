using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace WinMemoryGame
{
    public partial class ChoosePartner : Form
    {

        string baseUrl = "http://localhost:58141/api/";
        List<User> userList = new List<User>();
        string oldResponse;
        public ChoosePartner()
        {
            InitializeComponent();
            lblUserName.Text = Properties.Settings.Default.userName;
        }
        void getAllPartners()
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetStringAsync(new Uri($"{baseUrl}getUsersWaitToPartner")).Result;
            //refresh the dataGridWiew only if data changed
            if (oldResponse != response)
            {
                userList = new List<User>();
                var partners = JArray.Parse(response).ToList();
                foreach (var item in partners)
                {
                    if (item["UserName"].ToString() != Properties.Settings.Default.userName)
                        userList.Add(new User() { UserName = item["UserName"].ToString(), Age = int.Parse(item["Age"].ToString()) });
                }
                dgvPartner.DataSource = userList;
                dgvPartner.Columns["PartnerName"].Visible = false;
                oldResponse = response;
            }
        }

        void HavePartner()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:58141/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"getUserDetails/{Properties.Settings.Default.userName}").Result;
            if (response.IsSuccessStatusCode)
            {

                var result = response.Content.ReadAsStringAsync().Result;
                dynamic obj = JsonConvert.DeserializeObject(result);
                if (obj["PartnerName"] != null)
                {
                    lblMessage.Text = "you have partner: " + obj["PartnerName"];
                    timerHasPartner.Enabled = false;
                    Game g = new Game(timerGetUssers, timerHasPartner);
                    g.Show();
                }
            }
        }
        private void timerGetUsers(object sender, EventArgs e)
        {

            getAllPartners();
        }
        private void timerHasPartner_Tick(object sender, EventArgs e)
        {
            HavePartner();
        }

        private void dgvPartner_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var partner = dgvPartner.Rows[e.RowIndex].Cells[0].Value.ToString();
            HttpClient httpClient = new HttpClient();
            var response = httpClient.PutAsync(new Uri($"{baseUrl}ChoosingPartner/{Properties.Settings.Default.userName }/{partner}"), new StringContent("")).Result;
            if (response.IsSuccessStatusCode)
            {
                lblMessage.Text = "you have partner";
                timerHasPartner.Enabled = false;
                Game g = new Game(timerGetUssers,timerHasPartner);
              
                g.Show();

            }
            else
            {
                lblMessage.Text = response.ReasonPhrase;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (Global.exit())
            {
                Environment.Exit(0);
            }
        }

        private void ChoosePartner_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}




