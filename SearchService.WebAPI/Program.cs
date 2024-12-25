using System.IO;
using CommonInitializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nest;
using SearchService.Domain;
using SearchService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.search.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", "search.service.log");
builder.ConfigureExtraServices(new InitializerOptions
{
    EventBusQueueName = "SearchService.WebAPI",
    LogFilePath = logFilePath
});
// Add services to the container.

// var elasticUrl = builder.Configuration.GetSection("ElasticSearch:Url").Value;
// Console.WriteLine("ElasticSearch Url: " + elasticUrl);


builder.Services.Configure<ElasticSearchOptions>(builder.Configuration.GetSection("ElasticSearch"));
// 配置 IElasticClient
builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<ElasticSearchOptions>>().Value;
    var settings = new ConnectionSettings(new Uri(options.Url))
        .DefaultIndex(options.EpisodeIndex); // 使用配置文件中的 EpisodeIndex
    return new ElasticClient(settings);
});
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SearchService.WebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SearchService.WebAPI v1"));
}
app.UseZackDefault();
app.MapControllers();

app.Run();
