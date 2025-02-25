var builder = WebApplication.CreateBuilder(args);

// Agregar servicios necesarios
builder.Services.AddControllers();

// Configurar la conexión a la base de datos
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configurar el pipeline de middleware
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
