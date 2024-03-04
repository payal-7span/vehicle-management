using Blazored.LocalStorage;
using VM.Service.Services;
using VM.Web.Components;
using VM.Web.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var uri = new Uri("https://localhost:44351/api/");


builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<CustomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient(new CustomHttpHandler(sp.GetRequiredService<ILocalStorageService>()))
{
    BaseAddress = uri
});

builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();
builder.Services.AddScoped<IFeesHeadService, FeesHeadService>();
builder.Services.AddScoped<IFeesStructureService, FeesStructureService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
