using G4.SiMonDBExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureDepedency();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureDatabase();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
