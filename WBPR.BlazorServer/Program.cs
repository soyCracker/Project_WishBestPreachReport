using Blazored.LocalStorage;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;
using WBPR.BlazorServer.Data;
using WBPR.BlazorServer.Extension;
using WBPR.Service.Interfaces;
using WBPR.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddMicrosoftIdentityUI();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddControllers();
//builder.Services.AddHttpContextAccessor();
//IHttpClientFactory
//builder.Services.AddHttpClient();

builder.Services.AddScoped<IPreachReportService, PreachReportService>();
builder.Services.AddScoped<IStorageService, PrDataBrowserLocalStorageService>();
//builder.Services.AddScoped<IStorageService, OnedriveService>();
builder.Services.AddScoped<IBrowserLocalStorageService, BrowserLocalStorageService>();
builder.Services.AddScoped<ISettingService, SettingService>();
//builder.Services.AddScoped<TokenProvider>();
//builder.Services.AddScoped<GraphClientUtilService>();

//Auth
builder.Services.SetAuthTest(builder.Configuration);

//語系
builder.Services.AddCultureResource();

var app = builder.Build();

//語系
app.SetCulture();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
