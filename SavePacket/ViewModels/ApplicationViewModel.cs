using AutoMapper;
using PacketDotNet;
using SavePacket.Models;
using SavePacket.Service;
using SharpPcap;
using System;
using System.ComponentModel;

namespace SavePacket.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ICaptureDevice selectedDevice;
        private IInterfaceService _interfaceService;
        public CaptureDeviceList interfaces;
        public Models.Packet packet;
        private IDBService _dBService;

        public ApplicationViewModel(IInterfaceService interfaceService, IDBService dbService)
        {
            _dBService = dbService;
            _interfaceService = interfaceService;
            interfaces = _interfaceService.GetDevices();
            packet = new Models.Packet();
        }

        private System.Windows.Visibility interfacePnlVisibility;
        public System.Windows.Visibility InterfacePnlVisibility
        {
            get
            {
                return interfacePnlVisibility;
            }
            set
            {
                interfacePnlVisibility = value;
                RaisePropertyChanged("InterfacePnlVisibility");
            }
        }

        private System.Windows.Visibility stopCaptureVisibility = System.Windows.Visibility.Hidden;
        public System.Windows.Visibility StopCaptureVisibility
        {
            get
            {
                return stopCaptureVisibility;
            }
            set
            {
                stopCaptureVisibility = value;
                RaisePropertyChanged("StopCaptureVisibility");
            }
        }

        private string interfaceDescription;
        public string lblInterfaceDescription
        {
            get
            {
                return interfaceDescription;
            }
            set
            {
                interfaceDescription = value;
                RaisePropertyChanged("lblInterfaceDescription");
            }
        }

        public string PacketLength
        {
            get { return packet.Length.ToString(); }
            set
            {
                if (value != "")
                    packet.Length = Convert.ToInt64(value);
                else
                    packet.Length = 0;
                RaisePropertyChanged("PacketLength");
            }
        }

        public string PacketTime
        {
            get { return packet.Time.ToString(); }
            set
            {
                if (value != "")
                    packet.Time = Convert.ToDateTime(value);
                else
                    packet.Time = DateTime.Now;
                RaisePropertyChanged("PacketTime");
            }
        }

        public string PacketIP
        {
            get { return packet.SourceIP; }
            set
            {
                packet.SourceIP = value;
                RaisePropertyChanged("PacketIP");
            }
        }

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void device_OnPacketArrival(object sender, CaptureEventArgs capture)
        {
            var packetdotnet = PacketDotNet.Packet.ParsePacket(capture.Packet.LinkLayerType, capture.Packet.Data);
            var tcp = packetdotnet.Extract<TcpPacket>();

            if (tcp != null)
            {
                PacketTime = capture.Packet.Timeval.Date.ToString();
                PacketLength = capture.Packet.Data.Length.ToString();
                PacketIP = ((IPPacket)tcp.ParentPacket).SourceAddress.ToString();

                _dBService.Add(this.packet);
            }
        }
    }
}
