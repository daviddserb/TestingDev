using DavidSerb.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel
{
    public class SystemDataSet
    {
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<Depot> Depots { get; set; } = new List<Depot>();
        public List<DrugUnit> DrugUnits { get; set; } = new List<DrugUnit>();
        public List<DrugType> DrugTypes { get; set; } = new List<DrugType>();
        public List<Site> Sites { get; set; } = new List<Site>();

        public SystemDataSet()
        {
            // Init objs
            Country country1 = new Country("1", "CountryA");
            Country country2 = new Country("2", "CountryB");

            Depot depot1 = new Depot("1", "DepotA");
            Depot depot2 = new Depot("2", "DepotB");

            DrugUnit drugUnit1 = new DrugUnit("ABC111", 10);
            DrugUnit drugUnit2 = new DrugUnit("ABC112", 20);
            DrugUnit drugUnit3 = new DrugUnit("ABC113", 30);
            DrugUnit drugUnit4 = new DrugUnit("ABC114", 40);
            DrugUnit drugUnit5 = new DrugUnit("ABC115", 50);

            DrugUnit drugUnit6 = new DrugUnit("ERT123", 60);
            DrugUnit drugUnit7 = new DrugUnit("ERT124", 70);
            DrugUnit drugUnit8 = new DrugUnit("ERT125", 80);
            DrugUnit drugUnit9 = new DrugUnit("ERT126", 90);
            DrugUnit drugUnit10 = new DrugUnit("ERT127", 100);

            DrugUnit drugUnit11 = new DrugUnit("IOP987", 200);
            DrugUnit drugUnit12 = new DrugUnit("IOP986", 210);
            DrugUnit drugUnit13 = new DrugUnit("IOP985", 220);
            DrugUnit drugUnit14 = new DrugUnit("IOP984", 230);
            DrugUnit drugUnit15 = new DrugUnit("IOP983", 240);

            DrugUnit drugUnit16 = new DrugUnit("DEV999", 250);
            DrugUnit drugUnit17 = new DrugUnit("DEV888", 260);
            DrugUnit drugUnit18 = new DrugUnit("DEV777", 270);
            DrugUnit drugUnit19 = new DrugUnit("DEV666", 280);
            DrugUnit drugUnit20 = new DrugUnit("DEV555", 290);

            DrugType drugType1 = new DrugType("1", "DrugType1");
            DrugType drugType2 = new DrugType("2", "DrugType2");

            Site site1 = new Site("SiteId1", "SiteA", "1");
            Site site2 = new Site("SiteId2", "SiteB", "1");
            Site site3 = new Site("SiteId3", "SiteC", "2");

            // Set relationships
            country1.Depot = depot1;
            country1.Sites.Add(site1);
            country1.Sites.Add(site2);

            country2.Depot = depot2;
            country2.Sites.Add(site3);

            depot1.Countries.Add(country1);
            depot1.Countries.Add(country2);

            depot2.Countries.Add(country1);

            drugUnit1.Depot = depot1;
            drugUnit1.DrugType = drugType1;

            drugUnit2.Depot = depot2;
            drugUnit2.DrugType = drugType2;

            drugUnit3.Depot = depot1;
            drugUnit3.DrugType = drugType2;

            drugUnit4.Depot = depot1;

            drugUnit5.Depot = depot2;
            drugUnit5.DrugType = drugType1;

            drugUnit6.Depot = depot2;
            drugUnit6.DrugType = drugType1;

            drugUnit7.DrugType = drugType1;

            drugUnit8.Depot = depot1;
            drugUnit8.DrugType = drugType2;

            drugUnit9.Depot = depot2;
            drugUnit9.DrugType = drugType2;

            drugUnit10.Depot = depot1;
            drugUnit10.DrugType = drugType2;

            // Add objs to lists
            Countries.Add(country1);
            Countries.Add(country2);

            Depots.Add(depot1);
            Depots.Add(depot2);

            DrugUnits.Add(drugUnit1);
            DrugUnits.Add(drugUnit2);
            DrugUnits.Add(drugUnit3);
            DrugUnits.Add(drugUnit4);
            DrugUnits.Add(drugUnit5);
            DrugUnits.Add(drugUnit6);
            DrugUnits.Add(drugUnit7);
            DrugUnits.Add(drugUnit8);
            DrugUnits.Add(drugUnit9);
            DrugUnits.Add(drugUnit10);
            DrugUnits.Add(drugUnit11);
            DrugUnits.Add(drugUnit12);
            DrugUnits.Add(drugUnit13);
            DrugUnits.Add(drugUnit14);
            DrugUnits.Add(drugUnit15);
            DrugUnits.Add(drugUnit16);
            DrugUnits.Add(drugUnit17);
            DrugUnits.Add(drugUnit18);
            DrugUnits.Add(drugUnit19);
            DrugUnits.Add(drugUnit20);

            DrugTypes.Add(drugType1);
            DrugTypes.Add(drugType2);

            Sites.Add(site1);
            Sites.Add(site2);
            Sites.Add(site3);
        }
    }
}
