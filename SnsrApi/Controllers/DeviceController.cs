using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SnsrApi.DbModels;
using SnsrApi.Models;

namespace SnsrApi.Controllers
{
    [Route("api/Device")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly SnsrContext _context;

        public DeviceController(SnsrContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<DeviceModel> GetDevices()
        {
            var logicalsQuery =
                from ld in _context.Set<DeviceLogical>()
                join dev in _context.Set<Device>() on ld.DeviceFkey equals dev.IdKey
                join mld in _context.Set<ModelLogicalDevice>() on ld.ModelLogicalDevice equals mld.IdKey
                where dev.DeviceType == 1 && mld.LdType == 1
                select new { ld.IdKey, dev.SerialNumber, dev.DeviceType };

            var deviceModels = new List<DeviceModel>();

            foreach (var logical in logicalsQuery.ToList())
            {
                var objectsForLogicalQuery =
                    from dob in _context.Set<DeviceObject>()
                    where dob.DeviceLdFkey.Equals(logical.IdKey)
                    select new ObjectItem { ObjectId = dob.ObjectDictId };

                var deviceModel = 
                    new DeviceModel 
                    { 
                        ObjectItems = objectsForLogicalQuery.ToList(),
                        DeviceSerial = logical.SerialNumber,
                        DeviceModelType = logical.DeviceType
                    };

                deviceModels.Add(deviceModel);
            }

            return deviceModels;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
