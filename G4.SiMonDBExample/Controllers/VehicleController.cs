using Amazon.Runtime.Internal;
using G4.SiMonDBExample.Models;
using G4.SiMonDBExample.Models.Request;
using G4.SiMonDBExample.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace G4.SiMonDBExample.Controllers {

    [ApiController]
    [Route("api/v1")]
    public class VehicleController {

        private readonly VehicleService vehicleService;

        public VehicleController(VehicleService vehicleService) {
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        [Route("vehicle")]
        public List<Vehicle> List() => vehicleService.List();

        [HttpGet]
        [Route("vehicle/{id}")]
        public Vehicle? Get(string id) {

            var idParse = ObjectId.Parse(id);
            return vehicleService.Get(idParse);

        }

        [HttpPost]
        [Route("vehicle")]
        public void Add(Vehicle vehicle) {

           

            vehicleService.Add(vehicle);
        }

        [HttpPut]
        [Route("vehicle")]
        public bool Update(VehicleUpdateRequest request) {

            var vehicle = new Vehicle() {
                _id = ObjectId.Parse(request._id),
                Brand = request.Brand,
                Model = request.Model,
            };

            return vehicleService.Update(vehicle);
        }

        [HttpDelete]
        [Route("vehicle/{id}")]
        public bool Update(string id) {

            var idParse = ObjectId.Parse(id);
            return vehicleService.Delete(idParse);

        }


    }
}
