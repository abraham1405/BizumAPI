var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de JSON
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Si quieres usar Swagger

var app = builder.Build();

// Middleware
app.UseRouting();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Swagger (opcional, útil para pruebas)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
