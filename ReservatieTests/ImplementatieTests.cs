using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;
using DomainLibrary;
using ReservatieTests;
using Microsoft.EntityFrameworkCore.Query.Internal;
using DomainLibrary.Enums;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Linq;


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
        [TestMethod]
        public void CorrectLimousineToevoegen()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string naam = "testLimo1";
            int uurPrijs = 150;
            int nightlifePrijs1 = 2500;
            int weddingPrijs2 = 3000;
            int wellnessPrijs3 = 4213;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs2, wellnessPrijs3);

            Assert.IsTrue(rm.GetAantalLimousines() == 1);

            Limousine testLimo = rm.FindLimousineVoorId(1);

            Assert.IsTrue(testLimo.Naam == naam,"Naam van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.EersteUurPrijs == uurPrijs, "Eerste uur prijs van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.NightlifePrijs == nightlifePrijs1, "Nightlife prijs van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.WeddingPrijs == weddingPrijs2, "Wedding prijs van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.WellnessPrijs == wellnessPrijs3, "Wellness prijs van de Limo is niet correct toegevoegd");
        }
        [TestMethod]
        public void LimoToevoegenAccepteertNullWaarden()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string naam = "testLimo1";
            int uurPrijs = 150;
            int? nightlifePrijs1 = null;
            int? weddingPrijs2 = null;
            int? wellnessPrijs3 = null;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs2, wellnessPrijs3);

            Limousine testLimo = rm.FindLimousineVoorId(1);

            Assert.IsTrue(testLimo.Naam == naam, "Naam van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.EersteUurPrijs == uurPrijs, "Eerste uur prijs van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.NightlifePrijs == null, "Nightlife prijs van de Limo is niet null");
            Assert.IsTrue(testLimo.WeddingPrijs == null, "Wedding prijs van de Limo is niet null");
            Assert.IsTrue(testLimo.WellnessPrijs == null, "Wellness prijs van de Limo is niet null");
        }
        [TestMethod]
        public void LimoToevoegenAccepteertGemengdeNullWaarden()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string naam = "testLimo1";
            int uurPrijs = 150;
            int? nightlifePrijs1 = 1234;
            int? weddingPrijs2 = null;
            int? wellnessPrijs3 = null;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs2, wellnessPrijs3);

            Limousine testLimo = rm.FindLimousineVoorId(1);

            Assert.IsTrue(testLimo.Naam == naam, "Naam van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.EersteUurPrijs == uurPrijs, $"Eerste uur prijs van {testLimo.Naam} is niet correct toegevoegd");
            Assert.IsTrue(testLimo.NightlifePrijs == nightlifePrijs1, $"Nightlife prijs van {testLimo.Naam} is niet null");
            Assert.IsTrue(testLimo.WeddingPrijs == null, $"Wedding prijs van {testLimo.Naam} is niet null");
            Assert.IsTrue(testLimo.WellnessPrijs == null, $"Wellness prijs van {testLimo.Naam} is niet null");

            nightlifePrijs1 = null;
            weddingPrijs2 = 9876;
            wellnessPrijs3 = null;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs2, wellnessPrijs3);

            testLimo = rm.FindLimousineVoorId(2);

            Assert.IsTrue(testLimo.Naam == naam, "Naam van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.EersteUurPrijs == uurPrijs, $"Eerste uur prijs van {testLimo.Naam} is niet correct toegevoegd");
            Assert.IsTrue(testLimo.NightlifePrijs == null, $"Nightlife prijs van {testLimo.Naam} is niet null");
            Assert.IsTrue(testLimo.WeddingPrijs == weddingPrijs2, $"Wedding prijs van {testLimo.Naam} is niet null");
            Assert.IsTrue(testLimo.WellnessPrijs == null, $"Wellness prijs van {testLimo.Naam} is niet null");

            nightlifePrijs1 = null;
            weddingPrijs2 = null;
            wellnessPrijs3 = 5605;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs2, wellnessPrijs3);

            testLimo = rm.FindLimousineVoorId(3);

            Assert.IsTrue(testLimo.Naam == naam, "Naam van de Limo is niet correct toegevoegd");
            Assert.IsTrue(testLimo.EersteUurPrijs == uurPrijs, $"Eerste uur prijs van {testLimo.Naam} is niet correct toegevoegd");
            Assert.IsTrue(testLimo.NightlifePrijs == null, $"Nightlife prijs van {testLimo.Naam} is niet null");
            Assert.IsTrue(testLimo.WeddingPrijs == null, $"Wedding prijs van {testLimo.Naam} is niet null");
            Assert.IsTrue(testLimo.WellnessPrijs == wellnessPrijs3, $"Wellness prijs van {testLimo.Naam} is niet null");
        }
        [TestMethod]
        public void KlantenCategorieToevoegenZonderBestaandeStaffelKorting_Test() 
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string naam = "TestCategorie1";
            rm.VoegKlantenCategorieToe(naam);

            KlantenCategorie test = rm.VindKlantenCategorieVoorNaam(naam);

            Assert.IsTrue(test != null);
            Assert.IsTrue(test.Naam == naam);
            Assert.IsTrue(test.StaffelKorting.Naam == "geen");
            Assert.IsTrue(test.StaffelKorting.BerekenKorting(999999999) == 0);
        }
        [TestMethod]
        public void StaffelKortingToevoegen()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string naam = "testKorting1";
            int b1 = 0;
            int b2 = 5;
            int b3 = 10;
            int b4 = 15;
            int b5 = 20;
            int b6 = 25;
            int b7 = 30;

            double k1 = 0;
            double k2 = 7;
            double k3 = 10;
            double k4 = 13;
            double k5 = 16;
            double k6 = 19;
            double k7 = 22;
            List<int> breekpunten = new List<int> {0, 5, 10, 15, 20, 25, 30 };
            List<double> kortingsPercentages = new List<double> {0, 7, 10, 13, 16, 19, 22 };

            rm.VoegStaffelKortingToe(naam, breekpunten, kortingsPercentages);
            StaffelKorting kP = rm.VindStaffelKortingVoorNaam(naam);

            Assert.IsTrue(kP.Naam == naam);
            Assert.IsTrue(kP.BerekenKorting(b1) == k1);
            Assert.IsTrue(kP.BerekenKorting(b2) == k2);
            Assert.IsTrue(kP.BerekenKorting(b3) == k3);
            Assert.IsTrue(kP.BerekenKorting(b4) == k4);
            Assert.IsTrue(kP.BerekenKorting(b5) == k5);
            Assert.IsTrue(kP.BerekenKorting(b6) == k6);
            Assert.IsTrue(kP.BerekenKorting(b7) == k7);
        }
        [TestMethod]
        public void ReservatieToevoegen()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            //Limo maken
            string naam = "testLimo1";
            int uurPrijs = 150;
            int nightlifePrijs1 = 2500;
            int weddingPrijs2 = 3000;
            int wellnessPrijs3 = 4213;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs2, wellnessPrijs3);
            //klant maken
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);
            //reservatie
            int klantNr = 1;
            int limoId = 1;
            DateTime startDatum = new DateTime(2100, 8, 12);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 22;
            int duur = 10;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie= StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Reservatie res = rm.VindReservatieVoorReservatieNummer(1);
            Klant klant = rm.VindVolledigeKlantVoorKlantNummer(1);
            Limousine limo = rm.FindLimousineVoorId(1);

            Assert.AreEqual(res.Klant, klant);
            Assert.AreEqual(res.Limousine, limo);
            Assert.AreEqual(res.StartMoment,startDatum.AddHours(startUur));
            Assert.AreEqual(res.AantalUur , duur);
            Assert.AreEqual(res.StartStalLocatie, stalLocatie);
            Assert.AreEqual(res.AankomstStalLocatie, eindLocatie);
            Assert.AreEqual(res.VerwachtAdres, verwachtAdres);
        }
        [TestMethod]
        public void GetAantalLimosTest()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            Assert.AreEqual(rm.GetAantalLimousines(), 0);

            rm.AddLimousine("test1", 2, null, null, null);
            rm.AddLimousine("test2", 2, null, null, null);
            rm.AddLimousine("test3", 2, null, null, null);
            rm.AddLimousine("test4", 2, null, null, null);
            rm.AddLimousine("test5", 2, null, null, null);

            Assert.AreEqual(rm.GetAantalLimousines(), 5);
        }
        [TestMethod]
        public void GetAantalKlantenTest()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            Assert.AreEqual(rm.GetAantalKlanten(), 0);

            rm.VoegKlantToe("testklant1", "test", "testbtw", "testadres");
            rm.VoegKlantToe("testklant2", "test", "testbtw", "testadres");
            rm.VoegKlantToe("testklant3", "test", "testbtw", "testadres");

            Assert.AreEqual(rm.GetAantalKlanten(), 3);
        }
        [TestMethod]
        public void GetBeschikbareLimousinesTest_ControleerKoppeling()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 150;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

            string naam2 = "testLimo2";
            int uurPrijs2 = 150;
            int nightlifePrijs2 = 2500;
            int weddingPrijs2 = 3000;
            int wellnessPrijs2 = 4213;
            rm.AddLimousine(naam2, uurPrijs2, nightlifePrijs2, weddingPrijs2, wellnessPrijs2);

            int klantNr = 1;
            int limoId = 1;
            DateTime startDatum = new DateTime(2100, 8, 12);
            Arrengement arrengement = Arrengement.Business;
            int startUur = 7;
            int duur = 3;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            int klantNr2 = 1;
            int limoId2 = 2;
            DateTime startDatum2 = new DateTime(2100, 8, 12);
            Arrengement arrengement2 = Arrengement.Airport;
            int startUur2 = 13;
            int duur2 = 7;
            StalLocatie stalLocatie2 = StalLocatie.Antwerpen;
            StalLocatie eindLocatie2 = StalLocatie.Gent;
            string verwachtAdres2 = "testAdres1 25 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr2, startDatum2, arrengement2, startUur2, duur2, limoId2, stalLocatie2, eindLocatie2, verwachtAdres2);

            DateTime startMoment1 = startDatum.AddHours(startUur);
            DateTime startMoment2 = startDatum2.AddHours(startUur2);

            Assert.IsTrue(rm.GetBeschikbareLimousines(startMoment1, startMoment1.AddHours(duur)).Count==0);
            Assert.IsTrue(rm.GetBeschikbareLimousines(new DateTime(2101, 8, 12, 10,0,0), new DateTime(2101, 8, 12, 19,0,0)).Count == 2);
            Assert.IsTrue(rm.GetBeschikbareLimousines(startMoment1.AddHours(-12), startMoment1.AddHours(-6)).Count == 2);
            Assert.IsTrue(rm.GetBeschikbareLimousines(startMoment2.AddHours(duur2+7), startMoment2.AddHours(duur2+8)).Count == 2);
            var limos = rm.GetBeschikbareLimousines(startMoment1.AddHours(duur+6), startMoment1.AddHours(duur+8));
            Assert.IsTrue(limos.Count == 1);
            Assert.IsTrue(limos[0].Id == 1);

            limos = rm.GetBeschikbareLimousines(startMoment2.AddHours(-10), startMoment2.AddHours(-6));
            Assert.IsTrue(limos.Count == 1);
            Assert.IsTrue(limos[0].Id == 2);

            limos = rm.GetBeschikbareLimousines(startMoment2.AddHours(-10), startMoment2.AddHours(-5));
            Assert.IsTrue(limos.Count == 0);

            limos = rm.GetBeschikbareLimousines(startMoment1.AddHours(5), startMoment1.AddHours(8));
            Assert.IsTrue(limos.Count == 0);
        }
        [TestMethod]
        public void FindKlantVoorBtwNummerTest_ZouCorrecteKlantMoetenReturnenEnEnkelMetDeExacteString()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "BE12345678";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            Klant testKlant = rm.FindKlantVoorBtwNummer(btwNummer);

            Assert.IsTrue(testKlant.Adres == adres, "Adres klopte niet");
            Assert.IsTrue(testKlant.BtwNummer == btwNummer, "Btw nummer klopte niet");
            Assert.IsTrue(testKlant.Categorie.Naam == klantenCategorie, "categorie naam klopte niet");
            Assert.IsTrue(testKlant.Categorie.StaffelKorting.Naam == "geen", "Staffelkorting naam klopte niet");
            Assert.IsTrue(testKlant.Naam == klantNaam, "klant naam klopte niet");

            testKlant = rm.FindKlantVoorBtwNummer(btwNummer.Substring(0, 1));
            Assert.IsNull(testKlant,"FindKlantVoorBtwNummer moet null returnen als de btwNummer string niet exact is");
        }
        [TestMethod]
        public void FindKlantVoorNaamTest()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string klantNaam2 = klantNaam+"2";
            string klantenCategorie2 = "testCategorie";
            string btwNummer2 = "TestBTwNummer123456789";
            string adres2 = "TestAdres";
            rm.VoegKlantToe(klantNaam2, klantenCategorie2, btwNummer2, adres2);
            string klantNaam3 = "testklantje";
            string klantenCategorie3= "testCategorie";
            string btwNummer3 = "TestBTwNummer123456789";
            string adres3 = "TestAdres";
            rm.VoegKlantToe(klantNaam3, klantenCategorie3, btwNummer3, adres3);



            var klanten = rm.FindKlantVoorNaam(klantNaam.Substring(0,6));
            Assert.IsTrue(klanten.Count == 3);

            klanten = rm.FindKlantVoorNaam(klantNaam3);
            Assert.IsTrue(klanten.Count == 1);
            Assert.IsTrue(klanten[0].Naam == klantNaam3);

            klanten = rm.FindKlantVoorNaam(klantNaam);
            Assert.IsTrue(klanten.Count == 2);
            Assert.IsTrue(klanten[0].KlantNummer == 1);
            Assert.IsTrue(klanten[1].KlantNummer == 2);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void GetBeschikbareLimousinesVoorEindDatumVRoegerDanStartDatum_ShouldThrowIncorrectParameterException()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler(true));
            DateTime datum = DateTime.Now;
            rm.GetBeschikbareLimousines(datum, datum.AddHours(-6));  
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void GetBeschikbareLimousinesVoorEindDatumGelijkAanStartDatum_ShouldThrowIncorrectParameterException()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler(true));
            DateTime datum = DateTime.Now;
            rm.GetBeschikbareLimousines(datum, datum);
        }
        [TestMethod]
        public void FindReservatieVoorKlantNaamTest()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 150;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

            string klantNaam2 = "testklant12";
            string klantenCategorie2 = "testCategorie";
            string btwNummer2 = "TestBTwNummer123456789";
            string adres2 = "TestAdres";
            rm.VoegKlantToe(klantNaam2, klantenCategorie2, btwNummer2, adres2);

            string klantNaam3 = "EenAndereKlantOmLeegTeLaten";
            string klantenCategorie3 = "testCategorie";
            string btwNummer3 = "TestBTwNummer123456789";
            string adres3 = "TestAdres";
            rm.VoegKlantToe(klantNaam3, klantenCategorie3, btwNummer3, adres3);

            int klantNr = 1;
            int limoId = 1;
            DateTime startDatum = new DateTime(2100, 8, 13);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 22;
            int duur = 9;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            int klantNr2 = 2;
            int limoId2 = 1;
            DateTime startDatum2 = new DateTime(2100, 8, 12);
            Arrengement arrengement2 = Arrengement.NightLife;
            int startUur2 = 22;
            int duur2 = 7;
            StalLocatie stalLocatie2 = StalLocatie.Antwerpen;
            StalLocatie eindLocatie2 = StalLocatie.Gent;
            string verwachtAdres2 = "testAdres2 50 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr2, startDatum2, arrengement2, startUur2, duur2, limoId2, stalLocatie2, eindLocatie2, verwachtAdres2);

            int klantNr3 = 2;
            int limoId3 = 1;
            DateTime startDatum3 = new DateTime(2100, 8, 14);
            Arrengement arrengement3 = Arrengement.NightLife;
            int startUur3 = 22;
            int duur3 = 7;
            StalLocatie stalLocatie3 = StalLocatie.Antwerpen;
            StalLocatie eindLocatie3 = StalLocatie.Gent;
            string verwachtAdres3 = "testAdres3 75 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr3, startDatum3, arrengement3, startUur3, duur3, limoId3, stalLocatie3, eindLocatie3, verwachtAdres3);

            var result = rm.FindReservatieDetailsVoorKlantNaam(klantNaam.Substring(0, 8));
            Assert.IsTrue(result.Count == 3);
            result = rm.FindReservatieDetailsVoorKlantNaam(klantNaam3);
            Assert.IsTrue(result.Count == 0);
            result = rm.FindReservatieDetailsVoorKlantNaam(klantNaam2);
            Assert.IsTrue(result.Count == 2);

        }
        [TestMethod]
        public void FindReservatieVoorKlantNaamEnDatumTest()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 150;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.AddLimousine(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

            string klantNaam2 = "testklant12";
            string klantenCategorie2 = "testCategorie";
            string btwNummer2 = "TestBTwNummer123456789";
            string adres2 = "TestAdres";
            rm.VoegKlantToe(klantNaam2, klantenCategorie2, btwNummer2, adres2);

            string klantNaam3 = "EenAndereKlantOmLeegTeLaten";
            string klantenCategorie3 = "testCategorie";
            string btwNummer3 = "TestBTwNummer123456789";
            string adres3 = "TestAdres";
            rm.VoegKlantToe(klantNaam3, klantenCategorie3, btwNummer3, adres3);

            int klantNr = 1;
            int limoId = 1;
            DateTime startDatum = new DateTime(2100, 8, 13);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 22;
            int duur = 9;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            int klantNr2 = 2;
            int limoId2 = 1;
            DateTime startDatum2 = new DateTime(2100, 8, 13);
            Arrengement arrengement2 = Arrengement.NightLife;
            int startUur2 = 22;
            int duur2 = 7;
            StalLocatie stalLocatie2 = StalLocatie.Antwerpen;
            StalLocatie eindLocatie2 = StalLocatie.Gent;
            string verwachtAdres2 = "testAdres2 50 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr2, startDatum2, arrengement2, startUur2, duur2, limoId2, stalLocatie2, eindLocatie2, verwachtAdres2);

            int klantNr3 = 2;
            int limoId3 = 1;
            DateTime startDatum3 = new DateTime(2100, 8, 14);
            Arrengement arrengement3 = Arrengement.NightLife;
            int startUur3 = 22;
            int duur3 = 7;
            StalLocatie stalLocatie3 = StalLocatie.Antwerpen;
            StalLocatie eindLocatie3 = StalLocatie.Gent;
            string verwachtAdres3 = "testAdres3 75 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr3, startDatum3, arrengement3, startUur3, duur3, limoId3, stalLocatie3, eindLocatie3, verwachtAdres3);

            var result = rm.FindReservatieDetailsVoorKlantNaamEnDatum(klantNaam, startDatum);
            Assert.IsTrue(result.Count == 2);
            result = rm.FindReservatieDetailsVoorKlantNaamEnDatum(klantNaam2, startDatum);
            Assert.IsTrue(result.Count == 1);
            result = rm.FindReservatieDetailsVoorKlantNaamEnDatum(klantNaam3,startDatum3);
            Assert.IsTrue(result.Count == 0);

        }
    }
}