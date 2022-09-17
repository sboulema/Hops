using Hops.Models.Hops;
using Hops.Models.Malts;
using Hops.Models.Yeasts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Hops.Models;

public class BrewDBContext : DbContext
{
    public DbSet<Hop>? Hop { get; set; }

    public DbSet<Substitution>? Substitution { get; set; }

    public DbSet<Alias>? Alias { get; set; }

    public DbSet<Aroma>? Aroma { get; set; }

    public DbSet<Malt>? Malt { get; set; }

    public DbSet<Yeast>? Yeast { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Hop>().Navigation(hop => hop.Substitutions).AutoInclude();
        builder.Entity<Hop>().Navigation(hop => hop.Aromas).AutoInclude();
        builder.Entity<Hop>().Navigation(hop => hop.Aliases).AutoInclude();

        builder.Entity<Substitution>().HasKey(m => new { m.HopId, m.SubId });

        builder.Entity<Alias>().HasKey(m => new { m.HopId, m.Name });
        builder.Entity<Aroma>().HasKey(m => new { m.HopId, m.Profile });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Filename=BrewDB/brewDB.sqlite");
}
