using Blazored.LocalStorage;
using MudBlazor.Services;
using WBPR.BlazorServer.Data;
using WBPR.Service.Interfaces;
using WBPR.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddControllers();

builder.Services.AddScoped<IPreachReportService, PreachReportService>();
builder.Services.AddScoped<IStorageService, PrDataBrowserLocalStorageService>();
builder.Services.AddScoped<IBrowserLocalStorageService, BrowserLocalStorageService>();
builder.Services.AddScoped<ISettingService, SettingService>();

//»y¨t
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

var app = builder.Build();

//»y¨t
var supportedCultures = new[] { "zh-Hant", "en-US" };
app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
