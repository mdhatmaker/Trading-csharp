using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Resources;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;
using TT.Hatmaker;
using NMALib;
using System.Configuration;
using System.Media;
using System.Threading;
using EZAPI;
using EZAPI.Containers;

namespace FillsDeluxe
{
    public partial class MainForm : Form
    {
        private const string VERSION = "v1.0";
        private const bool DEBUG = false;
        private TTAPIFunctions api;
        private Thread _apiThread;

        #region PROPERTY FORMS and SETTINGS
        private PropertyForm settingsForm;
        private Dictionary<string, string> settings = new Dictionary<string, string>();
        private PropertyForm soundSettingsForm;
        private Dictionary<string, string> soundSettings = new Dictionary<string, string>();
        private PropertyForm messageSettingsForm;
        private Dictionary<string, string> messageSettings = new Dictionary<string, string>();
        private PropertyForm hedgerSettingsForm;
        private Dictionary<string, string> hedgerSettings = new Dictionary<string, string>();
        #endregion

        private Dictionary<InstrumentKey, InstrumentDescriptor> instruments = new Dictionary<InstrumentKey, InstrumentDescriptor>();
        private Dictionary<InstrumentKey, InstrumentDescriptor> managedHedges = new Dictionary<InstrumentKey, InstrumentDescriptor>();
        private List<InstrumentDescriptor> managedHedgeList = new List<InstrumentDescriptor>();
        private Dictionary<string, List<TTFill>> hashSymbols = new Dictionary<string, List<TTFill>>();

        public MainForm()
        {
            InitializeComponent();
           
            APIStart(LoginType.Universal);
        }

        /// <summary>
        /// After the TTAPI is initialized, the AppStart method is called.
        /// </summary>
        void AppStart()
        {
            //TTInstrument tti = api.CreateInstrument("CME", "FUTURE", "CL", "Dec12");
            //Console.WriteLine(tti.Name);

            settingsForm = new PropertyForm("General Settings", "Enter your general application settings here.");

            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            soundSettingsForm = new PropertyForm("Sounds", "Specify different sounds for different products. You put the instrument name (or any portion of it) in the Property Name column and the sound filename in the Property Value column. If you double-click in a cell in the Property Value column, a file dialog will appear that will let you find your sounds more easily.", appPath + "\\SOUNDS.TXT");
            messageSettingsForm = new PropertyForm("Messaging", "Enter your messaging settings for text message, iPhone and Android.", appPath + "\\MESSAGING.TXT");
            hedgerSettingsForm = new PropertyForm("Hedger", "Enter instruments for which you want to run the Hedger. See the help file for the format to enter these instruments.", appPath + "\\HEDGER.TXT");

            // Read and apply the settings...and connect to the event for property form closing
            ReadSettings();
            ApplySettings();
            soundSettingsForm.OnPropertyFormClose += new PropertyForm.PropertyFormResult(settingsForm_OnPropertyFormClose);
            messageSettingsForm.OnPropertyFormClose += new PropertyForm.PropertyFormResult(settingsForm_OnPropertyFormClose);

            this.Text = "FillsDeluxe " + VERSION;
        }

        void AppStop()
        {
            api.Dispose();
        }

        #region EZAPI STARTUP AND CALLBACKS
        #region EZAPI STARTUP
        void APIStart(LoginType loginType)
        {
            if (loginType == LoginType.Universal)
            {
                // Use Universal Login Mode
                APILoginForm loginForm = new APILoginForm();
                loginForm.ShowDialog(this);

                if (loginForm.LoginResult != LoginResult.Cancel)
                {
                    UniversalAPIStart(loginForm.Username, loginForm.Password);
                }
            }
            else if (loginType == LoginType.XTrader)
            {
                // Use XTrader Connect Mode
                XTraderAPIStart();
            }
        }

