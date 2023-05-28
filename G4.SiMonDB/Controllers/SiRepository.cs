using G4.SiMonDB.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace G4.SiMonDB.Controllers {
    public class SiRepository<Entity> where Entity : MongoEntity {

        private IMongoCollection<Entity> Collection;

        public IMongoQueryable<Entity> Entities { get { return Collection.AsQueryable(); } }

        internal SiRepository(IMongoCollection<Entity> collection) {

            Collection = collection;

        }

        public void Add(Entity entity) => Collection.InsertOne(entity);
        public void AddMany(Entity[] entities) => Collection.InsertMany(entities);


        public bool Update(Entity entity) {

            var old = Entities.Where(x => x._id == entity._id).FirstOrDefault();

            if (old == null)
                throw new NullReferenceException($"entity with id ({entity._id.ToString()}) not found on database !");

            var updateDefinition = SiTools.CreateUpdate<Entity>(old, entity);
            var filter = Builders<Entity>.Filter.Eq("_id", old._id);

            if (updateDefinition == null)
                throw new Exception("No changes find for the entity.");

            var result = Collection.UpdateOne(filter, updateDefinition);

            return result.ModifiedCount > 0;

        }

        public long UpdateMany(object value, Expression<Func<Entity, bool>> where) {

            var filter = Builders<Entity>.Filter.Where(where);

            var properties = value.GetType().GetProperties();

            if (properties.Length == 0)
                throw new Exception("Object for values have no properties.");

            var update = Builders<Entity>.Update.Set(properties[0].Name, properties[0].GetValue(value));

            for(var position = 1;  position < properties.Length; position++) {
                update = Builders<Entity>.Update.Combine(
                        update,
                        Builders<Entity>.Update.Set(properties[position].Name, properties[position].GetValue(value))
                    );
            }

            var result = Collection.UpdateMany(filter, update);

            return (result.IsModifiedCountAvailable ? result.ModifiedCount : 0);

        }

        public bool Delete(Entity entity) {

            var filter = Builders<Entity>.Filter.Eq("_id", entity._id);

            var result = Collection.DeleteOne(filter);

            return result.DeletedCount > 0;

        }

        public long DeleteMany(Expression<Func<Entity, bool>> where) {

            var filter = Builders<Entity>.Filter.Where(where);

            var result = Collection.DeleteMany(filter);

            return result.DeletedCount;

        }

    }
}
