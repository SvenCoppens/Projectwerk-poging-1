using DomainLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Schema;

namespace DomainLibrary
{
    public class Reservatie
    {
        public Reservatie() { }
        public Reservatie(Klant klant,DateTime startDatum,Arrengement arrengement,int startUur,int aantalUur,Limousine limo,DateTime datumVanAanmaakReservatie,
            StalLocatie startStalLocatie,StalLocatie aankomstStalLocatie, string verwachtAdres,double aangerekendeKorting)
        {
            Klant = klant;
            DatumVanReservering = datumVanAanmaakReservatie;
           // ReserveringsNummer = reservatieNummer;
            Limousine = limo;
            StartStalLocatie = startStalLocatie;
            AankomstStalLocatie = aankomstStalLocatie;
            StartMoment = startDatum.AddHours(startUur);
            Arrengement = arrengement;
            AantalUur = aantalUur;
            AangerekendeKorting = aangerekendeKorting;
            VerwachtAdres = verwachtAdres;

            if (TotaalTeBetalen == 0)
            {
                PrijsBerekening();
            }
        }
        public Klant Klant { get; set; }
        public DateTime DatumVanReservering { get; set; }
        [Key]
        public int ReserveringsNummer { get; set; }
        public StalLocatie StartStalLocatie { get; set; }
        public StalLocatie AankomstStalLocatie { get; set; }
        public string VerwachtAdres { get; set; }
        public DateTime StartMoment { get; set; }
        public Limousine Limousine { get; set; }
        public Arrengement Arrengement { get; set; }
        public int AantalUur { get; set; }

        //uren
        public int AantalEersteUur { get; set; }
        public int AantalStandaardUur { get; set; }
        public int AantalOverUur { get; set; }
        public int AantalNachtUur { get; set; }
        //prijzen
        public double VastePrijs { get; set; }
        public double EersteUurPrijs { get; set; }
        public double StandaarUurPrijs { get; set; }
        public double OverUurPrijs { get; set; }
        public double NachtUurPrijs { get; set; }

        public double AangerekendeKorting { get; set; }
        public double TotaalMetKortingExclusiefBtw { get; set; }
        public double BtwBedrag { get; set; }
        public double TotaalTeBetalen { get; set; }

        private void PrijsBerekening()
        {
            double btwPercentage = 0.06;
            if (Arrengement == Arrengement.Wellness)
            {
                TotaalMetKortingExclusiefBtw = (double)Limousine.WellnessPrijs;
                
            }
            else if (Arrengement == Arrengement.Wedding)
            {
                //maximum aantal uren controle nog toevoegen.
                OverUurPrijs = (Math.Round(Limousine.EersteUurPrijs * 0.65) / 5) * 5;
                AantalOverUur = AantalUur - 7;
                TotaalMetKortingExclusiefBtw = AantalOverUur * OverUurPrijs;
                //TotaalExclusiefBtw = WeddingExtraUrenPrijsBerekening(AantalUur, Limousine);

                VastePrijs = (double)Limousine.WeddingPrijs;
                TotaalMetKortingExclusiefBtw += VastePrijs;
            }
            else if (Arrengement == Arrengement.NightLife)
            {
                NachtUurPrijs = (Math.Round(Limousine.EersteUurPrijs * 1.4) / 5) * 5;
                AantalNachtUur = AantalUur - 7;
                TotaalMetKortingExclusiefBtw = AantalNachtUur * NachtUurPrijs;

                VastePrijs = (double)Limousine.NightlifePrijs;
                TotaalMetKortingExclusiefBtw += VastePrijs;
            }
            else if (Arrengement == Arrengement.Business || Arrengement == Arrengement.Airport)
            {
                EersteUurPrijs = Limousine.EersteUurPrijs;
                AantalEersteUur = 1;
                TotaalMetKortingExclusiefBtw = EersteUurPrijs * AantalEersteUur;

                DateTime eindMoment = StartMoment.AddHours(AantalUur);

                //geen nachturen (BUITEN)
                if (StartMoment.Hour < 22 && StartMoment.Hour >= 7 && AantalUur < 22 - StartMoment.Hour)
                {
                    AantalStandaardUur = AantalUur - 1;
                }
                //Enkel nachturen (BINNEN)
                else if(StartMoment.Hour>=22 && (eindMoment.Hour < 7||eindMoment.Day==StartMoment.Day))
                {
                    AantalNachtUur = AantalUur - 1;
                }
                //begint voor de nachturen
                else if (StartMoment.Hour < 22 && StartMoment.Hour>=7)
                {
                    //loopt niet door tot de volgende ochtend
                    if (eindMoment.Hour < 7 || eindMoment.Day == StartMoment.Day)
                    {
                        AantalStandaardUur = 22 - StartMoment.Hour - 1;
                        AantalNachtUur = AantalUur - 1 - AantalStandaardUur;
                    }
                    //loopt wel door tot de volgende ochtend
                    else
                    {
                        //begint ervoor, loopt door tot na => maximaal aantal nachturen
                        AantalNachtUur = 9;
                        AantalStandaardUur = AantalUur - 1 - AantalNachtUur;
                    }
                }
                //begint in de nachturen, en loopt door tot erna
                else
                {
                    AantalStandaardUur = eindMoment.Hour - 7;
                    AantalNachtUur = AantalUur - 1 - AantalStandaardUur;
                }
                StandaarUurPrijs = (Math.Round(Limousine.EersteUurPrijs * 0.65) / 5) * 5;
                NachtUurPrijs = (Math.Round(Limousine.EersteUurPrijs * 1.4) / 5) * 5;
                TotaalMetKortingExclusiefBtw += AantalStandaardUur * StandaarUurPrijs;
                TotaalMetKortingExclusiefBtw += AantalNachtUur * NachtUurPrijs;

                TotaalMetKortingExclusiefBtw = TotaalMetKortingExclusiefBtw * (1 - (AangerekendeKorting / 100));
            }
            else
            {
                throw new NotImplementedException("New Arrengement is not properly implemented.");
            }
            BtwBedrag = TotaalMetKortingExclusiefBtw * btwPercentage;
            TotaalTeBetalen = (TotaalMetKortingExclusiefBtw + BtwBedrag);
        }
    }
}
