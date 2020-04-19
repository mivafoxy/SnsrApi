using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnsrApi.Models
{
    public class DeviceDescriptionModel
    {
        public string DeviceModelName { get; set; }
        public int DeviceModelType { get; set; }
        public string DeviceUuid { get; set; }
    }
}
