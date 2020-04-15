using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ModelType
    {
        public ModelType()
        {
            Device = new HashSet<Device>();
            Model = new HashSet<Model>();
        }

        public int IdKey { get; set; }
        public string ModelTypeName { get; set; }

        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<Model> Model { get; set; }
    }
}
