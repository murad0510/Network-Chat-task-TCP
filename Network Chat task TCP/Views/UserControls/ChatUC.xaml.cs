﻿using Network_Chat_task_TCP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Network_Chat_task_TCP.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ChatUC.xaml
    /// </summary>
    public partial class ChatUC : UserControl
    {
        [Obsolete]
        public ChatUC()
        {
            InitializeComponent();
            App.UserMessageStackPanel = UserMessageWrapPanel;
            ChatUcViewModel chatUcViewModel = new ChatUcViewModel();
            this.DataContext = chatUcViewModel;
        }
    }
}
