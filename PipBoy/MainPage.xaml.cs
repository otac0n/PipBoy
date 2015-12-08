using System;
using System.Net;
using PipBoy.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace PipBoy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = new ClientViewModel(new IPEndPoint(IPAddress.Parse("192.168.0.3"), 27000));
        }
    }
}

