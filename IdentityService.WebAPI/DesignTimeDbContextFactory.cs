using CommonInitializer;
using IdentityService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityService.WebAPI;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdDbContext>
{
    public IdDbContext CreateDbContext(string[] args)
    {
        // var optionsBuilder = DbContextOptionsBuilderFactory.Create<IdDbContext>();
        var optionsBuilder = new DbContextOptionsBuilder<IdDbContext>();
        string? connectionString = "Server=localhost;Database=zheng-VNext;charset=utf8;uid=root;pwd=password;port=3306;";
        optionsBuilder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
        return new IdDbContext(optionsBuilder.Options);
    }
}