using MongoDB.Bson;

namespace G4.SiMonDB.Models {
    public abstract class MongoEntity {

        public ObjectId _id { get; set; }

    }
}
