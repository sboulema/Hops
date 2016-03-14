using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Hops.Models;

namespace Hops.Migrations
{
    [DbContext(typeof(HopContext))]
    [Migration("20160314114100_doubles")]
    partial class doubles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Hops.Models.Hop", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AlphaMax");

                    b.Property<double>("AlphaMin");

                    b.Property<string>("Aroma");

                    b.Property<double>("BetaMax");

                    b.Property<double>("BetaMin");

                    b.Property<int>("BrewingUsage");

                    b.Property<int>("CoHumuloneMax");

                    b.Property<int>("CoHumuloneMin");

                    b.Property<string>("Info");

                    b.Property<string>("Name");

                    b.Property<string>("Pedigree");

                    b.Property<string>("Styles");

                    b.Property<double>("TotalOilMax");

                    b.Property<double>("TotalOilMin");

                    b.Property<string>("Trade");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Hops.Models.Substitution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("HopId");

                    b.Property<long>("SubId");

                    b.HasKey("Id");
                });
        }
    }
}
