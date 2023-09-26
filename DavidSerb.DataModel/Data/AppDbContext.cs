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
        DbSet<Country> Countries { get; set; }
        DbSet<Depot> Depots => Set<Depot>();
        DbSet<DrugUnit> DrugUnits { get; set; }
        DbSet<DrugType> DrugTypes { get; set; }
        DbSet<Site> Sites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);
            optionBuilder.UseSqlServer("connection string");
        }
    }
}
