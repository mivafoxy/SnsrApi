using System;
using System.Collections.Generic;
using System.Globalization;
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


        [HttpGet("deviceId={deviceId}&objectId={objectId}&startDate={startDate}&endDate={endDate}")]
        public IEnumerable<ObjectValueModel> GetDeviceValues(
            string deviceId,
            int objectId,
            string startDate, // example: 20200120000000
            string endDate  // example: 20200125000000
            )
        {
            var fromDt = DateTime.ParseExact(startDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var toDt = DateTime.ParseExact(endDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            var deviceValues =
                from devals in _context.Set<DeviceObjectValue>()
                join dev in _context.Set<Device>() on deviceId equals dev.SerialNumber
                join dl in _context.Set<DeviceLogical>() on dev.IdKey equals dl.DeviceFkey
                join dob in _context.Set<DeviceObject>() on dl.IdKey equals dob.DeviceLdFkey
                where (devals.ReceiveTime.CompareTo(fromDt) >= 0) &&
                devals.ReceiveTime.CompareTo(toDt) <= 1 &&
                dob.ObjectDictId == objectId
                select
                new ObjectValueModel
                {
                    ObjectId = dob.ObjectDictId,
                    ObjectReceiveTime = devals.ReceiveTime,
                    ObjectValue = devals.ObjectValue
                };

            var result = deviceValues.ToList();

            Console.WriteLine(result);

            return deviceValues.ToList();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
