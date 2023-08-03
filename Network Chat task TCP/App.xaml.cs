using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Network_Chat_task_TCP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static WrapPanel MainChatWrapPanel { get; set; }
        public static WrapPanel UserMessageStackPanel { get; set; }
    }
}
