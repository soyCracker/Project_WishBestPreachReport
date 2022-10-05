using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WBPR.Service.Interfaces;
using WBPR.Service.Services;
using WBPR.Wasm;
using WBPR.Wasm.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//IHttpClientFactory
builder.Services.AddHttpClient();
var httpClient = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};
builder.Services.AddScoped(sp => httpClient);
//using var wbprSetting = await httpClient.GetAsync("WBPR.json");
//using var stream = await wbprSetting.Content.ReadAsStreamAsync();
//builder.Configuration.AddJsonStream(stream);

builder.Services.AddMudServices();
builder.Services.AddScoped<IPreachReportService, PreachReportService>();
builder.Services.AddScoped<IStorageService, OnedriveService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddGraphClient();
builder.Services.SetMsAuth();

//»y¨t
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

var host = builder.Build();

//»y¨t
await ConfigExtension.SetCulture(host);

await host.RunAsync();
