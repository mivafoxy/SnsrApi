using System;
using System.Collections.Generic;

namespace SnsrApi.DbModels
{
    public partial class ProfileNetwork
    {
        public string IdKey { get; set; }
        public string LeftProfileId { get; set; }
        public string RightProfileId { get; set; }

        public virtual DeviceProfile LeftProfile { get; set; }
        public virtual DeviceProfile RightProfile { get; set; }
    }
}
