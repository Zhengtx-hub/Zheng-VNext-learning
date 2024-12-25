using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CommonInitializer
{
    public static class DbContextOptionsBuilderFactory
    {
        public static DbContextOptionsBuilder<TDbContext> Create<TDbContext>()
            where TDbContext : DbContext
        {
            // load from local this cannot be use for real enviroment
            // var connStr = Environment.GetEnvironmentVariable("DefaultDB:ConnStr");
            
            //load  appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // load dir
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // read appsettings.json
                .Build();
            var connStr = configuration.GetConnectionString("DefaultDB");
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            // optionsBuilder(connStr, ServerVersion.AutoDetect(connStr));
            optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));

            // //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=YouzackVNextDB;User ID=sa;Password=dLLikhQWy5TBz1uM;");
            // optionsBuilder.UseSqlServer(connStr);
            return optionsBuilder;
        }
    }
}
