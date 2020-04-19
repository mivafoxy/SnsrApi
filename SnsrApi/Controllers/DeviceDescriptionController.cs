using Microsoft.AspNetCore.Mvc;
using SnsrApi.DbModels;
using SnsrApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnsrApi.Controllers
{
    [Route("api/DeviceDescriptions")]
    [ApiController]
    public class DeviceDescriptionController
    {
        private readonly SnsrContext context;

        public DeviceDescriptionController(SnsrContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<DeviceDescriptionModel> GetModelDescriptions()
        {
            var modelsQuery =
                from m in context.Set<Model>()
                select new DeviceDescriptionModel { DeviceUuid = m.IdKey, DeviceModelName = "Stub name", DeviceModelType = m.ModelTypeFkey };

            return modelsQuery.ToList();
        }
    }
}
