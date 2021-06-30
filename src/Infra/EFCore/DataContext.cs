using dotnetServer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetServer.Infra.EFCore
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql()
                .UseSnakeCaseNamingConvention();
    }
}