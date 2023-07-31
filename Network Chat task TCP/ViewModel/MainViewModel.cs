using Network_Chat_task_TCP.Commands;
using Network_Chat_task_TCP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

        static TcpClient _client = null;

        public MainViewModel()
        {
            ConnectServerCommand = new RelayCommand((_) =>
            {
                Task.Run(() =>
                {
                    User user = new User();
                    user.Name = UserEnterName;

                    string hostName = Dns.GetHostName();

                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                    var ipAddress = IPAddress.Parse(myIP);

                    var ep = new IPEndPoint(ipAddress, 27001);

                    user.EndPoint = ep;

                    MessageBox.Show($"{user.Name} - {user.EndPoint}");


                    var ipAdressRemote = IPAddress.Parse("10.2.11.3");
                    var port = 27001;

                    var endPointRemote = new IPEndPoint(ipAdressRemote, port);

                    _client = new TcpClient();

                    _client.Connect(endPointRemote);

                    var stream = _client.GetStream();
                    var bw = new BinaryWriter(stream);
                    bw.Write(user.Name);

                });


            });
        }
    }
}
