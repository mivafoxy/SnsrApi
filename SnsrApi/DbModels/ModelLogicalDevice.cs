using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ModelLogicalDevice
    {
        public ModelLogicalDevice()
        {
            DeviceLogical = new HashSet<DeviceLogical>();
            ModelLogicalDeviceObject = new HashSet<ModelLogicalDeviceObject>();
            ModelProfile = new HashSet<ModelProfile>();
        }

        public string IdKey { get; set; }
        public string ModelFkey { get; set; }
        public int LdType { get; set; }

        public virtual ModelLogicalType LdTypeNavigation { get; set; }
        public virtual Model ModelFkeyNavigation { get; set; }
        public virtual ICollection<DeviceLogical> DeviceLogical { get; set; }
        public virtual ICollection<ModelLogicalDeviceObject> ModelLogicalDeviceObject { get; set; }
        public virtual ICollection<ModelProfile> ModelProfile { get; set; }
    }
}
