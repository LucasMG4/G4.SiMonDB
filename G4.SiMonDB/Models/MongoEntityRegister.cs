namespace G4.SiMonDB.Models {
    public class MongoEntityRegister {

        public Type EntityType { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }

        internal MongoEntityRegister(Type entityType, string database, string collection) {
            this.EntityType = entityType;
            this.Database = database;
            this.Collection = collection;
        }

    }
}
