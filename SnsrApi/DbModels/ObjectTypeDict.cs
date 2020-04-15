using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ObjectTypeDict
    {
        public ObjectTypeDict()
        {
            ObjectDict = new HashSet<ObjectDict>();
        }

        public int IdKey { get; set; }
        public string ObjectTypeName { get; set; }

        public virtual ICollection<ObjectDict> ObjectDict { get; set; }
    }
}
