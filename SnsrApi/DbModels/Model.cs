using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class Model
    {
        public Model()
        {
            Device = new HashSet<Device>();
            ModelLogicalDevice = new HashSet<ModelLogicalDevice>();
        }

        public int ModelTypeFkey { get; set; }
        public string IdKey { get; set; }

        public virtual ModelType ModelTypeFkeyNavigation { get; set; }
        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<ModelLogicalDevice> ModelLogicalDevice { get; set; }
    }
}
