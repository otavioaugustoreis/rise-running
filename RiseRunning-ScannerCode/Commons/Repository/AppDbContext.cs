using Microsoft.EntityFrameworkCore;
using RiseRunning_ScannerCode.Model.Entity;

namespace RiseRunning_ScannerCode.Commons.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RunnerEntity> Runners { get; set; }

        public RunnerEntity AddRunner(object entity)
        {
            base.Add(entity);
            base.SaveChanges();
            return (RunnerEntity)entity;
        }
    }
}
