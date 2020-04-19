using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class Network
    {
        public int IdKey { get; set; }
        public string ParentDeviceId { get; set; }
        public string ChildDeviceId { get; set; }

        public virtual Device ChildDevice { get; set; }
        public virtual Device ParentDevice { get; set; }
    }
}
