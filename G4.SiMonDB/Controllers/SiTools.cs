using MongoDB.Driver;

namespace G4.SiMonDB.Controllers {
    internal static class SiTools {

        public static UpdateDefinition<Entity>? CreateUpdate<Entity>(Entity old, Entity _new, bool ignoreID = true) {

            var results = new List<UpdateDefinition<Entity>>();

            if (old == null || _new == null)
                throw new Exception("Old or new Entity is Null, is not possible to create UpdateDefinition");

            var properties = _new.GetType().GetProperties().ToList();

            if (ignoreID)
                properties = properties.Where(x => !x.Name.Equals("_id")).ToList();

            foreach (var property in properties) {

                var oldValue = property.GetValue(old);
                var newValue = property.GetValue(_new);

                var isDiferent = false;

                isDiferent = (oldValue == null && newValue != null);
                isDiferent = isDiferent || (oldValue != null && newValue == null);

                if (oldValue != null && newValue != null)
                    #pragma warning disable CS8602 
                    isDiferent = !oldValue.ToString().Equals(newValue.ToString());
                    #pragma warning restore CS8602 

                if (isDiferent)
                    results.Add(Builders<Entity>.Update.Set(property.Name, newValue));

            }

            if (results.Count == 0)
                return null;

            var result = results.FirstOrDefault();

            for (var position = 1; position < results.Count; position++) {
                result = Builders<Entity>.Update.Combine(result, results.ElementAt(position));
            }

            return result;


        }

    }
}
