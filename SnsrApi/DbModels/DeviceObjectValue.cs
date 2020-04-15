using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class DeviceObjectValue
    {
        public string IdKey { get; set; }
        public string DeviceObjectFkey { get; set; }
        public string ObjectValue { get; set; }
        public DateTime ReceiveTime { get; set; }

        public virtual DeviceObject DeviceObjectFkeyNavigation { get; set; }
    }
}
