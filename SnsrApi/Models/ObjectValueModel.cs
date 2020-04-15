using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnsrApi.Models
{
    public class ObjectValueModel
    {
        public int ObjectId { get; set; }
        public string ObjectValue { get; set; }
        public DateTime ObjectReceiveTime { get; set; }
    }
}
