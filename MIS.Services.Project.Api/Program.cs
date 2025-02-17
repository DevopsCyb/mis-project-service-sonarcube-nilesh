using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using Prometheus;
using Azure.Messaging.ServiceBus;
using MIS.Services.Project.Api.Repository;
using MIS.Services.Project.Api.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    builder.Services.AddDbContext<ProjectdbContext>(item =>
        item.UseSqlServer(builder.Configuration.GetConnectionString("ProjectServiceConnectionString")));
}
else
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    // Load environment-specific configuration
    builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);

    // Retrieve key vault information from the environment-specific configuration
    var keyVaultUri = builder.Configuration["AzureKeyVault:keyVaultURL"];
    var dbSecret = builder.Configuration["AzureKeyVault:ProjectDbSecret"];

    // Use the retrieved key vault information
    var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
    KeyVaultSecret secret = client.GetSecret(dbSecret);
    builder.Services.AddDbContext<ProjectdbContext>(item =>
        item.UseSqlServer(client.GetSecret($"{dbSecret}").Value.Value.ToString()));
}

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
    options.AddPolicy("corsapp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ProjectdbContext>(options =>
  //              options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"), 
    //            sqlServerOptionsAction: sqlOptions => sqlOptions.EnableRetryOnFailure()));
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IProjectResourcesRepository, ProjectResourcesRepository>();
builder.Services.AddTransient<IRolesRepository, RolesRepository > ();
builder.Services.AddTransient<IVerticalRepository, VerticalRepository>();
builder.Services.AddMvc().AddJsonOptions(options =>
{
    
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Add Azure Service Bus dependency injection
//builder.Services.AddSingleton<ServiceBusClient>(new ServiceBusClient(builder.Configuration.GetConnectionString("serviceBusConnection")));
// builder.Services.AddSingleton<IEventPublisher>(sb =>
// {
//     var connectionString = builder.Configuration.GetConnectionString("serviceBusConnection");
//     var topicName = "mis-pjt-svc";
//     var serviceBusClient = sb.GetService<ServiceBusClient>();
//     return new EventPublisher(serviceBusClient, topicName);
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");
app.UseHttpsRedirection();
app.UseMetricServer();
app.UseHttpMetrics();

app.UseAuthorization();

app.MapControllers();


app.Run();
