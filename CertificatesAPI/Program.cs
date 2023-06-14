using CertificatesAPI.Data;
using CertificatesAPI.Services.Interface;
using CertificatesAPI.Services.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text.Json.Serialization;
using CertificatesAPI.DTOs.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityCore<IdentityUser>(opt =>
{
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+áéíóúÁÉÍÓÚãõÃÕ";
    opt.User.RequireUniqueEmail = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
{

    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
    ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["wt:key"]))

});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("PermissionApiRequest", c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build());
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

app.UseCors("PermissionApiRequest");

app.UseAuthentication();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{

    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Static/Images")),
    RequestPath = new PathString("/Static/Images")

});

app.UseAuthorization();

app.MapControllers();

app.Run();
