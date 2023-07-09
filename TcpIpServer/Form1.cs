using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Timers;

namespace TcpIpServer
{
    public partial class Form1 : Form
    {
        private readonly IPAddress _iPAddress = IPAddress.Parse("127.0.0.1");
        private readonly int _Port = 8888;
        private readonly string serverName = "Server";

        private TcpListener tcpListener;

        private readonly byte[] receivedMessage = new byte[2048];
        private readonly ASCIIEncoding asen = new ASCIIEncoding();

        private Thread serviceThread, CheckThread;

        private Dictionary<string, Thread> ClientsThread;
        private Dictionary<string, Socket> ClientsSocket;
        private Dictionary<string, string> ClientsStatus;
        private Dictionary<string, Queue<string>> QueueMsgs;

        private bool Running { get; set; }


        public Form1()
        {
            InitializeComponent();
            InitializeServer();
        }

        #region reinitialization
        private void InitializeServer()
        {
            ClientsSocket = new Dictionary<string, Socket>();
            ClientsThread = new Dictionary<string, Thread>();
            ClientsStatus = new Dictionary<string, string>();
            QueueMsgs = new Dictionary<string, Queue<string>>();
            HandleThread(TextMsgs, () => TextMsgs.Text = string.Empty);
            UpdateClientsList();
            HandleThread(BtnStart, () =>
            {
                BtnStart.Enabled = true;
                BtnStart.Text = "Start Service";
            });
            InitializeServiceThread();
            InitializeCheckThread();

        }

