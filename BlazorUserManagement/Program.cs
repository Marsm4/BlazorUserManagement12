using BlazorMovieApp.Services;
using BlazorUserManagement;
using BlazorUserManagement.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7253/api/movies") }); // исправленный URL

builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<ChatService>();
await builder.Build().RunAsync();
