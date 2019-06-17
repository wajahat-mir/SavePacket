using SavePacket.ViewModels;
using SharpPcap;
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

namespace SavePacket.UserControls
{
    /// <summary>
    /// Interaction logic for DeviceButton.xaml
    /// </summary>
    public partial class DeviceButton : UserControl
    {
        public ICaptureDevice _device { get; set; }
        public ApplicationViewModel _applicationViewModel { get; set; }

        public DeviceButton(ICaptureDevice device, ApplicationViewModel applicationViewModel)
        {
            InitializeComponent();
            _device = device;
            _applicationViewModel = applicationViewModel;

            AButton.Content = device.Description;
            AButton.Click += DeviceButton_Click;
        }

        private void DeviceButton_Click(object sender, RoutedEventArgs e)
        {
            _applicationViewModel.selectedDevice = _device;
            _applicationViewModel.lblInterfaceDescription = _device.Description;
            _applicationViewModel.InterfacePnlVisibility = Visibility.Collapsed;
            _applicationViewModel.StopCaptureVisibility = Visibility.Visible;

            _applicationViewModel.selectedDevice.OnPacketArrival +=
               new SharpPcap.PacketArrivalEventHandler(_applicationViewModel.device_OnPacketArrival);

            _applicationViewModel.selectedDevice.Open();
            _applicationViewModel.selectedDevice.StartCapture();
        }
    }
}
