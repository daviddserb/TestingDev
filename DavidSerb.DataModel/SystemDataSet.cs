﻿using DavidSerb.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel
{
    public class SystemDataSet
    {
        public List<Country> Countries { get; } = new List<Country>();
        public List<Depot> Depots { get; } = new List<Depot>();
        public List<DrugUnit> DrugUnits { get; } = new List<DrugUnit>();
        public List<DrugType> DrugTypes { get; } = new List<DrugType>();
        public List<Site> Sites { get; } = new List<Site>();

        public SystemDataSet()
        {
            // Set Values (declaration + initialization)
            Country c1 = new Country(1, "CountryA");
            Country c2 = new Country(2, "CountryB");

            Depot d1 = new Depot("1", "DepotA");
            Depot d2 = new Depot("2", "DepotB");

            //NOTE: Do not associate any of the drug units to a depot
            DrugUnit du1 = new DrugUnit("ABC111", 10);
            DrugUnit du2 = new DrugUnit("ABC112", 20);
            DrugUnit du3 = new DrugUnit("ABC113", 30);
            DrugUnit du4 = new DrugUnit("ABC114", 40);
            DrugUnit du5 = new DrugUnit("ABC115", 50);

            DrugUnit du6 = new DrugUnit("ERT123", 60);
            DrugUnit du7 = new DrugUnit("ERT124", 70);
            DrugUnit du8 = new DrugUnit("ERT125", 80);
            DrugUnit du9 = new DrugUnit("ERT126", 90);
            DrugUnit du10 = new DrugUnit("ERT127", 100);

            DrugUnit du11 = new DrugUnit("IOP987", 200);
            DrugUnit du12 = new DrugUnit("IOP986", 210);
            DrugUnit du13 = new DrugUnit("IOP985", 220);
            DrugUnit du14 = new DrugUnit("IOP984", 230);
            DrugUnit du15 = new DrugUnit("IOP983", 240);

            DrugUnit du16 = new DrugUnit("DEV999", 250);
            DrugUnit du17 = new DrugUnit("DEV888", 260);
            DrugUnit du18 = new DrugUnit("DEV777", 270);
            DrugUnit du19 = new DrugUnit("DEV666", 280);
            DrugUnit du20 = new DrugUnit("DEV555", 290);

            DrugType dt1 = new DrugType(1, "DrugType1");
            DrugType dt2 = new DrugType(2, "DrugType2");

            Site s1 = new Site("SiteId1", "SiteA", 1);
            Site s2 = new Site("SiteId2", "SiteB", 1);
            Site s3 = new Site("SiteId3", "SiteC", 2);

            // Set Relationships
            c1.Depot = d1;
            c1.Sites.Add(s1);
            c1.Sites.Add(s2);

            c2.Depot = d2;
            c2.Sites.Add(s3);

            d1.Countries.Add(c1);
            d1.Countries.Add(c2);

            d2.Countries.Add(c1);

            du1.Depot = d1;
            du1.DrugType = dt1;

            du2.Depot = d2;
            du2.DrugType = dt2;

            du3.Depot = d1;
            du3.DrugType = dt2;

            du4.Depot = d1;
            du4.DrugType = dt2;

            du5.Depot = d2;
            du5.DrugType = dt1;

            du6.Depot = d2;
            du6.DrugType = dt1;

            du7.Depot = d2;
            du7.DrugType = dt1;

            du8.Depot = d1;
            du8.DrugType = dt2;

            du9.Depot = d2;
            du9.DrugType = dt2;

            du10.Depot = d1;
            du10.DrugType = dt2;

            //Add the objects to the properties
            Countries.Add(c1);
            Countries.Add(c2);

            Depots.Add(d1);
            Depots.Add(d2);

            DrugUnits.Add(du1);
            DrugUnits.Add(du2);
            DrugUnits.Add(du3);
            DrugUnits.Add(du4);
            DrugUnits.Add(du5);
            DrugUnits.Add(du6);
            DrugUnits.Add(du7);
            DrugUnits.Add(du8);
            DrugUnits.Add(du9);
            DrugUnits.Add(du10);
            DrugUnits.Add(du11);
            DrugUnits.Add(du12);
            DrugUnits.Add(du13);
            DrugUnits.Add(du14);
            DrugUnits.Add(du15);
            DrugUnits.Add(du16);
            DrugUnits.Add(du17);
            DrugUnits.Add(du18);
            DrugUnits.Add(du19);
            DrugUnits.Add(du20);

            DrugTypes.Add(dt1);
            DrugTypes.Add(dt2);

            Sites.Add(s1);
            Sites.Add(s2);
            Sites.Add(s3);
        }
    }
}
