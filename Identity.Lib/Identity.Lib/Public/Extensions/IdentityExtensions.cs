using Microsoft.Extensions.DependencyInjection;

namespace Identity.Lib.Public.Extensions
{
    public static class IdentityExtensions
    {
        /// <summary>
        /// Will wireup the conection string for the data store using SQL Server
        /// </summary>
        /// <param name="services"></param>
        /// <param name="conn"></param>
        public static void AddIdentityStoreConfigSqlServer(this IServiceCollection services, string conn)
        {
            //var ctxOptsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            //ctxOptsBuilder.UseSqlServer(conn);
            //services.AddSingleton<DbContextOptions<UserDbContext>>(ctxOptsBuilder.Options);
        }

        /// <summary>
        /// Will wire up the Identity components for dependency injection
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomIdentityProviders(this IServiceCollection services)
        {

        }
    }
}
