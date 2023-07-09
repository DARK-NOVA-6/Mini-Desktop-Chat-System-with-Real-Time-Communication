using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TcpIpClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BtnConnect.Enabled = false;
            BtnSend.Enabled = false;
        }
        private readonly string ServerName = "Server";
        private readonly string _HostName = "127.0.0.1";
        private readonly int _Port = 8888;

        private TcpClient _tcpClient;
        private Stream _stream;
        private readonly byte[] _receivedMsg = new byte[2048];
        private readonly ASCIIEncoding encoding = new ASCIIEncoding();
        private Thread ListeningThread;


        private readonly int BtnHeight = 65;
        private string clientName;

        private string Current = string.Empty;

        private Dictionary<string, string> Conversation;
        private Dictionary<string, string> UsersList;
        private Dictionary<string, int> NewMsgs;
        private Dictionary<string, Button> BtnUsersList;

        private bool Connected { get; set; }

        #region reinitialization

        private void InitializeClient()
        {
            Conversation = new Dictionary<string, string>();
            NewMsgs = new Dictionary<string, int>();
            UsersList = new Dictionary<string, string>();
            BtnUsersList = new Dictionary<string, Button>();
            Current = string.Empty;
            clientName = string.Empty;
            HandleThread(LabelUserName, () => LabelUserName.Text = string.Empty);
            HandleThread(LabelMsgs, () => LabelMsgs.Text = "Messages");
            HandleThread(PanelUsers, () => PanelUsers.Controls.Clear());
            HandleThread(TextMsgs, () => TextMsgs.Text = string.Empty);
            InitializeListeningThread();
        }

        private void InitializeListeningThread()
        {
            ListeningThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        _stream = _tcpClient.GetStream();
                        int bytesCount = _stream.Read(_receivedMsg, 0, 2048);
                        Encoding.ASCII.GetString(_receivedMsg, 0, bytesCount)
                            .Split("~")
                            .Where(msg => msg.Length > 0)
                            .ToList()
                            .ForEach(msg => MessageProcess(msg));
                    }
                }
                catch (Exception) { }
            });
        }

        #endregion

        #region Interact with the server

        private void TryConnect()
        {
            HandleThread(BtnConnect, () =>
            {
                BtnConnect.Enabled = false;
                BtnConnect.Text = "Connecting ..";
            });

            _tcpClient = new TcpClient();
            _tcpClient.Connect(_HostName, _Port);
            clientName = TextInput.Text.Trim();
            SendMsg(clientName);
            MessageBox.Show("Connected Successfuly", "Status", MessageBoxButtons.OKCancel);
            Connected = true;
            HandleThread(BtnConnect, () =>
            {
                BtnConnect.Enabled = true;
                BtnConnect.Text = "Disconnect";
            });
            HandleThread(TextInput, () => TextInput.Text = string.Empty);
            HandleThread(LabelName, () => LabelName.Text = "Message");
            HandleThread(LabelUserName, () => LabelUserName.Text = clientName);
        }


        private bool Connect()
        {
            InitializeClient();
            try
            {
                TryConnect();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    return Connect();
                return false;
            }
            ListeningThread.Start();
            Current = ServerName;
            AddUser(ServerName);
            RenderText();
            return true;
        }

        private bool Disconnect(bool forced = true)
        {
            HandleThread(BtnConnect, () =>
            {
                BtnConnect.Enabled = false;
                BtnConnect.Text = "Disconnecting ..";
                _tcpClient.Close();
                Connected = false;
                BtnConnect.Text = "Connect";
            });
            HandleThread(LabelName, () => LabelName.Text = "Username");
            if (!forced)
                InitializeClient();
            clientName = string.Empty;
            return true;
        }

        private void SendMsg(string msg)
        {
            _stream = _tcpClient.GetStream();
            byte[] encodedMsg = encoding.GetBytes(msg);
            _stream.Write(encodedMsg, 0, encodedMsg.Length);
        }

        #endregion

        #region processing the received message from server

        private void MessageProcess(string FullMsg)
        {
            if (FullMsg[0] == '&')
                ServerDisconnected();
            else if (FullMsg[0] == '@')
                AddOrUpdateUserStatus(FullMsg[1..].Split('!')[0], FullMsg[1..].Split('!')[1]);
            else
            {
                var arr = FullMsg.Split("%%");
                string sentFrom = arr[0], msg = string.Empty;
                for (int i = 1; i < arr.Length; i++)
                    msg += arr[i] + (i + 1 < arr.Length ? "%%" : string.Empty);
                if (FullMsg[0] != '#')
                    BroadcastMessage(sentFrom, msg);
                else
                    PrivateMessage(sentFrom[1..], msg);
                RenderText();
            }
        }

        private void ServerDisconnected() => Disconnect(true);

        private void AddOrUpdateUserStatus(string name, string status)
        {
            if (!BtnUsersList.ContainsKey(name))
                AddUser(name);
            UsersList[name] = status;
            RenderUsersList(name);
        }

        private void BroadcastMessage(string talker, string msg)
        {
            NewMsgs[ServerName]++;
            Conversation[ServerName] += $"{talker}:{Environment.NewLine}{msg}{Environment.NewLine}";
            RenderUsersList(ServerName);
        }

        private void PrivateMessage(string talker, string msg)
        {
            NewMsgs[talker]++;
            Conversation[talker] += $"{talker}:{Environment.NewLine}{msg}{Environment.NewLine}";
            RenderUsersList(talker);
        }

        #endregion

        #region Form methods

        private void AddUser(string name)
        {
            Conversation.Add(name, string.Empty);
            NewMsgs.Add(name, 0);
            UsersList.Add(name, "Online");
            Button btn = new Button
            {
                Text = $"{name} (Online)",
                Location = new Point(0, (BtnHeight + 3) * BtnUsersList.Count),
                Size = new Size(PanelUsers.Width, BtnHeight),
                Enabled = true,
                BackColor = Color.White
            };
            btn.Click += (object sender, EventArgs e) =>
            {
                BtnUsersList[Current].Enabled = true;
                BtnUsersList[Current].BackColor = Color.White;
                Current = name;
                RenderUsersList(name);
                RenderText();
            };
            BtnUsersList.Add(name, btn);
            HandleThread(PanelUsers, () => PanelUsers.Controls.Add(btn));
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!Connected)
                Connected = Connect();
            else
                Connected = !Disconnect(false);
        }

        private void TextInput_TextChanged(object sender, EventArgs e)
        {
            Button button = Connected ? BtnSend : BtnConnect;
            if (string.IsNullOrEmpty(TextInput.Text) || string.IsNullOrWhiteSpace(TextInput.Text))
                button.Enabled = false;
            else
                button.Enabled = true;
        }

        private void RenderText()
        {
            NewMsgs[Current] = 0;
            LabelMsgs.Text = Current;
            HandleThread(TextMsgs, () => TextMsgs.Text = Conversation[Current]);
            HandleThread(BtnUsersList[Current], () => BtnUsersList[Current].Enabled = false);
            RenderUsersList(Current);

        }

        private void RenderUsersList(string name)
        {
            HandleThread(BtnUsersList[name], () =>
            {
                string Text = $"{name} ({UsersList[name]})"
                                        + (NewMsgs[name] > 0 ? $" {NewMsgs[name]}" : string.Empty);
                Color color = NewMsgs[name] > 0 ? Color.Red : Color.White;
                if (Current == name)
                    color = Color.Gray;
                BtnUsersList[name].Text = $"{name} ({UsersList[name]})";
                BtnUsersList[name].Text = Text;
                BtnUsersList[name].BackColor = color;
            });
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendMsg("#" + (Current == ServerName ? string.Empty : Current) + "%%" + TextInput.Text + "~");
                Conversation[Current] += $"ME:{Environment.NewLine}{TextInput.Text}{Environment.NewLine}";
                TextInput.Text = string.Empty;
                RenderText();
            }
            catch (Exception) { }
        }

        #endregion

        private void HandleThread(Control control, Action action)
        {
            if (control.InvokeRequired)
                control.BeginInvoke((Action)delegate () { action(); });
            else
                action();
        }
    }
}
