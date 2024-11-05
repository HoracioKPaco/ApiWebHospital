using ApiWebHospital.Data;
using ApiWebHospital.Data.Repository;
using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Data.UnitOfWork;
using ApiWebHospital.Service;
using ApiWebHospital.SeviceImplement;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options=>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
 policy =>
 {
     policy.WithOrigins("*") // Replace with specific origins if needed
           .WithMethods("*")
           .WithHeaders("*");
 });
});
//Repositories and Unit Of Work
builder.Services.AddScoped<IEgresoRepository,EgresoRepository>();
builder.Services.AddScoped<IIngresoRepository,IngresoRepository>();
builder.Services.AddScoped<IMedicoRepository,MedicoRepository>();
builder.Services.AddScoped<IPacienteRepository,PacienteRepository>();
builder.Services.AddScoped<IIngresoRepository, IngresoRepository>();
builder.Services.AddScoped<IEgresoService, EgresoService>();
builder.Services.AddScoped<IUnitOfWork,UnitToWork>();

//Servicios
builder.Services.AddScoped<IMedicoService,MedicoService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IIngresoService, IngresoService>();


builder.Services.AddDbContext<EfContext>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    EfContext context = scope.ServiceProvider.GetRequiredService<EfContext>();
    context.Database.EnsureCreated();
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