        private void InitializeServiceThread()
        {
            serviceThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        var client = tcpListener.AcceptSocket();
                        string name = Encoding.ASCII.GetString(receivedMessage, 0, client.Receive(receivedMessage));
                        NewClient(client, name);
                    }
                }
                catch (SocketException)
                {
                    Thread.CurrentThread.Interrupt();
                }
            });
        }

        private void InitializeCheckThread()
        {
            CheckThread = new Thread(() =>
            {
                string dateNow = DateTime.Now.ToShortTimeString();
                System.Timers.Timer timer = new System.Timers.Timer(1000)
                {
                    Enabled = true,
                    AutoReset = true
                };
                timer.Elapsed += (object sender, ElapsedEventArgs elapsedEventArgs) =>
                {
                    PingUsers();
                    ClientsStatus
                        .Keys
                        .Where(ClientName => ClientsStatus[ClientName] == "Online")
                        .Where(ClientName => !ClientsSocket[ClientName].Connected)
                        .ToList()
                        .ForEach(ClientName =>
                        {
                            ClientsStatus[ClientName] = dateNow;
                            SendMsg($"@{ClientName}!{dateNow}~{serverName}%%Client {ClientName} Disconnected in {dateNow}~", _ClientName => _ClientName != ClientName);
                            Msg(serverName, $"Client {ClientName} Disconnected in {dateNow}");
                        });
                    UpdateClientsList();
                };
            });
        }

        #endregion

        #region server methods

        private bool StartServer()
        {
            try
            {
                TryStartServer();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    return StartServer();
                return false;
            }
            CheckThread.Start();
            return true;
        }

        private void TryStartServer()
        {
            BtnStart.Enabled = false;
            BtnStart.Text = "Starting ..";
            InitializeServer();
            tcpListener = new TcpListener(_iPAddress, _Port);
            tcpListener.Start();
            serviceThread.Start();
            BtnStart.Text = "Stop Serviec";
            BtnStart.Enabled = true;
        }

        private bool StopServer()
        {
            HandleThread(BtnStart, () =>
            {
                BtnStart.Enabled = false;
                BtnStart.Text = "Stopping ..";
            });
            SendMsg($"{serverName}%%Server Has Been Stopped!!~&~", _ClientName => true);
            foreach (string ClientName in ClientsStatus.Keys)
            {
                ClientsSocket[ClientName].Shutdown(SocketShutdown.Both);
                ClientsSocket[ClientName].Close();
            }
            tcpListener.Stop();
            InitializeServer();
            return true;
        }

        private void DeleteOldClient(string name)
        {
            ClientsStatus.Remove(name);
            ClientsThread[name].Interrupt();
            ClientsThread.Remove(name);
            ClientsSocket[name].Shutdown(SocketShutdown.Both);
            ClientsSocket[name].Close();
            ClientsSocket.Remove(name);
        }

        private void NewClient(Socket client, string name)
        {
            var clientThr = new Thread(() => this.GetMessage(client, name));

            if (QueueMsgs.ContainsKey(name))
                DeleteOldClient(name);
            else
                QueueMsgs.Add(name, new Queue<string>());
            ClientsSocket.Add(name, client);
            ClientsThread.Add(name, clientThr);
            ClientsStatus.Add(name, "Online");

            clientThr.Start();

            ClientsStatus
                .Keys
                .Where(ClientName => ClientName != name)
                .ToList()
                .ForEach(ClientName =>
                    SendMsg($"@{ClientName}!{ClientsStatus[ClientName]}~", _ClientName => _ClientName == name));


            SendMsg($"@{name}!Online~{serverName}%%Client {name} Is Now Online~", _ClientName => _ClientName != name);
            Msg(serverName, $"Client {name} Is Now Online");

            UpdateClientsList();

            while (QueueMsgs[name].Count > 0)
                client.Send(asen.GetBytes(QueueMsgs[name].Dequeue()));
        }

        #endregion

        #region Interact with the clients

        private void GetMessage(Socket _client, string name)
        {
            try
            {
                while (true)
                {
                    var count = _client.Receive(receivedMessage);
                    if (count >= 1)
                        Encoding
                            .ASCII.GetString(receivedMessage, 0, count)
                            .Split("~")
                            .Where(msg => msg.Length > 0)
                            .ToList()
                            .ForEach(msg => MessageProcess(msg, name));
                }
            }
            catch (Exception)
            {
                Thread.CurrentThread.Interrupt();
            }
        }

        private void MessageProcess(string FullMsg, string name)
        {
            var arr = FullMsg.Split("%%");
            string msg = string.Empty;
            for (int i = 1; i < arr.Length; i++)
                msg += arr[i] + (i + 1 < arr.Length ? "%%" : string.Empty);
            Func<string, bool> Check;
            if (arr[0] != "#")
            {
                Check = ClientName => ClientName == arr[0][1..];
                msg = "#" + name + "%%" + msg;
            }
            else
            {
                Msg(name, msg);
                msg = name + "%%" + msg;
                Check = ClientName => ClientName != name;
            }
            SendMsg(msg + "~", Check);
        }

        private void SendMsg(string msg, Func<string, bool> Check)
        {
            PingUsers();
            ClientsSocket
                .Keys
                .Where(Check)
                .ToList()
                .ForEach(ClientName =>
                {
                    if (ClientsSocket[ClientName].Connected)
                        ClientsSocket[ClientName].Send(asen.GetBytes(msg));
                    else
                        QueueMsgs[ClientName].Enqueue(msg);
                });
        }

        private void PingUsers()
        {
            ClientsSocket
                .Keys
                .Where(ClientName => ClientsSocket[ClientName].Connected)
                .ToList()
                .ForEach(ClientName => ClientsSocket[ClientName].Send(asen.GetBytes("~")));
        }

        #endregion

        #region Form methods

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!Running)
                Running = StartServer();
            else
                Running = !StopServer();
        }


        private void BtnSend_Click(object sender, EventArgs e)
        {
            string msg = serverName + "%%" + TextBroadcastMeg.Text + "~";
            SendMsg(msg, ClientName => true);
            Msg(serverName, TextBroadcastMeg.Text);
            TextBroadcastMeg.Text = string.Empty;
        }

        private void UpdateClientsList()
        {
            HandleThread(TextClientList, () =>
            {
                TextClientList.Text = string.Empty;
                ClientsStatus
                    .Keys
                    .ToList()
                    .ForEach(ClientName =>
                        TextClientList.Text += $"{ClientName}: {ClientsStatus[ClientName]}{Environment.NewLine}"
                    );

            });
        }

        private void MsgTxt_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBroadcastMeg.Text) || string.IsNullOrWhiteSpace(TextBroadcastMeg.Text))
                BtnSend.Enabled = false;
            else
                BtnSend.Enabled = true;
        }

        private void Msg(string name, string msg)
        {
            HandleThread(TextMsgs, () => TextMsgs.AppendText($"{name}:{Environment.NewLine}{msg}{Environment.NewLine}"));
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
