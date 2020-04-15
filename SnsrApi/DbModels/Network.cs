using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class Network
    {
        public int IdKey { get; set; }
        public string LeftDeviceId { get; set; }
        public string RightDeviceId { get; set; }

        public virtual Device LeftDevice { get; set; }
        public virtual Device RightDevice { get; set; }
    }
}
