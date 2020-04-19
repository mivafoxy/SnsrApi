using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ProfileNetwork
    {
        public string IdKey { get; set; }
        public string ParentProfileId { get; set; }
        public string ChildProfileId { get; set; }

        public virtual DeviceProfile ChildProfile { get; set; }
        public virtual DeviceProfile ParentProfile { get; set; }
    }
}
