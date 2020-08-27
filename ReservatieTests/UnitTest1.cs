using DomainLibrary;
using DomainLibrary.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ReservatieTests
{
    [TestClass]
    public class UnitTest1
    {
        #region StaffelKortingen
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingZonder0AlsBegin_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int> { 5 }, new List<double> { 5 });
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingZonderElementen_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int>(), new List<double>());
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingMetOngelijkAantalBreekPuntenEnKortingen_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int> {0,1,2,3,4 }, new List<double> {0,5,6,7,8,9 });
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingMoetGroeiendeBreekPuntenHebben_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int> { 0,1, 2, 4,3 }, new List<double> {0, 5, 6, 7, 8});
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingMagGeenNegatieveBreekPuntenBevatten_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int> { 0, -2, 4, 3 }, new List<double> { 0,5, 6, 7, 8 });
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingMoetGroeiendeKortingenHebben_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int> { 1, 2,  3,4 }, new List<double> { 5, 6, 8,7 });
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingMagGeenNegatieveKortingenHebben_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int> {0,- 1, 2, 3, 4 }, new List<double> { 0,-5, 6, 8, 7 });
        }
        [TestMethod]
        public void StaffelKortingGeeftCorrecteKorting()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int> { 0, 5, 10, 15 }, new List<double> { 0, 7, 13, 16 });

            Assert.AreEqual(test.BerekenKorting(2), 0);
            Assert.AreEqual(test.BerekenKorting(4), 0);
            Assert.AreEqual(test.BerekenKorting(5), 7);
            Assert.AreEqual(test.BerekenKorting(6), 7);
            Assert.AreEqual(test.BerekenKorting(10), 13);
            Assert.AreEqual(test.BerekenKorting(16), 16);
        }
        #endregion

        [TestMethod]
        public void AfrondingenOpTarieven_Test()
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
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

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
            Arrengement arrengement = Arrengement.Business;
            int startUur = 18;
            int duur = 10;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 210, "De nachtuur prijs werd niet correct afgerond.");

            startDatum = startDatum.AddDays(1);
            naam = "testLimo2";
            uurPrijs = 151;
            nightlifePrijs1 = 2500;
            weddingPrijs1 = 3000;
            wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);
            limoId++;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 210, "De nachtuur prijs werd niet correct afgerond.");

            startDatum = startDatum.AddDays(1);
            naam = "testLimo3";
            uurPrijs = 152;
            nightlifePrijs1 = 2500;
            weddingPrijs1 = 3000;
            wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);
            limoId++;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 215, "De nachtuur prijs werd niet correct afgerond.");

            startDatum = startDatum.AddDays(1);
            naam = "testLimo4";
            uurPrijs = 153;
            nightlifePrijs1 = 2500;
            weddingPrijs1 = 3000;
            wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);
            limoId++;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 215, "De nachtuur prijs werd niet correct afgerond.");

            startDatum = startDatum.AddDays(1);
            naam = "testLimo5";
            uurPrijs = 154;
            nightlifePrijs1 = 2500;
            weddingPrijs1 = 3000;
            wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);
            limoId++;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 215, "De nachtuur prijs werd niet correct afgerond.");

            startDatum = startDatum.AddDays(1);
            naam = "testLimo6";
            uurPrijs = 155;
            nightlifePrijs1 = 2500;
            weddingPrijs1 = 3000;
            wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);
            limoId++;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 215, "De nachtuur prijs werd niet correct afgerond.");

            startDatum = startDatum.AddDays(1);
            naam = "testLimo7";
            uurPrijs = 156;
            nightlifePrijs1 = 2500;
            weddingPrijs1 = 3000;
            wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);
            limoId++;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 220, "De nachtuur prijs werd niet correct afgerond.");

            startDatum = startDatum.AddDays(1);
            naam = "testLimo8";
            uurPrijs = 157;
            nightlifePrijs1 = 2500;
            weddingPrijs1 = 3000;
            wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);
            limoId++;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 220, "De nachtuur prijs werd niet correct afgerond.");

            startDatum = startDatum.AddDays(1);
            naam = "testLimo9";
            uurPrijs = 158;
            nightlifePrijs1 = 2500;
            weddingPrijs1 = 3000;
            wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);
            limoId++;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.StandaarUurPrijs == 105, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.NachtUurPrijs == 220, "De nachtuur prijs werd niet correct afgerond.");
        }
        [TestMethod]
        public void TarievenBerekeningAfronding_Test()
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
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

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
            Arrengement arrengement = Arrengement.Business;
            int startUur = 7;
            int duur = 3;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.StandaarUurPrijs == 100, "De standaard uurprijs werd niet correct afgerond.");
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 350, "Totaal werd niet correct berekend");
            Assert.IsTrue(res.TotaalTeBetalen == 371, "btw percentage werd niet correct toegevoegd");
        }
        [TestMethod]
        public void TarievenNachturenPrijsBerekening_Test()
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
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

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
            int startUur = 20;
            int duur = 7;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 0);
            Assert.IsTrue(res.VastePrijs == nightlifePrijs1);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == nightlifePrijs1, "Totaal werd niet correct berekend");
        }
        [TestMethod]
        public void BusinessEnAirportMetNachturenCorrectBerekening_Test()
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
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

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
            Arrengement arrengement = Arrengement.Business;
            int startUur = 20;
            int duur = 11;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 1);
            Assert.IsTrue(res.EersteUurPrijs == 150);
            Assert.IsTrue(res.AantalStandaardUur == 1);
            Assert.IsTrue(res.StandaarUurPrijs == 100);
            Assert.IsTrue(res.AantalNachtUur == 9);
            Assert.IsTrue(res.NachtUurPrijs == 210);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2140, "Totaal werd niet correct berekend");
        }

        [TestMethod]
        public void WellnessTariefBerekening_Test()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 200;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

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
            Arrengement arrengement = Arrengement.Wellness;
            int startUur = 11;
            int duur = 10;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 0);
            Assert.IsTrue(res.AantalStandaardUur == 0);
            Assert.IsTrue(res.AantalNachtUur == 0);
            Assert.IsTrue(res.VastePrijs == wellnessPrijs1);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == wellnessPrijs1, "Totaal werd niet correct berekend");
        }
        [TestMethod]
        public void NightlifeArrengement_Test()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 200;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

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
            int startUur = 20;
            int duur = 11;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 0);
            Assert.IsTrue(res.AantalStandaardUur == 0);
            Assert.IsTrue(res.AantalNachtUur == 4);
            Assert.IsTrue(res.NachtUurPrijs == 280);
            Assert.IsTrue(res.VastePrijs == nightlifePrijs1);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 3620, "Totaal werd niet correct berekend");
        }
        [TestMethod]
        public void WeddingArrengement_Test()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 200;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

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
            Arrengement arrengement = Arrengement.Wedding;
            int startUur = 15;
            int duur = 11;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 0);
            Assert.IsTrue(res.AantalStandaardUur == 0);
            Assert.IsTrue(res.AantalOverUur == 4);
            Assert.IsTrue(res.OverUurPrijs == 130);
            Assert.IsTrue(res.AantalNachtUur == 0);
            Assert.IsTrue(res.VastePrijs == 3000);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 3520, "Totaal werd niet correct berekend");
        }
        [TestMethod]
        public void NachtuurBerekeningVoorBusinessEnAirport_Test()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            string klantNaam = "testklant1";
            string klantenCategorie = "testCategorie";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 200;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

            int klantNr = 1;
            int limoId = 1;
            DateTime startDatum = new DateTime(2100, 8, 13);
            Arrengement arrengement = Arrengement.Business;
            int startUur = 1;
            int duur = 11;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 1);
            Assert.IsTrue(res.EersteUurPrijs == 200);
            Assert.IsTrue(res.AantalStandaardUur == 5);
            Assert.IsTrue(res.StandaarUurPrijs == 130);
            Assert.IsTrue(res.AantalNachtUur == 5);
            Assert.IsTrue(res.NachtUurPrijs == 280);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2250, "Totaal werd niet correct berekend");


            startDatum = startDatum.AddDays(1);
            startUur = 20;
            duur = 11;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 1);
            Assert.IsTrue(res.AantalStandaardUur == 1);
            Assert.IsTrue(res.AantalNachtUur == 9);

            startDatum = startDatum.AddDays(1);
            startUur = 21;
            duur = 11;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 1);
            Assert.IsTrue(res.AantalStandaardUur == 1);
            Assert.IsTrue(res.AantalNachtUur == 9);

            startDatum = startDatum.AddDays(1);
            startUur = 18;
            duur = 11;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 1);
            Assert.IsTrue(res.AantalStandaardUur == 3);
            Assert.IsTrue(res.AantalNachtUur == 7);

            startDatum = startDatum.AddDays(1);
            startUur = 7;
            duur = 11;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 1);
            Assert.IsTrue(res.AantalStandaardUur == 10);
            Assert.IsTrue(res.AantalNachtUur == 0);

            startDatum = startDatum.AddDays(1);
            startUur = 6;
            duur = 11;
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            Assert.IsTrue(res.AantalEersteUur == 1);
            Assert.IsTrue(res.AantalStandaardUur == 10);
            Assert.IsTrue(res.AantalNachtUur == 0);
        }
        [TestMethod]
        public void KortingCorrectToegepast_Test()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());

            rm.VoegStaffelKortingToe("test", new List<int> { 0, 2, 4, 6 }, new List<double> { 0, 8, 15, 20 });

            string klantNaam = "testklant1";
            string klantenCategorie = "test";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 200;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

            int klantNr = 1;
            int limoId = 1;
            DateTime startDatum = new DateTime(2100, 8, 13);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 23;
            int duur = 7;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            rm.ReservatieMakenZonderReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            startDatum = startDatum.AddDays(1);
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2500);

            startDatum = startDatum.AddDays(1);
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2300);

            startDatum = startDatum.AddDays(1);
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2300);

            startDatum = startDatum.AddDays(1);
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2125);

            startDatum = startDatum.AddDays(1);
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2125);

            startDatum = startDatum.AddDays(1);
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2000);
        }
        [TestMethod]
        public void KortingCorrectToegepastMetEindeVanHetJaar_Test()
        {
            ReservatieManager rm = new ReservatieManager(new TestDataBaseHandler());
            rm.VoegStaffelKortingToe("test", new List<int> { 0, 2, 4, 6 }, new List<double> { 0, 8, 15, 20 });

            string klantNaam = "testklant1";
            string klantenCategorie = "test";
            string btwNummer = "TestBTwNummer123456789";
            string adres = "TestAdres";
            rm.VoegKlantToe(klantNaam, klantenCategorie, btwNummer, adres);

            string naam = "testLimo1";
            int uurPrijs = 200;
            int nightlifePrijs1 = 2500;
            int weddingPrijs1 = 3000;
            int wellnessPrijs1 = 4213;
            rm.VoegLimousineToe(naam, uurPrijs, nightlifePrijs1, weddingPrijs1, wellnessPrijs1);

            int klantNr = 1;
            int limoId = 1;
            DateTime startDatum = new DateTime(2100, 12, 31);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 23;
            int duur = 7;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";


            rm.ReservatieMakenZonderReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            startDatum = startDatum.AddDays(1);
            Reservatie res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2500);

            startDatum = startDatum.AddDays(1);
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2500);

            startDatum = startDatum.AddDays(1);
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);
            Assert.IsTrue(res.VastePrijs == 2500);
            Assert.IsTrue(res.TotaalMetKortingExclusiefBtw == 2300);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void ReservatieMagMaximaal11UurDuren_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 23;
            int duur = 12;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void ReservatieAanmakenDieNietOpHetUurBegint_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 0, 1, 0);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 23;
            int duur = 12;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void ReservatieAanmakenWaarDeStartDatumEenTijdBevat_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 23;
            int duur = 12;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void ReservatieAanmakenMetNegatieveDuur_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 23;
            int duur = -1;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void ReservatieAanmakenWaarHetStartUurNegatiefIs_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = -1;
            int duur = 12;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void ReservatieAanmakenZonderDuur_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = -1;
            int duur = 0;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void WellnessArrangementMetMeerDan10Uur_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.Wellness;
            int startUur = 10;
            int duur = 11;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void WellnessArrangementMetMinderDan10Uur_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.Wellness;
            int startUur = 10;
            int duur = 9;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void WellnessArrangementMagNietVoor7UurBeginnen_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.Wellness;
            int startUur = 6;
            int duur = 10;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void WellnessArrangementMagNietNa12UurBeginnen_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.Wellness;
            int startUur = 13;
            int duur = 10;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void NightlifeArrangementDuurtAltijdMinstens7Uur_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 22;
            int duur = 6;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void NightlifeArrangementMagNietVroegerDan20UurBeginnen_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 19;
            int duur = 7;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void WeddingArrangementDuurtAltijdMinstens7Uur_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.Wedding;
            int startUur = 22;
            int duur = 6;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void WeddingArrangementMagNietVroegerDan7UurBeginnen_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.Wedding;
            int startUur = 6;
            int duur = 7;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void WeddingArrangementMagNietLaterDan15UurBeginnen_ZouIncorrecteParameterExceptionMoetenGeven()
        {
            Klant klant = new Klant();
            Limousine limo = new Limousine();

            DateTime startDatum = new DateTime(2100, 12, 31, 1, 0, 0);
            Arrengement arrengement = Arrengement.Wedding;
            int startUur = 16;
            int duur = 7;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";

            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, stalLocatie, eindLocatie, verwachtAdres, 0);
        }
    }
}
