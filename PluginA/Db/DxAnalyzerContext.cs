using pluginA.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace pluginA.Db
{
    public class DxAnalyzerContext : DbContext
    {
        public DbSet<DxMessage> Messages { get; set; }

        public DxAnalyzerContext(DbContextOptions<DxAnalyzerContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DxMessage>(entity =>
            {
                entity.ToTable("3DX_Messages");
                entity.HasKey(m => m.ID);
            });

        }


    }
}
