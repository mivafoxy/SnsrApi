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
        private readonly snsrContext _context;

        public DeviceController(snsrContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<DeviceModel> GetDevices()
        {
            var devicesQuery =
                from dev in _context.Set<Device>()
                join m in _context.Set<Model>() on dev.ModelFkey equals m.IdKey
                select new 
                { 
                    dev.IdKey,
                    dev.SerialNumber,
                    dev.DeviceType,
                    dev.ModelFkey,
                    m.ModelName
                };

            var deviceModels = new List<DeviceModel>();

            foreach (var device in devicesQuery.ToList())
            {
                var logicalsQuery =
                from ld in _context.Set<DeviceLogical>()
                join dev in _context.Set<Device>() on ld.DeviceFkey equals dev.IdKey
                join mld in _context.Set<ModelLogicalDevice>() on ld.ModelLogicalDevice equals mld.IdKey
                join m in _context.Set<Model>() on dev.ModelFkey equals m.IdKey
                where dev.IdKey == device.IdKey
                select new
                {
                    ld.IdKey,
                    dev.DeviceType,
                    dev.ModelFkey,
                    m.ModelName
                };

                var objectItems = new List<ObjectItem>();

                foreach (var logical in logicalsQuery.ToList())
                {
                    var objectsForLogicalQuery =
                        from dob in _context.Set<DeviceObject>()
                        from mldo in _context.Set<ModelLogicalDeviceObject>()
                        where dob.DeviceLdFkey.Equals(logical.IdKey) && mldo.IdKey.Equals(dob.ModelObjectFkey)
                        select new ObjectItem
                        {
                            ObjectId = dob.ObjectDictId,
                            ObjectInitValue = dob.StartValue,
                            IsShown = mldo.IsShown,
                            IsEditable = mldo.IsEditable,
                            IsInitable = mldo.IsInitable
                        };

                    objectItems.AddRange(objectsForLogicalQuery.ToList());
                }

                var deviceModel =
                        new DeviceModel
                        {
                            ObjectItems = objectItems,
                            DeviceSerial = device.SerialNumber,
                            DeviceModelType = device.DeviceType,
                            DeviceModelUuid = device.ModelFkey,
                            DeviceModelName = device.ModelName
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

        [HttpPost]
        public void CreateNewDevice(DeviceModel deviceModel)
        {
            // Get model
            var modelLogicals =
                from m in _context.Set<Model>()
                from ml in _context.Set<ModelLogicalDevice>()
                where m.IdKey.Equals(deviceModel.DeviceModelUuid) &&
                m.IdKey.Equals(ml.ModelFkey)
                select new { ml.LdType, ml.IdKey };

            var newDevice =
                new Device
                {
                    IdKey = System.Guid.NewGuid().ToString(),
                    SerialNumber = deviceModel.DeviceSerial,
                    ModelFkey = deviceModel.DeviceModelUuid,
                    DeviceType = deviceModel.DeviceModelType
                };

            //
            // Пока возможен только один главный логический прибор
            // с одним профилем связи.
            //

            var mainLogical =
                (from ml in modelLogicals
                where ml.LdType == 0 
                select new { ml.IdKey }).ToList()[0];

            var newMainDeviceLogical =
                new DeviceLogical
                {
                    IdKey = System.Guid.NewGuid().ToString(),
                    DeviceFkey = newDevice.IdKey,
                    ModelLogicalDevice = mainLogical.IdKey
                };


            // Должна произойти доработка модели в БД.

            //var mainModelProfileId =
            //    (from mp in _context.Set<ModelProfile>()
            //     where mp.ModelLdFkey == mainLogical.IdKey
            //     select new { mp.IdKey }).First().IdKey;

            //var newCommunicationProfile =
            //    new DeviceProfile
            //    {
            //        IdKey = System.Guid.NewGuid().ToString(),
            //        DeviceLdFkey = newMainDeviceLogical.IdKey,
            //        ModelProfileFkey = mainModelProfileId
            //    };


            //_context.DeviceLogical.Add(newMainDeviceLogical);
            //_context.DeviceProfile.Add(newCommunicationProfile);

            //newMainDeviceLogical.DeviceProfile.Add(newCommunicationProfile);
            
            _context.Device.Add(newDevice);
            _context.SaveChanges();

            _context.DeviceLogical.Add(newMainDeviceLogical);
            _context.SaveChanges();

            newDevice.MainLogicalDevice = newMainDeviceLogical.IdKey;
            _context.SaveChanges();



            //
            // Связь возможна тоже только по интернету.
            //

            //var commObjects =
            //    from mldob in _context.Set<ModelLogicalDeviceObject>()
            //    where (mldob.ObjectId == 0 || mldob.ObjectId == 1 || mldob.ObjectId == 2) &&
            //    mldob.ModelLdFkey.Equals(mainLogical.IdKey)
            //    select new { mldob.ObjectId, mldob.IdKey };

            //foreach (var profileObject in commObjects.ToList())
            //{
            //    if (profileObject.ObjectId == 0)
            //    {
            //        var serialObject =
            //            new DeviceObject
            //            {
            //                IdKey = System.Guid.NewGuid().ToString(),
            //                DeviceProfileFkey = newCommunicationProfile.IdKey,
            //                DeviceLdFkey = newMainDeviceLogical.IdKey,
            //                ModelObjectFkey = profileObject.IdKey,
            //                StartValue = deviceModel.DeviceSerial,
            //                ObjectDictId = 0
            //            };

            //        _context.DeviceObject.Add(serialObject);
            //    }
            //    else if (profileObject.ObjectId == 1)
            //    {
            //        var hostObject =
            //            new DeviceObject
            //            {
            //                IdKey = System.Guid.NewGuid().ToString(),
            //                DeviceProfileFkey = newCommunicationProfile.IdKey,
            //                DeviceLdFkey = newMainDeviceLogical.IdKey,
            //                ModelObjectFkey = profileObject.IdKey,
            //                StartValue = "127.0.0.1", // standard home host.
            //                ObjectDictId = 1
            //            };

            //        _context.DeviceObject.Add(hostObject);
            //    }
            //    else if (profileObject.ObjectId == 2)
            //    {
            //        var portObject =
            //            new DeviceObject
            //            {
            //                IdKey = System.Guid.NewGuid().ToString(),
            //                DeviceProfileFkey = newCommunicationProfile.IdKey,
            //                DeviceLdFkey = newMainDeviceLogical.IdKey,
            //                ModelObjectFkey = profileObject.IdKey,
            //                StartValue = "40400", // standard home port.
            //                ObjectDictId = 2
            //            };

            //        _context.DeviceObject.Add(portObject);
            //    }
            //}

            //
            // Задача с сетью на будущее.
            //

            foreach (var modelLogical in modelLogicals.ToList())
            {
                var newDeviceLogical =
                    new DeviceLogical
                    {
                        IdKey = System.Guid.NewGuid().ToString(),
                        DeviceFkey = newDevice.IdKey,
                        ModelLogicalDevice = modelLogical.IdKey
                    };

                var logicalObjects =
                    from mldo in _context.Set<ModelLogicalDeviceObject>()
                    where mldo.ModelLdFkey == modelLogical.IdKey
                    select new
                    {
                        mldo.ObjectId,
                        mldo.IsEditable,
                        mldo.IsInitable,
                        mldo.IsShown,
                        mldo.IdKey
                    };

                foreach (var logicalObject in logicalObjects.ToList())
                {
                    var newObject = new DeviceObject
                    {
                        IdKey = System.Guid.NewGuid().ToString(),
                        DeviceLdFkey = newDeviceLogical.IdKey,
                        ModelObjectFkey = logicalObject.IdKey,
                        ObjectDictId = deviceModel.ObjectItems.First(obj => obj.ObjectId == logicalObject.ObjectId).ObjectId
                    };

                    if (logicalObject.IsInitable)
                        newObject.StartValue = deviceModel.ObjectItems.First(obj => obj.ObjectId == logicalObject.ObjectId).ObjectInitValue;

                    newDeviceLogical.DeviceObject.Add(newObject);
                }

                newDevice.DeviceLogical.Add(newDeviceLogical);
            }

            _context.SaveChanges();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
