using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class Device
    {
        public Device()
        {
            DeviceLogical = new HashSet<DeviceLogical>();
            NetworkChildDevice = new HashSet<Network>();
            NetworkParentDevice = new HashSet<Network>();
        }

        public string IdKey { get; set; }
        public string ModelFkey { get; set; }
        public string MainLogicalDevice { get; set; }
        public string SerialNumber { get; set; }
        public int DeviceType { get; set; }

        public virtual ModelType DeviceTypeNavigation { get; set; }
        public virtual DeviceLogical MainLogicalDeviceNavigation { get; set; }
        public virtual Model ModelFkeyNavigation { get; set; }
        public virtual ICollection<DeviceLogical> DeviceLogical { get; set; }
        public virtual ICollection<Network> NetworkChildDevice { get; set; }
        public virtual ICollection<Network> NetworkParentDevice { get; set; }
    }
}
