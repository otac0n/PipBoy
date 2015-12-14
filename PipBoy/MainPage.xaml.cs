// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy
{
    using System.Net;
    using PipBoy.Middleware;
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
            this.DataContext = new ViewModel(new DnsEndPoint("localhost", 27000));
        }

        public sealed class ViewModel : ClientViewModel
        {
            public ViewModel(EndPoint endPoint)
                : base(endPoint)
            {
                this.Nurse = new Middleware.Nurse(this);
            }

            public Nurse Nurse { get; }
        }
    }
}
