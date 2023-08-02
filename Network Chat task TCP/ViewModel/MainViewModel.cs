using Network_Chat_task_TCP.Commands;
using Network_Chat_task_TCP.Helpers;
using Network_Chat_task_TCP.Models;
using Network_Chat_task_TCP.Views.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Network_Chat_task_TCP.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand ConnectServerCommand { get; set; }

        private string userEnterName;

        public string UserEnterName
        {
            get { return userEnterName; }
            set { userEnterName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<User> allUsers;

        public ObservableCollection<User> AllUsers
        {
            get { return allUsers; }
            set { allUsers = value; }
        }

        [Obsolete]
        public MainViewModel()
        {
            ConnectServerCommand = new RelayCommand((_) =>
            {
                ChatUC chatUC = new ChatUC();
                App.MainChatWrapPanel.Children.Add(chatUC);
                User user = new User();
                user.Name = UserEnterName;

                string hostName = Dns.GetHostName();

                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                var ipAddress = IPAddress.Parse(myIP);

                var ep = new IPEndPoint(ipAddress, 27001);

                user.EndPoint = ep.ToString();

                var jsonString = JsonConvert.SerializeObject(user);

                MessageBox.Show($"{user.Name} - {user.EndPoint}");

                var ipAdressRemote = IPAddress.Parse("10.1.18.3");
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
                                    if (jsonString != null)
                                    {
                                        var stream = _client.GetStream();
                                        var bw = new BinaryWriter(stream);
                                        bw.Write(jsonString);
                                        jsonString = null;
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
                                    MessageBox.Show(msg);
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
