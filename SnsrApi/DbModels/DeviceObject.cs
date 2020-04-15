using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class DeviceObject
    {
        public DeviceObject()
        {
            DeviceObjectValue = new HashSet<DeviceObjectValue>();
        }

        public string IdKey { get; set; }
        public string DeviceProfileFkey { get; set; }
        public string DeviceLdFkey { get; set; }
        public string ModelObjectFkey { get; set; }
        public string StartValue { get; set; }
        public int ObjectDictId { get; set; }

        public virtual DeviceLogical DeviceLdFkeyNavigation { get; set; }
        public virtual DeviceProfile DeviceProfileFkeyNavigation { get; set; }
        public virtual ModelLogicalDeviceObject ModelObjectFkeyNavigation { get; set; }
        public virtual ObjectDict ObjectDict { get; set; }
        public virtual ICollection<DeviceObjectValue> DeviceObjectValue { get; set; }
    }
}
