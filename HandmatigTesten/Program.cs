using DomainLibrary;
using DomainLibrary.Enums;
using System;
using System.Collections.Generic;

namespace HandmatigTesten
{
    class Program
    {
        static void Main(string[] args)
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


            startDatum = startDatum.AddDays(1);
            res = rm.ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, stalLocatie, eindLocatie, verwachtAdres);



            Console.WriteLine();
        }
    }
}
