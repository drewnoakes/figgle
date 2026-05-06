// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Figgle.Site;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Show detailed errors in the browser console
builder.Logging.SetMinimumLevel(LogLevel.Debug);

await builder.Build().RunAsync();
