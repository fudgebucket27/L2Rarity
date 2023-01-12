using L2Rarity.Server.Models;
using L2Rarity.Server.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddResponseCaching();
builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClient(configuration));
builder.Services.AddSingleton<IGamestopClient, GamestopService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

public static partial class Program
{
    public static CosmosDbService InitializeCosmosClient(IConfiguration configuration)
    {
        var client = new Microsoft.Azure.Cosmos.CosmosClient(configuration.GetValue<string>("CosmosDbEndpoint"), configuration.GetValue<string>("CosmosDbAuthKey"));
        var cosmosDbService = new CosmosDbService(client, configuration.GetValue<string>("CosmosDbDatabaseId")!, configuration.GetValue<string>("CosmosDbContainerId")!);
        return cosmosDbService;
    }
}