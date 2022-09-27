using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using System.Globalization;
using WBPR.Service.Interfaces;
using WBPR.Service.Services;
using WBPR.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped<IPreachReportService, PreachReportService>();
//builder.Services.AddScoped<IStorageService, TestLocalStorageService>();
builder.Services.AddScoped<IStorageService, BrowserLocalStorageService>();


builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//»y¨t
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

var host = builder.Build();

CultureInfo culture;
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");

culture = new CultureInfo("zh-Hant");
await js.InvokeVoidAsync("blazorCulture.set", "zh-Hant");
/*if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    culture = new CultureInfo("zh-Hant");
    await js.InvokeVoidAsync("blazorCulture.set", "zh-Hant");
}*/

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
