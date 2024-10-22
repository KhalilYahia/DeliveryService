
using Services.DependenyInjection;
using DeliveryService.Model.ApplicationDbContext_DependencyInjection;
using DeliveryService.Domain.Entities;
using DeliveryService.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using DeliveryService.WebApi.AssestanceClasses;
using Serilog;
//using DeliveryService.Dependency;
//using DeliveryService.Iservices;
//using Services.Services.DependenyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext_Khalil(connectionString);
builder.Services.AddIdentityOptions_Khalil();


// add auth and jwt
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
    };
});



// init Swagger for API test and make it fimilure with Bearer token
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer",new string[0]}
                };
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme ="oauth2",
                            Name ="Bearer",
                            In = ParameterLocation.Header,
                        },new List<string>()
                    }
                });

    #region This to show the comments
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile_WebApi = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath_WebApi = Path.Combine(AppContext.BaseDirectory, xmlFile_WebApi);
    c.IncludeXmlComments(xmlPath_WebApi);
    var xmlFile_Service = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    xmlFile_Service = xmlFile_Service.Replace("WebApi", "Services");
    var xmlPath_Service = Path.Combine(AppContext.BaseDirectory, xmlFile_Service);
    c.IncludeXmlComments(xmlPath_Service);
    #endregion

});




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)  // Logs will be saved in 'Logs' folder
    .CreateLogger();

builder.Host.UseSerilog();

//builder.Services.AddScoped<IPostsService, PostsService>();

//IServiceDependenyInjection.IServiceDependenyInjection_Register(builder.Services);

//IServiceDependenyInjection.SetDependencies();
builder.Services.SetDependencies();


//builder.Services.AddScoped<IPostsService, PostsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//else
{   
    app.UseHsts();
}

app.UseHttpsRedirection();

// Register the custom middleware
app.UseMiddleware<MiddlewareFor_Log>();

app.UseCors(
        options => options.WithOrigins("http://alarabiacom.ru").AllowAnyMethod().AllowAnyHeader()
        .AllowAnyOrigin()
       .WithExposedHeaders("Custom-Header")
       .SetPreflightMaxAge(TimeSpan.FromMinutes(10))
    );
//app.UseCors(
//        options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()
//        .AllowAnyOrigin()
//   );
////http://localhost:3000
///"http://khalilbroker-001-site1.ctempurl.com"

// In Configure method
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();



app.Run();
