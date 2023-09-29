using DavidSerb.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<DrugUnit> DrugUnits { get; set; }
        public DbSet<DrugType> DrugTypes { get; set; }
        public DbSet<Site> Sites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(@"Server=DESKTOP-EP63S60;Database=InternshipDev;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Fluent API - Make Country.CountryName Unique
            builder.Entity<Country>()
                .HasIndex(country => country.CountryName)
                .IsUnique();

            // Because the FK (CountryCode) from Site (to Country) doesn't follow the name conventions (should be CountryId), EFC generates a shadow property FK CountryId.
            // => use Fluent API - set relationship: Site has one Country, Country can be with Many Sites, and the FK is CountryCode.
            builder.Entity<Site>()
                .HasOne(site => site.Country)
                .WithMany(country => country.Sites)
                .HasForeignKey(site => site.CountryCode);
        }
    }
}
