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
        public void VoegKlantToe(string naam, string klantenCategorie,string btw,string adres, int klantNummer = 0)
        {
            if (klantNummer == 0)
            {
                klantNummer = Handler.GetNewKlantNummer();
            }
            KlantenCategorie categorie = null;  

            categorie= VindKlantenCategorieVoorNaam(klantenCategorie);

            if (categorie == null)
            {
                VoegKlantenCategorieToe(klantenCategorie);
                categorie = VindKlantenCategorieVoorNaam(klantenCategorie);
            }
            Handler.AddKlant(new Klant(klantNummer, naam, categorie, btw, adres));
        }
        public KlantenCategorie VindKlantenCategorieVoorNaam(string klantenCategorie)
        {
            return Handler.VindKlantenCategorieVoorNaam(klantenCategorie);
        }
        public void VoegKlantenCategorieToe(string naam)
        {
            StaffelKorting staffelKorting = Handler.VindStaffelKortingVoorNaam(naam);
            if (staffelKorting == null)
            {
                if (naam.Contains("planner"))
                    staffelKorting = Handler.VindStaffelKortingVoorNaam("planner");
                else 
                { 
                    staffelKorting = Handler.VindStaffelKortingVoorNaam("geen");
                    if (staffelKorting == null)
                    {
                        VoegStaffelKortingToe("geen", new List<int> { 0 }, new List<double> { 0 });
                        staffelKorting = VindStaffelKortingVoorNaam("geen");
                    }
                }
            }
            KlantenCategorie categorie = new KlantenCategorie(naam, staffelKorting);
            Handler.VoegKlantenCategorieToe(categorie);
        }
        public StaffelKorting VindStaffelKortingVoorNaam(string StaffelKortingNaam)
        {
            return Handler.VindStaffelKortingVoorNaam(StaffelKortingNaam);
        }

        public void VoegStaffelKortingToe(string naam, List<int> breekpunten, List<double> kortingsPercentages)
        {
            StaffelKorting staffelKorting = new StaffelKorting(naam, breekpunten, kortingsPercentages);
            Handler.VoegStaffelKortingToe(staffelKorting);
        }
        public Reservatie ReservatieMakenEnReturnen(int klantNr,DateTime startDatum,Arrengement arrengement, int startUur,int duur,int limoId, StalLocatie startStalLocatie, StalLocatie aankomstStalLocatie, string verwachtAdres)
        {
            Klant klant = VindVolledigeKlantVoorKlantNummer(klantNr);
            //int reservatieNummer = GetNewReservatieNummer();
            Limousine limo = FindLimousineVoorId(limoId);
            double korting = 0;
            if(klant.Categorie.StaffelKorting!=null)
                korting = BerekenKortingsPercentage(klant,startDatum);
            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur,limo, DateTime.Now, startStalLocatie, aankomstStalLocatie, verwachtAdres,korting);
            AddReservatie(res);
            return res;
        }
        public void ReservatieMakenZonderReturnen(int klantNr, DateTime startDatum, Arrengement arrengement, int startUur, int duur, int limoId, StalLocatie startStalLocatie, StalLocatie aankomstStalLocatie, string verwachtAdres)
        {
            Klant klant = VindVolledigeKlantVoorKlantNummer(klantNr);
            //int reservatieNummer = GetNewReservatieNummer();
            Limousine limo = FindLimousineVoorId(limoId);
            double korting = 0;
            if (klant.Categorie.StaffelKorting != null)
                korting = BerekenKortingsPercentage(klant, startDatum);
            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur, limo, DateTime.Now, startStalLocatie, aankomstStalLocatie, verwachtAdres, korting);
            AddReservatie(res);
        }
        public Reservatie VindReservatieVoorReservatieNummer(int reservatieNummer)
        {
            return Handler.VindReservatieVoorReservatieNummer(reservatieNummer);
        }

        public Limousine FindLimousineVoorId(int id)
        {
            return Handler.FindLimousineVoorId(id);
        }

        public void AddReservatie(Reservatie reservatie)
        {
            Handler.AddReservatie(reservatie);
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
            if (eind <= start)
                throw new IncorrectParameterException("Eind van een zoekperiode mag niet voor het begin vallen");
            List<Limousine> limousines = Handler.GetLimousinesWithReservaties();

            List<Limousine> result = new List<Limousine>();
            foreach (Limousine limo in limousines)
            {
                if (limo.Reservaties.Count==0)
                {
                    result.Add(limo);
                }
                else {
                    bool beschikbaar = true;

                    limo.Reservaties.OrderBy(x => x.StartMoment);
                    foreach (Reservatie res in limo.Reservaties)
                    {
                        //beginuur nieuwe reservatie mag niet vallen tussen: beginuur reservatie en 6u na einduur reservatie
                        if (start > res.StartMoment.AddHours(-6) && start < res.StartMoment.AddHours(res.AantalUur + 6))
                            beschikbaar = false;
                        //zelde voor het eindUur
                        else if (eind > res.StartMoment.AddHours(-6) && eind < res.StartMoment.AddHours(res.AantalUur + 6))
                            beschikbaar = false;
                    }
                    if (beschikbaar)
                        result.Add(limo);
                }
            }
            return result;
        }
        public Klant FindKlantVoorBtwNummer(string btwNummer)
        {
            return Handler.FindKlantVoorBtwNummer(btwNummer);
        }
        public List<Klant> FindKlantVoorNaam(string naam)
        {
            return Handler.FindKlantVoorNaam(naam);
        }
        public Klant VindVolledigeKlantVoorKlantNummer(int klantNummer)
        {
            return Handler.FindVolledigeKlantVoorKlantNummer(klantNummer);
        }
        public List<Reservatie> FindReservatieDetailsVoorKlantNaam(string klantNaam)
        {
            return Handler.FindReservatieDetailsVoorKlantNaam(klantNaam);
        }
        public List<Reservatie> FindReservatieDetailsVoorKlantNummer(int klantNummer)
        {
            return Handler.FindReservatieDetailsVoorKlantNummer(klantNummer);
        }
        public List<Reservatie> FindReservatieDetailsVoorDatum(DateTime datum)
        {
            return Handler.FindReservatieDetailsVoorDatum(datum);
        }
        public List<Reservatie> FindReservatieDetailsVoorKlantNaamEnDatum(string klantNaam, DateTime datum)
        {
            return Handler.FindReservatieDetailsVoorKlantNaamEnDatum(klantNaam, datum);
        }
        public List<Reservatie> FindReservatieDetailsVoorKlantNummerEnDatum(int klantNummer, DateTime datum)
        {
            return Handler.FindReservatieDetailsVoorKlantNummerEnDatum(klantNummer, datum);
        }
        public int GetAantalReservatiesVoorKlantInJaar(Klant klant,int jaar)
        {
            return Handler.GetAantalReservatiesVoorKlantInJaar(klant, jaar);
        }
        public double BerekenKortingsPercentage(Klant klant,DateTime datum)
        {
            int aantalReservaties = GetAantalReservatiesVoorKlantInJaar(klant, datum.Year);
            return klant.Categorie.StaffelKorting.BerekenKorting(aantalReservaties);
        }
    }
}
