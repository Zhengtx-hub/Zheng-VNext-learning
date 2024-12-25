using CommonInitializer;
using MediaEncoder.Infrastructure;
using MediaEncoder.WebAPI.BgServices;
using MediaEncoder.WebAPI.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zack.JWT;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.media.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Services.AddDbContext<MEDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultDB");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", "mediaEncoder.log");
builder.ConfigureExtraServices(new InitializerOptions
{
    EventBusQueueName = "IdentityService.WebAPI",
    LogFilePath = logFilePath
});

builder.Services.Configure<FileServiceOptions>(builder.Configuration.GetSection("FileService:Endpoint"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.AddHttpClient();
builder.Services.AddHostedService<EncodingBgService>();//后台转码服务
builder.Services.AddMediatR(typeof(MEDbContext).Assembly); 
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MediaEncoder.WebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MediaEncoder.WebAPI v1"));
}
app.UseZackDefault();
app.MapControllers();
app.Run();
