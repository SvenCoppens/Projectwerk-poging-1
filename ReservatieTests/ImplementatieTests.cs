using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;
using DomainLibrary;
using ReservatieTests;

namespace DataLayer.Tests
{
    [TestClass()]
    public class ImplementatieTests
    {
        [TestMethod]
        public void CorrectKlantToevoegen_Test()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            Assert.IsTrue(rm.GetAantalKlanten() == 1);

            Klant testKlant = rm.VindVolledigeKlantVoorKlantNummer(1);

            Assert.IsTrue(testKlant.Adres == adres, "Adres klopte niet");
            Assert.IsTrue(testKlant.BtwNummer == btwNummer, "Btw nummer klopte niet");
            Assert.IsTrue(testKlant.Categorie.Naam == klantenCategorie, "categorie naam klopte niet");
            Assert.IsTrue(testKlant.Categorie.StaffelKorting.Naam == "geen", "Staffelkorting naam klopte niet");
            Assert.IsTrue(testKlant.Naam == klantNaam, "klant naam klopte niet");
        }
    }
}