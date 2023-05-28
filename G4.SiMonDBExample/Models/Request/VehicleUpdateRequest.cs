namespace G4.SiMonDBExample.Models.Request {
    public class VehicleUpdateRequest {

        public string _id { get; set; } = "";
        public string Model { get; set; } = "";
        public string Brand { get; set; } = "";

        public VehicleUpdateRequest() { }

    }
}
