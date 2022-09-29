using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WBPR.Base.Config;
using WBPR.Service.Interfaces;
using WBPR.Service.Services;
using WBPR.Wasm;
using WBPR.Wasm.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped<IPreachReportService, PreachReportService>();
builder.Services.AddScoped<IStorageService, BrowserLocalStorageService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddGraphClient();
//builder.Services.SetMsAuth();
builder.Services.AddMsalAuthentication(options =>
{
    options.ProviderOptions.Authentication.ClientId = Constant.MS_AUTH_CLIENT_ID;
    options.ProviderOptions.Authentication.Authority = "https://login.microsoftonline.com/da114b60-0d10-45a2-8f43-bfd5bfda9758";
    options.ProviderOptions.Authentication.ValidateAuthority = true;
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
    options.ProviderOptions.LoginMode = "redirect";
});

//»y¨t
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

var host = builder.Build();

//»y¨t
await ConfigExtension.SetCulture(host);

await host.RunAsync();
