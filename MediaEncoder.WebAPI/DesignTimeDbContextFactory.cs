
using CommonInitializer;
using MediaEncoder.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace MediaEncoder.WebAPI;

//用IDesignTimeDbContextFactory坑最少，最省事
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MEDbContext>
{
    public MEDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MEDbContext>();
        string? connectionString = "Server=localhost;Database=zheng-VNext;charset=utf8;uid=root;pwd=password;port=3306;";
        optionsBuilder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
        return new MEDbContext(optionsBuilder.Options, null);
    }
}
