using CertificatesAPI.Data;
using CertificatesAPI.Services.Interface;
using CertificatesAPI.Services.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text.Json.Serialization;
using CertificatesAPI.DTOs.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");



builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("PermissionApiRequest", c => c.WithOrigins("your origin here").WithMethods("GET"));
});


var mappingConfig = new MapperConfiguration(mc =>
{

    mc.AddProfile(new MappingProfile());
    
});



builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
