using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ModelLogicalType
    {
        public ModelLogicalType()
        {
            ModelLogicalDevice = new HashSet<ModelLogicalDevice>();
        }

        public int IdKey { get; set; }
        public string LdTypeName { get; set; }

        public virtual ICollection<ModelLogicalDevice> ModelLogicalDevice { get; set; }
    }
}
