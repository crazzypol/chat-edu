using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace ChatForLan
{
    public partial class frmMain : Form
    {
        const string MCASTADDR = "255.255.255.255";
        const int MCASTPORT = 32760;
        string MYIPADDR;
        
        ArrayList userList = new ArrayList(); //Список для хранения и управления именами пользователей
        bool IsThreadRunning = false; //Переменная управляющая потоком слушателя
        
        Thread _listener; //Переменная класса для создания отдельного потока для слушателя сети

        //Делегаты для изменения списка пользователя
        public delegate void AddListItem(string name); 
        public delegate void RemoveListItem(string name);
        //Переменные на основе делегатов
        public AddListItem myAddListItem;
        public RemoveListItem myRemoveListItem;

        public frmMain()
        {
            InitializeComponent();
            //Присвоим переменным делегатам реальные функции
            myAddListItem = new AddListItem(AddListItemMethod); 
            myRemoveListItem = new RemoveListItem(RemoveListItemMethod);

            //Узнаем свой адрес в сети
            IPAddress[] _ipaddrs = Dns.GetHostEntry(GetName()).AddressList;
            foreach (IPAddress _oneaddr in _ipaddrs) {
                if (_oneaddr.AddressFamily == AddressFamily.InterNetwork) {
                    MYIPADDR = _oneaddr.ToString();
                }
            }
            //Создаем и запускам отдельный поток-слушатель сети
            _listener = new Thread(new ThreadStart(_ReceiveWorker));
            _listener.Start();
            //Отправляем широковещательное сообщение о нашем появлении в сети
            string message = "Hello$" + GetName();
            MulticastSend(MCASTADDR, MCASTPORT, message);
        }

        /// <summary>
        /// Поток-слушатель сети
        /// Функция передается в ThreadStart
        /// </summary>
        private void _ReceiveWorker() {
            Receive(MYIPADDR, MCASTPORT);
        }

        /// <summary>
        /// Добавление пользователя в список пользователей чата
        /// </summary>
        /// <param name="name"></param>
        void AddListItemMethod(string name)
        {
            if (lvUsers.InvokeRequired)
            {
                Invoke(myAddListItem, new object[] { name });
            }
            else
            {
                lvUsers.Items.Add(name);
                rtReceiveMessage.AppendText("Появляется " + name + "\n"); 
            }
      
        }

        /// <summary>
        /// Удаление пользователя из списка пользователей чата
        /// </summary>
        /// <param name="name"></param>
        void RemoveListItemMethod(string name)
        {
            if (lvUsers.InvokeRequired)
            {
                Invoke(myRemoveListItem, new object[] { name });
            }
            else
            {
                rtReceiveMessage.AppendText(name + " уходит" + "\n");
                int _idxuser = userList.IndexOf(name);
                if (_idxuser > 0) lvUsers.Items.RemoveAt(_idxuser);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lvUsers.Columns[0].Width = splitContainer1.Panel1.Width-4;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            lvUsers.Columns[0].Width = splitContainer1.Panel1.Width - 4;
        }

        private void bnSendMessage_Click(object sender, EventArgs e)
        {
            _SendMessage();
        }

        private void rtSendMessage_KeyUp(object sender, KeyEventArgs e)
        {
            ///
            //При нажатии ENTER сообщение будет отправлено, если в это время нажата клавиша CTRL просто курсор перейдет на новую строку 
            ///
            if (e.KeyCode == Keys.Enter && !e.Control) {
                _SendMessage();
            }
        }

        /// <summary>
        /// Функция подготовки сообщения для отправки
        /// </summary>
        private void _SendMessage ()
        {
            if (rtSendMessage.Text == "") return;
            String _message = "MSG:$" + GetName() + " : " + rtSendMessage.Text;
            rtReceiveMessage.AppendText(GetName() + " : " + rtSendMessage.Text + "\n");
            if (lvUsers.SelectedItems.Count == 0) return;
            string _user = lvUsers.SelectedItems[0].Text;
            if (_user.Length > 1)
            {
                string _ipaddr = "";
                IPAddress[] _ipaddrs = Dns.GetHostEntry(_user).AddressList;
                foreach (IPAddress _oneaddr in _ipaddrs){
                    if (_oneaddr.AddressFamily == AddressFamily.InterNetwork) {
                        _ipaddr = _oneaddr.ToString ();
                    }
                }
                MulticastSend(_ipaddr, MCASTPORT, _message);
                rtSendMessage.Text = "";
            }
        }

        /// <summary>
        /// Функция отправки сообщения
        /// </summary>
        /// <param name="mAddress"></param>
        /// <param name="port"></param>
        /// <param name="message"></param>
        private void MulticastSend(string mAddress, int port, string message)
        {
            try
            {
                IPAddress GroupAddress = IPAddress.Parse(mAddress);
                int GroupPort = port;
                UdpClient sender = new UdpClient();
                IPEndPoint groupEP = new IPEndPoint(GroupAddress, GroupPort);
                byte[] bytes = Encoding.Default.GetBytes(message);
                sender.Send(bytes, bytes.Length, groupEP);
                sender.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// Слушаем сеть и в случае приема сообщения разбираем его
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        private void Receive(string address, int port)
        {
            Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint _ipendp = new IPEndPoint(IPAddress.Any, port);
            _socket.Bind(_ipendp);
            //IPAddress _ipaddr = IPAddress.Parse(address);
            IsThreadRunning = true;
            while (IsThreadRunning)
            {
                byte[] _receivebuf = new byte[500];
                _socket.Receive(_receivebuf);
                string _receivestr = System.Text.Encoding.Default.GetString(_receivebuf, 0, _receivebuf.Length);
                _receivestr = _receivestr.Replace("\0", "");
                if (_receivestr.StartsWith("Hello"))
                {
                    string[] array = _receivestr.Split('$');
                    if (array.Length > 1)
                    {
                        if (!userList.Contains(array[1]))
                        {
                            userList.Add(array[1]);
                            myAddListItem(array[1]);
                            string _sendip;
                            string _sendmes = "Hello$" + GetName();
                            IPAddress[] _tmpipaddr = Dns.GetHostEntry(array[1]).AddressList;
                            if (_tmpipaddr.Length > 1)
                            {
                                _sendip = _tmpipaddr[1].ToString();
                            }
                            else
                            {
                                _sendip = _tmpipaddr[0].ToString();
                            }
                            MulticastSend(_sendip, MCASTPORT, _sendmes);
                        }
                    }
                }
                else if (_receivestr.StartsWith("Stop"))
                {
                    string[] array = _receivestr.Split('$');
                    if (array.Length > 1)
                    {
                        if (userList.Contains(array[1]))
                        {
                            myRemoveListItem(array[1]);
                            userList.Remove(array[1]);
                        }
                    }
                }
                else if (_receivestr.StartsWith("MSG:"))
                {
                    _receivestr = _receivestr.Replace("MSG:$", "");
                    int start = this.rtReceiveMessage.TextLength;
                    int length = _receivestr.IndexOf(':');
                    rtReceiveMessage.AppendText(_receivestr + "\n");
                    if (start >= 0 && start < rtReceiveMessage.TextLength && length > 0 && length + start < rtReceiveMessage.TextLength)
                    {
                        this.rtReceiveMessage.Select(start, length);
                        this.rtReceiveMessage.SelectionColor = Color.Blue;
                        this.rtReceiveMessage.SelectionLength = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Получаем название машины, оно станет именем пользователя
        /// </summary>
        /// <returns></returns>
        private string GetName()
        {
            return Environment.MachineName;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ///
            //При закрытии чата посылаем сообщение всем о нашем уходе и выключаем слушатель
            ///
            IsThreadRunning = false;
            String message = "Stop$" + GetName();
            MulticastSend(MCASTADDR, MCASTPORT, message);
        }

    }
}