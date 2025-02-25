var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de JSON
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Si quieres usar Swagger

// Configurar CORS (opcional)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Middleware
app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAll"); // Activar CORS

// Mapear controladores
app.MapControllers();

// Swagger (opcional, útil para pruebas)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
