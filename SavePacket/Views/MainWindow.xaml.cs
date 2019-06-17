using PacketDotNet;
using SavePacket.Service;
using SavePacket.UserControls;
using SavePacket.ViewModels;
using SharpPcap;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SavePacket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {           
        private ApplicationViewModel _applicationViewModel;

        public MainWindow(ApplicationViewModel applicationViewModel)
        {
            InitializeComponent();
            _applicationViewModel = applicationViewModel;
            this.DataContext = _applicationViewModel;

            var devices = _applicationViewModel.interfaces;
            if (devices.Count < 1)
            {
                Label noDevices = new Label();
                noDevices.Content = "No Devices found";
                InterfacePanel.Children.Add(noDevices);
            }
            else
            {
                foreach (ICaptureDevice device in devices)
                {
                    DeviceButton deviceButton = new DeviceButton(device, _applicationViewModel);
                    InterfacePanel.Children.Add(deviceButton);
                }
            }   
        }
        
        private void EndCapture_Click(object sender, RoutedEventArgs e)
        {
            _applicationViewModel.selectedDevice.StopCapture();
            _applicationViewModel.selectedDevice.Close();

            _applicationViewModel.InterfacePnlVisibility = Visibility.Visible;
            _applicationViewModel.StopCaptureVisibility = Visibility.Hidden;
            _applicationViewModel.lblInterfaceDescription = "";
            _applicationViewModel.PacketTime = "";
            _applicationViewModel.PacketLength = "";
            _applicationViewModel.PacketIP = "";
        }



    }
}
