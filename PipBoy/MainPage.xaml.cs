// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy
{
    using System.Net;
    using PipBoy.Protocol.ViewModels;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = new ClientViewModel(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27000));
        }
    }
}
