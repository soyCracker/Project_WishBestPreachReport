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

//語系
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

var app = builder.Build();

//語系 ***取得的瀏覽器語系不是預期的zh-hant而是zh-tw，所以決定只設定語系'-'前字元
var supportedCultures = new[] { "en", "zh" };
app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
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
