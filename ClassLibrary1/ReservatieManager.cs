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
        public void VoegLimousineToe(string naam,int eersteUur,int? nightlife,int? wedding, int? wellness)
        {
            Handler.VoegLimousineToe(new Limousine(naam,eersteUur,nightlife,wedding,wellness));
        }
        public void VoegKlantToe(string naam, string klantenCategorie,string btw,string adres, int klantNummer = 0)
        {
            if (klantNummer == 0)
            {
                klantNummer = Handler.GeefNieuwKlantNummer();
            }
            KlantenCategorie categorie = null;  

            categorie= VindKlantenCategorieVoorNaam(klantenCategorie);

            if (categorie == null)
            {
                VoegKlantenCategorieToe(klantenCategorie);
                categorie = VindKlantenCategorieVoorNaam(klantenCategorie);
            }
            Handler.VoegKlantToe(new Klant(klantNummer, naam, categorie, btw, adres));
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
            Limousine limo = VindLimousineVoorId(limoId);
            double korting = 0;
            if(klant.Categorie.StaffelKorting!=null)
                korting = BerekenKortingsPercentage(klant,startDatum);
            Reservatie res = new Reservatie(klant, startDatum, arrengement, startUur, duur,limo, DateTime.Now, startStalLocatie, aankomstStalLocatie, verwachtAdres,korting);
            VoegReservatieToe(res);
            return res;
        }
        public void ReservatieMakenZonderReturnen(int klantNr, DateTime startDatum, Arrengement arrengement, int startUur, int duur, int limoId, StalLocatie startStalLocatie, StalLocatie aankomstStalLocatie, string verwachtAdres)
        {
            var res = ReservatieMakenEnReturnen(klantNr, startDatum, arrengement, startUur, duur, limoId, startStalLocatie, aankomstStalLocatie, verwachtAdres);
        }
        public Reservatie VindReservatieVoorReservatieNummer(int reservatieNummer)
        {
            return Handler.VindReservatieVoorReservatieNummer(reservatieNummer);
        }

        public Limousine VindLimousineVoorId(int id)
        {
            return Handler.VindLimousineVoorId(id);
        }

        private void VoegReservatieToe(Reservatie reservatie)
        {
            Handler.VoegReservatieToe(reservatie);
        }
        public int GeefAantalLimousines()
        {
            return Handler.GeefAantalLimousines();
        }
        public int GeefAantalKlanten()
        {
            return Handler.GeefAantalKlanten();
        }
        public List<Limousine> GeefBeschikbareLimousinesVoorPeriode(DateTime start, DateTime eind)
        {
            if (eind <= start)
                throw new IncorrectParameterException("Eind van een zoekperiode mag niet voor het begin vallen");
            List<Limousine> limousines = Handler.GeefLimousinesMetReservatie();

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
        public Klant VindKlantVoorBtwNummer(string btwNummer)
        {
            return Handler.VindKlantVoorBtwNummer(btwNummer);
        }
        public List<Klant> VindKlantVoorNaam(string naam)
        {
            return Handler.VindKlantVoorNaam(naam);
        }
        public Klant VindVolledigeKlantVoorKlantNummer(int klantNummer)
        {
            return Handler.VindVolledigeKlantVoorKlantNummer(klantNummer);
        }
        public List<Reservatie> VindReservatiesVoorKlantNaam(string klantNaam)
        {
            return Handler.VindReservatiesVoorKlantNaam(klantNaam);
        }
        public List<Reservatie> VindReservatiesVoorKlantNummer(int klantNummer)
        {
            return Handler.VindReservatiesVoorKlantNummer(klantNummer);
        }
        public List<Reservatie> VindReservatiesVoorDatum(DateTime datum)
        {
            return Handler.VindReservatiesVoorDatum(datum);
        }
        public List<Reservatie> VindReservatiesVoorKlantNaamEnDatum(string klantNaam, DateTime datum)
        {
            return Handler.VindReservatiesVoorKlantNaamEnDatum(klantNaam, datum);
        }
        public List<Reservatie> VindReservatiesVoorKlantNummerEnDatum(int klantNummer, DateTime datum)
        {
            return Handler.VindReservatiesVoorKlantNummerEnDatum(klantNummer, datum);
        }
        public int GeefAantalReservatiesVoorKlantInJaar(Klant klant,int jaar)
        {
            return Handler.GeefAantalReservatiesVoorKlantInJaar(klant, jaar);
        }
        public double BerekenKortingsPercentage(Klant klant,DateTime datum)
        {
            int aantalReservaties = GeefAantalReservatiesVoorKlantInJaar(klant, datum.Year);
            return klant.Categorie.StaffelKorting.BerekenKorting(aantalReservaties);
        }
    }
}
