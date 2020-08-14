using DomainLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary
{
    public class ReserveringsDetails
    {
        public ReserveringsDetails()
        {

        }
        public ReserveringsDetails(Limousine limo,DateTime startDatum,Arrengement arrengement,int startUur,int aantalUur,
            StalLocatie startAdres,StalLocatie eindAdres,string verwachtAdres, double aangerekendeKorting)
        {
            Limousine = limo;
            StartStalLocatie = startAdres;
            AankomstStalLocatie = eindAdres;
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
        public StalLocatie StartStalLocatie { get; set; }
        public StalLocatie AankomstStalLocatie { get; set; }
        public string VerwachtAdres { get; set; }
        public DateTime StartMoment { get; set; }
        public Limousine Limousine { get; set; }
        public Arrengement Arrengement { get; set; }
        public int AantalUur {get;set;}

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
        public double TotaalExclusiefBtw { get; set; }
        public double BtwBedrag { get; set; }
        public double TotaalTeBetalen { get; set; }

        private void PrijsBerekening()
        {
            double btwPercentage = 0.06;
            if (Arrengement == Arrengement.Wellness)
            {
                TotaalExclusiefBtw = (double)Limousine.WellnessPrijs;
                BtwBedrag = TotaalExclusiefBtw * btwPercentage;
                TotaalTeBetalen = TotaalExclusiefBtw + BtwBedrag;
            }
            else if (Arrengement == Arrengement.Wedding)
            {
                //maximum aantal uren controle nog toevoegen.
                OverUurPrijs = (Math.Round(Limousine.EersteUurPrijs * 0.65) / 5) * 5;
                AantalOverUur = AantalUur - 7;
                TotaalExclusiefBtw = AantalOverUur * OverUurPrijs;
                //TotaalExclusiefBtw = WeddingExtraUrenPrijsBerekening(AantalUur, Limousine);

                VastePrijs = (double)Limousine.WeddingPrijs;
                TotaalExclusiefBtw += VastePrijs;
                BtwBedrag = TotaalExclusiefBtw * btwPercentage;
                TotaalTeBetalen = TotaalExclusiefBtw + BtwBedrag;
            }
            else if (Arrengement == Arrengement.NightLife)
            {
                NachtUurPrijs = (Math.Round(Limousine.EersteUurPrijs * 1.4) / 5) * 5;
                AantalNachtUur = AantalUur - 7;
                TotaalExclusiefBtw = AantalNachtUur*NachtUurPrijs;

                VastePrijs = (double)Limousine.NightlifePrijs;
                TotaalExclusiefBtw += VastePrijs;
                BtwBedrag = TotaalExclusiefBtw * btwPercentage;
                TotaalTeBetalen = TotaalExclusiefBtw + BtwBedrag;
            }
            else if (Arrengement == Arrengement.Business || Arrengement == Arrengement.Airport)
            {
                EersteUurPrijs = Limousine.EersteUurPrijs;
                AantalEersteUur = 1;
                TotaalExclusiefBtw = EersteUurPrijs * AantalEersteUur;

                StandaarUurPrijs = (Math.Round(Limousine.EersteUurPrijs * 0.65) / 5) * 5;
                AantalStandaardUur = AantalUur - 1;
                TotaalExclusiefBtw += AantalStandaardUur * StandaarUurPrijs;
                BtwBedrag = TotaalExclusiefBtw * btwPercentage;
                TotaalTeBetalen = BtwBedrag + TotaalExclusiefBtw;
            }
            else
            {
                throw new NotImplementedException("New Arrengement is not properly implemented.");
            }
        }
        //private double WeddingExtraUrenPrijsBerekening(int aantalUur,Limousine limousine)
        //{
        //    return aantalUur * (Math.Round(limousine.EersteUurPrijs * 0.65) / 5) * 5;
        //}
        //private double NightlifeExtraUrenPrijsBerekening(int aantalUur, Limousine limousine)
        //{
        //    return aantalUur * (Math.Round(limousine.EersteUurPrijs * 1.4) / 5) * 5;
        //}
        //private double PerUurPrijsBerekening(int aantalUur,Limousine limousine)
        //{
        //    double resultaat = 0;
        //    resultaat = limousine.EersteUurPrijs;
        //    if(aantalUur>1)
        //        resultaat+= (aantalUur-1) * (Math.Round(limousine.EersteUurPrijs * 0.65) / 5) * 5;
        //    return resultaat;
        //}
    }
}
