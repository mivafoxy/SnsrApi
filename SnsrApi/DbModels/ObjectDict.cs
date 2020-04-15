using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ObjectDict
    {
        public ObjectDict()
        {
            DeviceObject = new HashSet<DeviceObject>();
            ModelLogicalDeviceObject = new HashSet<ModelLogicalDeviceObject>();
        }

        public int IdKey { get; set; }
        public string ObjectName { get; set; }
        public int ObjectType { get; set; }

        public virtual ObjectTypeDict ObjectTypeNavigation { get; set; }
        public virtual ICollection<DeviceObject> DeviceObject { get; set; }
        public virtual ICollection<ModelLogicalDeviceObject> ModelLogicalDeviceObject { get; set; }
    }
}
