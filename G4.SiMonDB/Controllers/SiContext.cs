using G4.SiMonDB.Models;
using MongoDB.Driver;

namespace G4.SiMonDB.Controllers {
    public class SiContext {

        private string? ConnectionString;
        public MongoEntities EntitiesRegister;

        public SiContext() {
            EntitiesRegister = new MongoEntities();
        }

        public void SetConnectionString(string connectionString) => ConnectionString = connectionString;

        private IMongoClient CreateClient() => new MongoClient(ConnectionString);
        private IMongoDatabase GetDatabase(string database) => CreateClient().GetDatabase(database);
        private IMongoCollection<Entity> GetCollection<Entity>(string database, string collection) => GetDatabase(database).GetCollection<Entity>(collection);

        public SiRepository<Entity> BuildRepository<Entity>() where Entity : MongoEntity {

            var register = EntitiesRegister.Get<Entity>();

            if (register == null)
                throw new Exception($"Entity '{typeof(Entity).FullName}' is not registred on SiContext.EntitiesRegister.");

            var collection = GetCollection<Entity>(register.Database, register.Collection);

            return new SiRepository<Entity>(collection);

        }


    }
}
