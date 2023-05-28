using G4.SiMonDB.Models;

namespace G4.SiMonDBExample.Models {
    public class Vehicle : MongoEntity {

        public string Model { get; set; } = "";
        public string Brand { get; set; } = "";

        public Vehicle() { }

    }
}
