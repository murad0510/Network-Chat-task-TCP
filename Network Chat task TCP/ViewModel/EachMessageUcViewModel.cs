using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_Chat_task_TCP.ViewModel
{
    public class EachMessageUcViewModel : BaseViewModel
    {
        private string message;

        public string Msg
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }
    }
}
