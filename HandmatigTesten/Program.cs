using DomainLibrary;
using DomainLibrary.Enums;
using System;

namespace HandmatigTesten
{
    class Program
    {
        static void Main(string[] args)
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
            Arrengement arrengement = Arrengement.NightLife;
            int startUur = 7;
            int duur = 3;
            StalLocatie stalLocatie = StalLocatie.Antwerpen;
            StalLocatie eindLocatie = StalLocatie.Gent;
            string verwachtAdres = "testAdres1 25 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);

            int klantNr2 = 1;
            int limoId2 = 2;
            DateTime startDatum2 = new DateTime(2100, 8, 12);
            Arrengement arrengement2 = Arrengement.NightLife;
            int startUur2 = 13;
            int duur2 = 7;
            StalLocatie stalLocatie2 = StalLocatie.Antwerpen;
            StalLocatie eindLocatie2 = StalLocatie.Gent;
            string verwachtAdres2 = "testAdres1 25 testGemeente";
            rm.ReservatieMakenZonderReturnen(klantNr2, startDatum2, arrengement2, startUur2, duur2, limoId2, stalLocatie2, eindLocatie2, verwachtAdres2);

            var result = rm.GetBeschikbareLimousines(startDatum, startDatum.AddHours(duur));

            Console.WriteLine();
        }
    }
}