        void UniversalAPIStart(string ttUsername, string ttPassword)
        {
            bool autoSubscribeInstruments = true;
            bool subscribeMarketDepth = false;
            bool subscribeTimeAndSales = false;
            api = new TTAPIFunctions(autoSubscribeInstruments, subscribeMarketDepth, subscribeTimeAndSales, ttUsername, ttPassword);
            api.OnInitialize += new InitializeHandler(api_OnInitialize);
            api.OnInstrumentFound += new InstrumentFoundHandler(api_OnInstrumentFound);
            api.OnInsideMarketUpdate += new InsideMarketHandler(api_OnInsideMarketUpdate);
            api.OnFill += new FillHandler(api_OnFill);
            api.OnOrder += new OrderHandler(api_OnOrder);

            _apiThread = new Thread(api.StartUniversal);
            _apiThread.Name = "Universal TTAPI Thread";
            _apiThread.Start();
        }

        void XTraderAPIStart()
        {
            bool autoSubscribeInstruments = true;
            bool subscribeMarketDepth = false;
            bool subscribeTimeAndSales = false;
            api = new TTAPIFunctions(autoSubscribeInstruments, subscribeMarketDepth, subscribeTimeAndSales);
            api.OnInitialize += new InitializeHandler(api_OnInitialize);
            api.OnInstrumentFound += new InstrumentFoundHandler(api_OnInstrumentFound);
            api.OnInsideMarketUpdate += new InsideMarketHandler(api_OnInsideMarketUpdate);
            api.OnFill += new FillHandler(api_OnFill);
            api.OnOrder += new OrderHandler(api_OnOrder);

            _apiThread = new Thread(api.StartXTMode);
            _apiThread.Name = "XTMode TTAPI Thread";
            _apiThread.Start();
        }
        #endregion

