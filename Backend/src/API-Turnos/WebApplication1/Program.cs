using Application;
using Application.Interfaces;
using Application.Services;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using static Infrastructure.Services.AuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("API-TurnosBearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Ac� pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "API-TurnosBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definici�n
                }, new List<string>() }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //setupAction.IncludeXmlComments(xmlPath); no se pudo configurar el archivo xml

});

string connectionString = builder.Configuration["ConnectionStrings:DBConnectionString"]!;

// Configure the SQLite connection
var connection = new SqliteConnection(connectionString);
connection.Open();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(connection, b => b.MigrationsAssembly("Infrastructure")));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticationService:Issuer"],
            ValidAudience = builder.Configuration["AuthenticationService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationService:SecretForKey"]))
        };
    }
);



#region Services
builder.Services.Configure<AuthenticationServiceOptions>(
    builder.Configuration.GetSection(AuthenticationServiceOptions.AuthenticationService));
builder.Services.AddScoped<ICustomAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IMedicService, MedicService>();
builder.Services.AddScoped<IMedicalCenterService, MedicalCenterService>();
builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IAdminMCService, AdminMCService>();
builder.Services.AddScoped<IWorkScheduleService, WorkScheduleService>();
builder.Services.AddScoped<ISysAdminService, SysAdminService>();
#endregion

#region Repositories
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IMedicRepository, MedicRepository>();
builder.Services.AddScoped<IMedicalCenterRepository, MedicalCenterRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IAdminMCRepository, AdminMCRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
builder.Services.AddScoped<ISysAdminRepository, SysAdminRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
