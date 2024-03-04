using DbUp;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using VM.API.Extensions;
using VM.API.Filters;
using VM.Common.Configurations;
using VM.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

string? appEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile($"appsettings.{appEnvironment}.json", true, true);
builder.Configuration.AddEnvironmentVariables();


string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));



// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.RegisterDependencies(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"]))
    };

    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/hub")))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VM.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()

                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VM.Api v1");
        c.EnablePersistAuthorization();
    });
}
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dataContext.Database.Migrate();

    string path = Path.Combine(Directory.GetCurrentDirectory(), "DbUpScripts");

    var upgrader = DeployChanges.To
                                                 .SqlDatabase(connectionString)
                                                 .LogToConsole()
                                                 .WithScriptsFromFileSystem(path)
                                                 .Build();
    var result = upgrader.PerformUpgrade();
    if (!result.Successful)
        throw new System.Exception("DB not updated");
}

app.UseHttpsRedirection();

app.UseRouting();


var webAppConfiguration = builder.Configuration.GetSection("WebApp").Get<WebAppConfiguration>();

if (webAppConfiguration != null)
{
    app.UseCors(config =>
    {
        string webUrl = webAppConfiguration.Url;
        List<string> otherUrlList = webAppConfiguration.OtherUrls;
        otherUrlList.Add(webUrl);
        config.WithOrigins([.. otherUrlList])
        .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

