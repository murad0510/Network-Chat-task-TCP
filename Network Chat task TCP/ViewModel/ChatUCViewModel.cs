using Network_Chat_task_TCP.Commands;
using Network_Chat_task_TCP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Network_Chat_task_TCP.ViewModel
{
    public class ChatUcViewModel : BaseViewModel
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        private string userMessage;

        public string UserMessage
        {
            get { return userMessage; }
            set { userMessage = value; OnPropertyChanged(); }
        }

        public RelayCommand SendCommand { get; set; }

        [Obsolete]
        public ChatUcViewModel()
        {
            SendCommand = new RelayCommand((d) =>
            {
                MessageBox.Show("s");

                //string hostName = Dns.GetHostName();

                //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                //var ipAddress = IPAddress.Parse("10.2.27.3");

                //var ep = new IPEndPoint(ipAddress, 27001);

                //user.EndPoint = ep.ToString();

                //var jsonString = JsonConvert.SerializeObject(user);

                //MessageBox.Show($"{user.Name} - {user.EndPoint}");

                var ipAdressRemote = IPAddress.Parse("10.2.27.3");
                var port = 27001;

                var endPointRemote = new IPEndPoint(ipAdressRemote, port);

                var _client = new TcpClient();

                Task.Run(() =>
                {
                    try
                    {
                        _client.Connect(endPointRemote);
                        if (_client.Connected)
                        {
                            var write = Task.Run(() =>
                            {
                                while (true)
                                {
                                    if (UserMessage != string.Empty)
                                    {
                                        var stream = _client.GetStream();
                                        var bw = new BinaryWriter(stream);
                                        bw.Write(UserMessage);
                                        UserMessage = string.Empty;
                                    }
                                }
                            });

                            var reader = Task.Run(() =>
                            {
                                while (true)
                                {
                                    var stream = _client.GetStream();
                                    var br = new BinaryReader(stream);
                                    var msg = br.ReadString();
                                    MessageBox.Show($"{UserName} - {msg}");
                                    //UserName = msg;
                                };
                            });

                            Task.WaitAll(write, reader);
                        }
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.Message);
                    }

                });
            });
        }

    }
}
