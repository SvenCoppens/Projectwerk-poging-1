using DomainLibrary.Enums;
using DomainLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace DomainLibrary
{
    public class ReservatieManager
    {
        private iReservatieDatabaseHandler Handler;
        public ReservatieManager(iReservatieDatabaseHandler handler)
        {
            Handler = handler;
            
        }
        public void AddLimousine(string naam,int eersteUur,int? nightlife,int? wedding, int? wellness)
        {
            //hier de exceptions schrijven?
            Handler.AddLimousine(new Limousine(naam,eersteUur,nightlife,wedding,wellness));
        }
        //klantnummer default gelijk aan 0 zodat we weten wanneer we een nieuw klantnummer moeten aanmaken.
        //Exceptions hier afhandelen want dit is de enige klasse die effectief alle nodige informatie heeft om deze exceptions te controleren.
        public void AddKlant(string naam,KlantenCategorie categorie,string btw,string adres, int klantNummer = 0)
        {
            if (klantNummer == 0)
            {
                klantNummer = Handler.GetNewKlantNummer();
            }
            //else
            //{
            //    if (Handler.BestaatKlantNummer(klantNummer))
            //    {
            //        throw new IncorrectParameterException("klantnummer was een ongeldige waarde");
            //    }
            //}
            Handler.AddKlant(new Klant(klantNummer,naam,categorie,btw,adres));

        }
        public void ReservatieMaken(int klantNr,DateTime startDatum,Arrengement arrengement, int startUur,int duur,Limousine limo, StalLocatie startStalLocatie, StalLocatie aankomstStalLocatie, string verwachtAdres)
        {
            Klant klant = FindKlantVoorKlantNummer(klantNr);
            //int reservatieNummer = GetNewReservatieNummer();
            limo = FindLimousineVoorId(limo.Id);
            double korting = 0;
            if(klant.StaffelKorting!=null)
                korting = BerekenKortingsPercentage(klant,startDatum);
            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur,limo, DateTime.Now, startStalLocatie, aankomstStalLocatie, verwachtAdres,korting);
            AddReservatie(res);
        }

        public Limousine FindLimousineVoorId(int id)
        {
            return Handler.FindLimousineVoorId(id);
        }

        public void AddReservatie(Reservatie reservatie)
        {
            Handler.AddReservatie(reservatie);
        }
        public List<Reservatie> GetReservatiesVoorKlant(Klant klant)
        {
            return Handler.GetReservatiesVoorKlant(klant);
        }
        public List<Reservatie> GetReservatiesVoorDatum(DateTime datum)
        {
            return Handler.GetReservatiesVoorDatum(datum);
        }
        public List<Reservatie> GetReservatiesVoorDatumEnKlant(Klant klant,DateTime datum)
        {
            return Handler.GetReservatiesVoorDatumEnKlant(klant, datum);
        }

        public int GetAantalLimousines()
        {
            return Handler.GetAantalLimousines();
        }
        public int GetAantalKlanten()
        {
            return Handler.GetAantalKlanten();
        }
        public List<Limousine> GetBeschikbareLimousines(DateTime start, DateTime eind)
        {
            List<Limousine> limousines = Handler.GetLimousinesWithReservaties();

            List<Limousine> result = new List<Limousine>();
            foreach (Limousine limo in limousines)
            {
                if (limo.Reservaties.Count==0)
                {
                    result.Add(limo);
                }
                else {
                    bool beschikbaar = false;

                    limo.Reservaties.OrderBy(x => x.StartMoment);
                    foreach (Reservatie res in limo.Reservaties)
                    {
                        //beginuur nieuwe reservatie mag niet vallen tussen: beginuur reservatie en 6u na einduur reservatie
                        if (!(start > res.StartMoment && start < res.StartMoment.AddHours(res.AantalUur + 6)))
                        {
                            if (eind < res.StartMoment.AddHours(-6))
                                beschikbaar = true;
                            else beschikbaar = false;
                        }
                        else beschikbaar = false;
                    }
                    if (beschikbaar)
                        result.Add(limo);
                }
            }
            return result;
        }
        public List<Klant> FindKlantVoorBtwNummer(string btwNummer)
        {
            return Handler.FindKlantVoorBtwNummer(btwNummer);
        }
        public List<Klant> FindKlantVoorNaam(string naam)
        {
            return Handler.FindKlantVoorNaam(naam);
        }
        public Klant FindKlantVoorKlantNummer(int klantNummer)
        {
            return Handler.FindKlantVoorKlantNummer(klantNummer);
        }
        public List<Reservatie> FindReservatieVoorKlantNaam(string klantNaam)
        {
            return Handler.FindReservatieVoorKlantNaam(klantNaam);
        }
        public List<Reservatie> FindReservatieVoorKlantNummer(int klantNummer)
        {
            return Handler.FindReservatieVoorKlantNummer(klantNummer);
        }
        public List<Reservatie> FindReservatieVoorDatum(DateTime datum)
        {
            return Handler.FindReservatieVoorDatum(datum);
        }
        public List<Reservatie> FindReservatieVoorKlantNaamEnDatum(string klantNaam, DateTime datum)
        {
            return Handler.FindReservatieVoorKlantNaamEnDatum(klantNaam, datum);
        }
        public List<Reservatie> FindReservatieVoorKlantNummerEnDatum(int klantNummer, DateTime datum)
        {
            return Handler.FindReservatieVoorKlantNummerEnDatum(klantNummer, datum);
        }
        //public int GetNewReservatieNummer()
        //{
        //    return Handler.GetNewReservatieNummer();
        //}
        public int GetAantalReservatiesVoorKlantInJaar(Klant klant,int jaar)
        {
            return Handler.GetAantalReservatiesVoorKlantInJaar(klant, jaar);
        }
        public double BerekenKortingsPercentage(Klant klant,DateTime datum)
        {
            int aantalReservaties = GetAantalReservatiesVoorKlantInJaar(klant, datum.Year);
            double korting = 0;
            //kortingenstring van het formaat "5:7.5;10:10 opsplitsen zodat we de gepaste korting kunnen uitrekenen
            string[] kortingSplits = klant.StaffelKorting.Kortingen.Split(";");
            List<int> breekpunten = new List<int>() { 0};
            List<double> kortingen = new List<double>() { 0};
            foreach(string combo in kortingSplits)
            {
                string[] temp = combo.Split(":");
                breekpunten.Add(int.Parse(temp[0]));
                kortingen.Add(double.Parse(temp[1]));
            }
            int teGebruikenIndex = 0;
            for(int i =0;i<breekpunten.Count;i++)
            {
                if (breekpunten[i] < aantalReservaties)
                    teGebruikenIndex = i;
            }
            korting = kortingen[teGebruikenIndex];
            return korting;
        }
    }
}
