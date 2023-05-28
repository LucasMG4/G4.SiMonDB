using G4.SiMonDB.Controllers;
using G4.SiMonDBExample.Models;

namespace G4.SiMonDBExample.Services {
    public static class DependencyResolver {

        private static string ConnectionString = "";

        public static void ConfigureDepedency(this WebApplicationBuilder builder) {

            var cnnString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (cnnString == null)
                throw new Exception("ConnectionString 'DefaultConnection' not informed on appSettings.");

            ConnectionString = cnnString;

            builder.Services.AddSingleton<SiContext>();

            builder.Services.AddScoped<VehicleService>();

        }

        public static void ConfigureDatabase(this WebApplication app) {

            var context = app.Services.GetRequiredService<SiContext>();

            context.SetConnectionString(ConnectionString);

            context.EntitiesRegister.Add<Vehicle>("g4", "vehicles");

        }

    }
}
