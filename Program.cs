using System.Text.Json.Serialization;
using backCommerce.Data;
using backCommerce.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AdministracionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdministracionDB")));

builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<ProyectoService>();
builder.Services.AddScoped<EmpProyService>();

const string CorsPolicy = "FrontendCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(CorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
