using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnsrApi.Models
{
    public class ObjectItem
    {
        public int ObjectId { get; set; }
        public string ObjectInitValue { get; set; }
        public bool IsEditable { get; set; }
        public bool IsInitable { get; set; }
        public bool IsShown { get; set; }
    }
}
