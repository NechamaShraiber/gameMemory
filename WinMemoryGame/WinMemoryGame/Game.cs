using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
namespace WinMemoryGame
{
    public partial class Game : Form
    {
        string baseUrl = "http://localhost:58141/api/";
        Dictionary<string, string> cards;
        string currentTurn;
        string charapter;
        bool isFirstTime = true;
        bool isStop;
        private Timer timerGetUssers, timerHasPartner;

        public Game( Timer timerGetUssers, Timer timerHasPartner)
        {
            InitializeComponent();
            lblUserName.Text = Properties.Settings.Default.userName;
           this.timerGetUssers = timerGetUssers;
            this.timerHasPartner = timerHasPartner;
        }
        public void GetAllCards()
        {
        
            isStop = true;
            charapter = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:58141/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"getGame/{Properties.Settings.Default.userName}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                dynamic obj = JsonConvert.DeserializeObject(result);
                currentTurn = obj["CurrentTurn"];
                gbCards.Text = $"current turn: {currentTurn}";
                var jsonCards = obj["CardArray"];
                cards = new Dictionary<string, string>();
                foreach (var item in jsonCards)
                {
                    cards.Add(item.Name, item.Value.Value);
                }

                if (isFirstTime)
                {
                    orderCards();

                }
                //show the raised cards
                else
                {

                    foreach (Control control in gbCards.Controls)
                    {
                        //this card adjusted pair
                        if (cards[control.Name] != null)
                        {
                            control.Text = control.Name;
                            control.BackColor = cards[control.Name]== Properties.Settings.Default.userName?Color.Red:Color.Gray;
                            control.Enabled = false;
                        }
                        else
                        {
                            control.Text = "?";
                            isStop = false;
                            control.Enabled = true;
                        }
                    }
                    //if all the cards adjusted pair - stop the game
                    if (isStop)
                    {
                        int myPairs = 0, otherPairs = 0;
                        timerGetCards.Enabled = false;
                        foreach (var item in cards)
                        {
                            if (item.Value == Properties.Settings.Default.userName)
                                myPairs++;
                            else otherPairs++;
                        }
                        MessageBox.Show(Properties.Settings.Default.userName +" "+(myPairs > otherPairs ? "You won!!" : "you lost!!"));
                        timerGetUssers.Enabled = true;
                        timerHasPartner.Enabled=true;
                        this.Close();
                        
                        return;
                    }

                }
                //not allow play if isn't this player turn and wait the other player stop play 
                if (currentTurn != Properties.Settings.Default.userName)
                {
                    timerGetCards.Enabled = true;
                    foreach (Control item in gbCards.Controls)
                    {
                        item.Enabled = false;
                    }
                }
                else
                {
                    timerGetCards.Enabled = false;
                }
            }

            else
            {
                MessageBox.Show("there are problems");
                timerGetCards.Enabled = false;
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);

            }

        }

        private void orderCards()
        {
            isFirstTime = false;
            Random r = new Random();
            List<int> controls = new List<int>();
            for (int i = 0; i < 18; i++)
            {
                controls.Add(i);
            }
            foreach (var item in cards)
            {
                //2 cards for each charapter
                for (int i = 0; i < 2; i++)
                {
                    int num = r.Next(0, controls.Count);
                    gbCards.Controls[controls[num]].Name = item.Key;
                    controls.RemoveAt(num);
                }

            }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            GetAllCards();
           

        }

        private void btn1_Click(object sender, EventArgs e)
        {

            (sender as Button).Text = (sender as Button).Name;
            if (charapter == null)
            {
                charapter = (sender as Button).Name;
                (sender as Button).Enabled = false;
            }
            else
            {
                if ((sender as Button).Name == charapter)
                    MessageBox.Show("good");
                else MessageBox.Show("not good");
                HttpClient httpClient = new HttpClient();
                var response = httpClient.PutAsync(new Uri($"{baseUrl}updateTurn/{Properties.Settings.Default.userName}/{charapter}/{(sender as Button).Name}"), new StringContent("")).Result;

                if (response.IsSuccessStatusCode)
                {
                    GetAllCards();
                }
                else
                {
                    MessageBox.Show("there are problems");
                }
            }
        }

        private void timerGetCards_Tick(object sender, EventArgs e)
        {
            GetAllCards();
        }
    }
}



