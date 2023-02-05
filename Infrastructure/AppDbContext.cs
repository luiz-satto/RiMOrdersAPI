using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public sealed class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}