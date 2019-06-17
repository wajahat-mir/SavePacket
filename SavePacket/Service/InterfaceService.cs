using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavePacket.Service
{
    class InterfaceService : IInterfaceService
    {
        public CaptureDeviceList GetDevices()
        {
            return CaptureDeviceList.Instance;
        }
    }

    public interface IInterfaceService
    {
        CaptureDeviceList GetDevices();
    }
}
