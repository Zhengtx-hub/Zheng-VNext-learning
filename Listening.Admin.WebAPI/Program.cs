using System.IO;
using CommonInitializer;
using Listening.Admin.WebAPI.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
// builder.ConfigureDbConfiguration();
builder.Configuration.AddJsonFile("appsettings.listening.admin.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Services.AddDbContext<ListeningDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultDB");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
// builder.Services.AddMediatR(cfg => 
//     cfg.RegisterServicesFromAssembly(typeof(ListeningDbContext).Assembly)); 
builder.Services.AddMediatR(typeof(ListeningDbContext).Assembly); 
var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", "listening.admin.log");
builder.ConfigureExtraServices(new InitializerOptions
{
    EventBusQueueName = "IdentityService.WebAPI",
    LogFilePath = logFilePath
});

builder.Services.AddScoped<EncodingEpisodeHelper>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Listening.Admin.WebAPI", Version = "v1" });
});
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Listening.Admin.WebAPI v1"));
}
app.MapHub<EpisodeEncodingStatusHub>("/Hubs/EpisodeEncodingStatusHub");
app.UseZackDefault();
app.MapControllers();
app.Run();
