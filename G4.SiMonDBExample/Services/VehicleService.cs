using G4.SiMonDB.Controllers;
using G4.SiMonDBExample.Models;
using MongoDB.Bson;

namespace G4.SiMonDBExample.Services {
    public class VehicleService {

        private readonly SiContext siContext;
        private SiRepository<Vehicle> repository;

        public VehicleService(SiContext siContext) {
            this.siContext = siContext;
            this.repository = siContext.BuildRepository<Vehicle>();
        }

        public List<Vehicle> List() => repository.Entities.ToList();
        public Vehicle? Get(ObjectId id) => repository.Entities.Where(x => x._id == id).FirstOrDefault();
        public void Add(Vehicle vehicle) => repository.Add(vehicle);
        public bool Update(Vehicle vehicle) => repository.Update(vehicle);
        public bool Delete(ObjectId id) => repository.Delete(new Vehicle() { _id = id });



    }
}
