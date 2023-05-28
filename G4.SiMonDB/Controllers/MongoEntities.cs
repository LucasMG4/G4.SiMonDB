using G4.SiMonDB.Models;

namespace G4.SiMonDB.Controllers {
    public class MongoEntities {

        private List<MongoEntityRegister> entities = new List<MongoEntityRegister>();

        internal MongoEntities() { }

        public MongoEntityRegister? Get<Entity>() => entities.Where(x => x.EntityType == typeof(Entity)).FirstOrDefault();
        public void Remove<Entity>() => entities = entities.Where(x => x.EntityType != typeof(Entity)).ToList();


        public void Add<Entity>(string database, string collection) where Entity : MongoEntity {

            var exists = entities.Where(x => x.EntityType == typeof(Entity)).Count() > 0;

            if (exists)
                throw new Exception($"Entity of type '{typeof(Entity).FullName}' is already registered.");

            var newRegister = new MongoEntityRegister(typeof(Entity), database, collection);

            entities.Add(newRegister);

        }

        public void Clear() => entities = new List<MongoEntityRegister>();

    }
}
