using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ModelProfile
    {
        public ModelProfile()
        {
            DeviceProfile = new HashSet<DeviceProfile>();
            ModelLogicalDeviceObject = new HashSet<ModelLogicalDeviceObject>();
        }

        public string IdKey { get; set; }
        public string ModelLdFkey { get; set; }
        public int ProfileType { get; set; }

        public virtual ModelLogicalDevice ModelLdFkeyNavigation { get; set; }
        public virtual ProfileType ProfileTypeNavigation { get; set; }
        public virtual ICollection<DeviceProfile> DeviceProfile { get; set; }
        public virtual ICollection<ModelLogicalDeviceObject> ModelLogicalDeviceObject { get; set; }
    }
}
