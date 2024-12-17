using CommonInitializer;
using FileService.Infrastructure;
using FileService.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
// builder.ConfigureDbConfiguration();
builder.Configuration.AddJsonFile("appsettings.FileService.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Services.AddDbContext<FSDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultDB");
    
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", "IdentityService.log");
builder.ConfigureExtraServices(new InitializerOptions
{
    EventBusQueueName = "IdentityService.WebAPI",
    LogFilePath = logFilePath
});

// Add services to the container.
builder.Services//.AddOptions() //asp.net core��Ŀ��AddOptions()��дҲ�У���Ϊ���һ���Զ�ִ����
    .Configure<SMBStorageOptions>(builder.Configuration.GetSection("FileService:SMB"))
    .Configure<UpYunStorageOptions>(builder.Configuration.GetSection("FileService:UpYun"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FileService.WebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileService.WebAPI v1"));
}
app.UseStaticFiles();
app.UseZackDefault();

app.MapControllers();

app.Run();
