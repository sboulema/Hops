using Microsoft.Data.Entity;

namespace Hops.Models
{
    public class HopContext : DbContext
    {
        public DbSet<Hop> Hops { get; set; }
        public DbSet<Substitution> Substitutions { get; set; }
        public DbSet<Alias> Alias { get; set; }
        public DbSet<Aroma> Aroma { get; set; }
        public DbSet<Malt> Malt { get; set; }
        public DbSet<Yeast> Yeast { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hop>().HasKey(m => m.Id);
            builder.Entity<Substitution>().HasKey(m => new { m.HopId, m.SubId });
            builder.Entity<Alias>().HasKey(m => new { m.HopId, m.Name });
            builder.Entity<Aroma>().HasKey(m => new { m.HopId, m.Profile });
            builder.Entity<Malt>().HasKey(m => new { m.Id });
            builder.Entity<Yeast>().HasKey(m => new { m.Id });
            base.OnModelCreating(builder);
        }
    }
}
