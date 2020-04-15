using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class DeviceLogical
    {
        public DeviceLogical()
        {
            Device = new HashSet<Device>();
            DeviceObject = new HashSet<DeviceObject>();
            DeviceProfile = new HashSet<DeviceProfile>();
        }

        public string IdKey { get; set; }
        public string DeviceFkey { get; set; }
        public string ModelLogicalDevice { get; set; }

        public virtual Device DeviceFkeyNavigation { get; set; }
        public virtual ModelLogicalDevice ModelLogicalDeviceNavigation { get; set; }
        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<DeviceObject> DeviceObject { get; set; }
        public virtual ICollection<DeviceProfile> DeviceProfile { get; set; }
    }
}
