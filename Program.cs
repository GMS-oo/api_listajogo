using jogos.Data;
using Microsoft.EntityFrameworkCore;
using jogos.Services; // <--- 1. NÃO ESQUEÇA DESTE USING AQUI EM CIMA

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Controllers
builder.Services.AddControllers();

// ---> 2. ADICIONE ESTA LINHA AQUI <---
builder.Services.AddScoped<UploadService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal",
        p => p.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://127.0.0.1:5500", "http://localhost:5500"));
});

// ... resto do código igual ...

// CORS (permite front local)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal",
        p => p.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://127.0.0.1:5500", "http://localhost:5500"));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowLocal");

// Serve arquivos estáticos (para imagens em wwwroot/images)
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();