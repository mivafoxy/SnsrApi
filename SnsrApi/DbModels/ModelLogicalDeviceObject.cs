using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ModelLogicalDeviceObject
    {
        public ModelLogicalDeviceObject()
        {
            DeviceObject = new HashSet<DeviceObject>();
        }

        public string IdKey { get; set; }
        public string ModelLdFkey { get; set; }
        public string ModelProfileFkey { get; set; }
        public int ObjectId { get; set; }

        public virtual ModelLogicalDevice ModelLdFkeyNavigation { get; set; }
        public virtual ModelProfile ModelProfileFkeyNavigation { get; set; }
        public virtual ObjectDict Object { get; set; }
        public virtual ICollection<DeviceObject> DeviceObject { get; set; }
    }
}
