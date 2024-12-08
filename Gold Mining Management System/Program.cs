using Gold_Mining_Management_System.Data;
using Gold_Mining_Management_System.Middlewares;
using Gold_Mining_Management_System.Repositories;
using Gold_Mining_Management_System.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
var jwtval = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtval["Key"]);

builder.Services.AddSingleton<JwtService>();

builder.Services.AddDbContext<AppDbContext>(i => i.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(i =>
{
    i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(i =>
{
    i.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtval["Issuer"],
        ValidAudience = jwtval["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization(i =>
{
    i.AddPolicy("AdminOnly", j => j.RequireRole("Admin"));
    i.AddPolicy("All", j => j.RequireRole("Admin", "Mine Manager", "Geologist", "Engineer", "Safety Officer", "Field Worker"));
});

builder.Services.AddScoped<JwtService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<ISafetyIncidentRepository, SafetyIncidentRepository>();
builder.Services.AddScoped<ISafetyIncidentService, SafetyIncidentService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IProductionRepository, ProductionRepository>();
builder.Services.AddScoped<IProductionService, ProductionService>();
builder.Services.AddScoped<IMinePlansRepository, MinePlansRepository>();
builder.Services.AddScoped<IMinePlansService, MinePlansService>();
builder.Services.AddScoped<IGeologicalDataRepository, GeologicalDataRepository>();
builder.Services.AddScoped<IGeologicalDataService,  GeologicalDataService>();
builder.Services.AddScoped<IEquipmentsRepository, EquipmentsRepository>();
builder.Services.AddScoped<IEquipmentsService, EquipmentsService>();
builder.Services.AddScoped<IEnvironmentalDataRepository, EnvironmentalDataRepository>();
builder.Services.AddScoped<IEnvironmentalDataService, EnvironmentalDataService>();
builder.Services.AddScoped<ICostManagementRepository, CostManagementRepository>();
builder.Services.AddScoped<ICostManagementService, CostManagementService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<JwtAuthenticationMiddleware>(); 

app.UseAuthorization(); 

app.MapControllers();

app.Run();