        void api_OnInstrumentFound(TTInstrument ttInstrument, bool success)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInstrumentFound(ttInstrument, success);
                });
                return;
            }
            #endregion

            if (success)
            {
                // TODO: change this to represent a selected order feed
                foreach (OrderFeed oFeed in ttInstrument.TTAPI_Instrument.GetValidOrderFeeds())
                {
                    if (oFeed.Name.Equals("CME-C"))
                    {
                        OrderRoute route = new OrderRoute(oFeed, AccountType.Agent1, "15085304");
                        ttInstrument.OrderRoute = route;
                        // TODO: where do i get these accounts from?
                        /*ttInstrument.AccountType = AccountType.Agent1;
                        ttInstrument.AccountName = "15085304";*/
                    }
                }
            }
        }

        void api_OnOrder(TTOrderStatus status, TTInstrument ttInstrument, TTOrder order, TTOrder newOrder)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnOrder(status, ttInstrument, order, newOrder);
                });
                return;
            }
            #endregion

        }

        void api_OnFill(EZAPI.Containers.FillOriginator originator, EZAPI.Containers.FillAction action, TTInstrument ttInstrument, TTFill fill, TTFill newFill)
        //void api_OnFill(FillOriginator originator, FillAction action, TTInstrument ttInstrument, TTFill fill, TTFill newFill)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnFill(originator, action, ttInstrument, fill, newFill);
                });
                return;
            }
            #endregion

            // Display the fill in the grid and play and sounds and/or send any messages related to
            // this fill.
            TTFill ttf = new TTFill(fill.TTAPI_Fill);
            displayFill(fillGrid, ttf);
            UpdateFillCount(fillGrid.Rows.Count);
            UpdateProfit(0);
            playSound(ttf);
            sendMessage(ttf);

            // See if this fill contains our '#' hashtag in either FFT field.
            string hashField = null;
            if (fill.FFT2.StartsWith("#"))
                hashField = fill.FFT2;
            if (fill.FFT3.StartsWith("#"))
                hashField = fill.FFT3;

            // If we found a valid '#' hashtag value, then put it in our dropdown combo box.
            if (hashField != null)
            {
                // Add this fill to our list of fills with this same hashsymbol
                if (!hashSymbols.ContainsKey(hashField))
                    hashSymbols.Add(hashField, new List<TTFill>());
                List<TTFill> hashFills = hashSymbols[hashField];
                hashFills.Add(fill);

                // Add this hashsymbol to our dropdown list if it is not already there
                if (!tscomboFFT.Items.Contains(hashField))
                {
                    tscomboFFT.Items.Add(hashField);
                }

                // If the user is filtering by this hash symbol then put this fill in the filter grid also
                string selectedHash = tscomboFFT.SelectedItem as string;
                if (selectedHash != null && selectedHash.Equals(hashField))
                {
                    displayFill(filteredGrid, ttf);
                }
            }

        }

        void api_OnInitialize(bool success, string message)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInitialize(success, message);
                });
                return;
            }
            #endregion

            if (success == true)
            {
                Console.WriteLine("TTAPI initialized successfully.");
                AppStart();
            }
            else
            {
                MessageBox.Show(message, "TTAPI Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void api_OnInsideMarketUpdate(TTInstrument instrument)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInsideMarketUpdate(instrument);
                });
                return;
            }
            #endregion

            //InstrumentKey key = instrument.Key;
            //TTInstrument tti = ttInstruments[key];
        }

        void api_OnSystemMessage(SystemMessage systemMessage)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnSystemMessage(systemMessage);
                });
                return;
            }
            #endregion

        }
        #endregion

        #region SETTINGS
        void settingsForm_OnPropertyFormClose(DialogResult dlgResult)
        {
            if (dlgResult == DialogResult.OK)
                ReadSettings();
        }

        private void ReadSettings()
        {
            settings = settingsForm.GetSettings();
            soundSettings = soundSettingsForm.GetSettings();
            messageSettings = messageSettingsForm.GetSettings();
            hedgerSettings = hedgerSettingsForm.GetSettings();
        }

        private void ApplySettings()
        {
            /*
            // See if there are any instruments for which hedge orders we need to handle.
            foreach (string key in hedgerSettings.Keys)
            {
                if (key.StartsWith("ManagedHedge"))
                {
                    string instrumentDescriptor = hedgerSettings[key];
                    if (!managedHedgeList.Contains(new InstrumentInfo(instrumentDescriptor)))
                    {
                        InstrumentInfo info = SubscribeToInstrument(instrumentDescriptor);
                        managedHedgeList.Add(info);
                    }
                }
            }*/
        }
        #endregion

        #region SHOW SETTINGS FORMS
        private void ShowMessagingSettingsForm()
        {
            messageSettingsForm.Show();
            messageSettingsForm.BringToFront();
        }

        private void ShowSoundSettingsForm()
        {
            soundSettingsForm.Show();
            soundSettingsForm.BringToFront();
        }

        private void ShowHedgerSettingsForm()
        {
            hedgerSettingsForm.Show();
            hedgerSettingsForm.BringToFront();
        }

        private void ShowSettingsForm()
        {
            settingsForm.Show();
            settingsForm.BringToFront();
        }
        #endregion

        #region HELPER METHODS
        void Message(string msg)
        {
            tslblInfo.Text = msg;
            if (DEBUG) Console.WriteLine(msg);
        }

        void ErrorMessage(string msg)
        {
            tslblInfo.Text = "ERROR: " + msg;
            if (DEBUG) Console.WriteLine("ERROR: " + msg);
        }
        #endregion

        #region DISPLAY FILLS and ORDERS
        void displayFill(DataGridView grid, TTFill ttf)
        {
            int rowIndex = grid.Rows.Add(ttf.ShortTime, ttf.Identifiers, ttf.BuySell, ttf.Quantity, ttf.InstrumentName, ttf.Price);
            if (tsitemAutoScroll.Checked)
                grid.FirstDisplayedScrollingRowIndex = grid.RowCount - 1;
            DataGridViewRow row = grid.Rows[rowIndex];
            if (ttf.BuySell == BuySell.Buy)
            {
                row.DefaultCellStyle.BackColor = Color.Blue;
                row.DefaultCellStyle.ForeColor = Color.White;
            }
            else if (ttf.BuySell == BuySell.Sell)
            {
                row.DefaultCellStyle.BackColor = Color.Red;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            Message("FILL: " + ttf);
        }

        void displayOrder(string orderMessage)
        {
            //listOrders.Items.Insert(0, orderMessage);
        }

        void displayOrder(Order order)
        {
            //listOrders.Items.Insert(0, OrderToString(order));
        }

        void displayOrder(string orderMessage, Order order)
        {
            //listOrders.Items.Insert(0, string.Format("{0} {1}", orderMessage, OrderToString(order)));
        }
        #endregion

        void ManageHedgedOrders(Order order)
        {
            if (!tsitemEnableHedging.Checked)
                return;

            // See if this order's instrument is in our managed hedges list
            foreach (InstrumentKey key in managedHedges.Keys)
            {
                InstrumentDescriptor descriptor = managedHedges[key];
                /*if (order.InstrumentKey == descriptor.InstrumentKey)
                {

                    //listOrders.Items.Insert(0, OrderToString(order));
                    if (order.IsChild && !order.IsSynthetic)
                    {
                        Instrument instrument = descriptor.Instrument;
                        OrderProfile profile = CreateOrderCopy(order, instrument);
                        // try to delete the order and re-insert it
                        if (order.Delete())
                        {
                            DisplayOrderSuccess(instrument.Session.SendOrder(profile));
                        }
                    }
                    break;
                }*/

            }
        }
        
        void playSound(TTFill ttf)
        {
            if (!tsitemEnableSounds.Checked)
                return;

            foreach (string pName in soundSettings.Keys)
            {
                string soundFile = soundSettings[pName].Trim();
                if (ttf.InstrumentName.Contains(pName))
                {
                    SoundPlayer snd = new SoundPlayer(soundFile);
                    snd.Play();
                }
            }
        }

        void sendMessage(TTFill ttf)
        {
            string subject = "FILL " + ttf.ShortTime;
            string body = ttf.ToString();

            if (tsitemTextMessageAlert.Checked)
            {
                string host = messageSettings["HOST"];
                int port = int.Parse(messageSettings["PORT"]); // should be 465 or 587
                string username = messageSettings["EMAIL_USERNAME"];
                string password = messageSettings["EMAIL_PASSWORD"];
                string fromEmail = messageSettings["EMAIL_ADDRESS"];

                Messaging msg = new Messaging(host, port, username, password, fromEmail);

                string carrier = messageSettings["CARRIER"];
                string phoneNumber = messageSettings["PHONE_NUMBER"];
                CellularRecipient recipient = new CellularRecipient(carrier, phoneNumber);
                msg.SendTextMessage(recipient, body, subject);
            }
            if (tsitemiPhoneAlert.Checked)
            {
                string apiKey = messageSettings["PROWL_KEY"];
                ProwlAPI.API_KEY = apiKey;
                ProwlAPI.Send(subject, body, ProwlAPI.Priority.NORMAL);
            }
            if (tsitemAndroidAlert.Checked)
            {
                string apiKey = messageSettings["NOTIFYMYANDROID_KEY"];

                var notification =
                    new NMANotification
                    {
                        Description = body,
                        Event = subject,
                        Priority = NMANotificationPriority.Normal
                    };

                // Create a notification.
                /*var testNotification =
                    new NMANotification
                    {
                        Description = "This is a test notification.",
                        Event = "Testing...",
                        Priority = NMANotificationPriority.Normal
                    };
                */

                // Create the NMA client.
                // By default, the NMA client will attempt to load configuration
                // from the configuration file (app.config).  You can use an overloaded constructor
                // to configure the client directly and bypass the configuration file.
                NMAClientConfiguration clientCfg = new NMAClientConfiguration();
                clientCfg.ApiKeychain = apiKey;
                clientCfg.ProviderKey = apiKey;
                clientCfg.ApplicationName = "Fills Deluxe";
                var testClient = new NMAClient(clientCfg);

                // Post the notification.
                testClient.PostNotification(notification);
            }
        }
        void UpdateFillCount(int count)
        {
            tslblFillCount.Text = "Fill count: " + count.ToString();
        }

        void UpdateProfit(double profit)
        {
            tslblProfit.Text = "Profit: $" + profit.ToString();
        }

        #region SHOW SETTINGS FORMS
        private void generalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void tsbtnMessaging_Click(object sender, EventArgs e)
        {
            ShowMessagingSettingsForm();
        }

        private void tsitemHedgerSettings_Click(object sender, EventArgs e)
        {
            ShowHedgerSettingsForm();
        }

        private void tsitemMessagingSettings_Click(object sender, EventArgs e)
        {
            ShowMessagingSettingsForm();
        }

        private void soundSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSoundSettingsForm();
        }
        #endregion

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fillGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            filteredGrid.Rows.Clear();
            Console.WriteLine(fillGrid.Rows[e.RowIndex].Cells["ProductColumn"].Value);
            string instrumentKey = fillGrid.Rows[e.RowIndex].Cells["ProductColumn"].Value as string;
            foreach (DataGridViewRow row in fillGrid.Rows)
            {
                if (row.Cells["ProductColumn"].Value != null && row.Cells["ProductColumn"].Value.Equals(instrumentKey))
                {
                    int rowIndex = filteredGrid.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value, row.Cells[5].Value);
                    filteredGrid.Rows[rowIndex].DefaultCellStyle.BackColor = row.DefaultCellStyle.BackColor;
                    filteredGrid.Rows[rowIndex].DefaultCellStyle.ForeColor = row.DefaultCellStyle.ForeColor;
                }
            }
        }

        #region TOOLSTRIP MENU ITEM CLICKS
        private void tsitemTextMessageAlert_Click(object sender, EventArgs e)
        {
            /*if (tsitemTextMessageAlert.Checked)
                tsitemTextMessageAlert.Checked = false;
            else
                tsitemTextMessageAlert.Checked = true;*/
        }

        private void tsitemiPhoneAlert_Click(object sender, EventArgs e)
        {
            if (tsitemiPhoneAlert.Checked || tsitemAndroidAlert.Checked)
                tsitemTextMessageBackup.Enabled = true;
            else
                tsitemTextMessageBackup.Enabled = false;
        }

        private void tsitemAndroidAlert_Click(object sender, EventArgs e)
        {
            if (tsitemiPhoneAlert.Checked || tsitemAndroidAlert.Checked)
                tsitemTextMessageBackup.Enabled = true;
            else
                tsitemTextMessageBackup.Enabled = false;
        }

        private void tsitemSendTestMessage_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> settings = messageSettingsForm.GetSettings(); 

            string host = settings["HOST"];
            int port = int.Parse(settings["PORT"]); // should be 465 or 587
            string username = settings["EMAIL_USERNAME"];
            string password = settings["EMAIL_PASSWORD"];
            string fromEmail = settings["EMAIL_ADDRESS"];

            Messaging msg = new Messaging(host, port, username, password, fromEmail);
            
            string toEmail = fromEmail.Clone() as string;
            string body = "This is a test from Fills Deluxe";
            string subject = "Fills Deluxe Test";
        
            msg.SendMail(toEmail, body, subject);

            string carrier = settings["CARRIER"];
            string phoneNumber = settings["PHONE_NUMBER"];
            CellularRecipient recipient = new CellularRecipient(carrier, phoneNumber);
            msg.SendTextMessage(recipient, "This is a test from Fills Deluxe", "Fills Deluxe Test");
        }

        private void tsitemAutoScroll_Click(object sender, EventArgs e)
        {

        }

        private void tsitemEnableSounds_Click(object sender, EventArgs e)
        {

        }

        private void tscomboFFT_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tag = tscomboFFT.SelectedItem as string;
            if (tag == null)
                return;

            Console.WriteLine("TAG: " + tag);

            filteredGrid.Rows.Clear();
            foreach (TTFill fill in hashSymbols[tag])
            {
                TTFill ttf = new TTFill(fill.TTAPI_Fill);
                displayFill(filteredGrid, ttf);
            }
        }
        #endregion

        private void tsbtnInfo_Click(object sender, EventArgs e)
        {
            (new AboutBox()).ShowDialog(this);
        }

        private void tsbtnHelp_Click(object sender, EventArgs e)
        {
            TTInstrument tti = api.GetInstrument("HG");
            TTOrder order = api.BuyLimit(tti, 1, 37800);
            //order.SimplePrice = 37780;
            //order.SimpleQuantity = 3;
            //order.Hold(true);
            //order.Hold(false);
            order.Cancel();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppStop();
        }

    } // class
} // namespace
