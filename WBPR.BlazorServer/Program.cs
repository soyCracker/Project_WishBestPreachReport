using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;
using WBPR.BlazorServer.Data;
using WBPR.BlazorServer.Extension;
using WBPR.Service.Interfaces;
using WBPR.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddScoped<IPreachReportService, PreachReportService>();
builder.Services.AddScoped<IStorageService, PrDataBrowserLocalStorageService>();
//builder.Services.AddScoped<IStorageService, OnedriveService>();
builder.Services.AddScoped<IBrowserLocalStorageService, BrowserLocalStorageService>();
builder.Services.AddScoped<ISettingService, SettingService>();

//Auth
builder.Services.SetAuth(builder.Configuration);

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
app.MapControllers();//.RequireAuthorization()
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
