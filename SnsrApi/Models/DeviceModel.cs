using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnsrApi.Models
{
    public class DeviceModel
    {
        public int DeviceModelType { get; set; }
        public string DeviceSerial { get; set; }
        public string DeviceModelName { get; set; }
        public List<ObjectItem> ObjectItems { get; set; } = new List<ObjectItem>();
    }
}
