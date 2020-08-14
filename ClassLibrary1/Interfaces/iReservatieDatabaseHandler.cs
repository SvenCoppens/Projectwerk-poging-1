using DomainLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.Interfaces
{
    public interface iReservatieDatabaseHandler
    {
        void AddLimousine(Limousine limousine);
        void AddKlant(Klant klant);
        void AddReservatie(Reservatie reservatie);
        int GetNewKlantNummer();
        bool BestaatKlantNummer(int klantNummer);
        int GetAantalLimousines();
        int GetAantalKlanten();
        List<Limousine> GetLimousinesWithReservaties();
        List<Klant> FindKlantVoorBtwNummer(string btwNummer);
        List<Klant>  FindKlantVoorNaam(string naam);
        List<Reservatie> FindReservatieDetailsVoorKlantNaam(string klantNaam);
        List<Reservatie> FindReservatieDetailsVoorKlantNummer(int klantNummer);
        List<Reservatie> FindReservatieDetailsVoorDatum(DateTime datum);
        List<Reservatie> FindReservatieDetailsVoorKlantNaamEnDatum(string klantNaam, DateTime datum);
        List<Reservatie> FindReservatieDetailsVoorKlantNummerEnDatum(int klantNummer, DateTime datum);
        Klant FindVolledigeKlantVoorKlantNummer(int klantNummer);
        //int GetNewReservatieNummer();
        int GetAantalReservatiesVoorKlantInJaar(Klant klant, int jaar);
        Limousine FindLimousineVoorId(int id);
        KlantenCategorie VindKlantenCategorieVoorNaam(string klantenCategorie);
        StaffelKorting VindStaffelKortingVoorNaam(string naam);
        void VoegStaffelKortingToe(StaffelKorting staffelKorting);
        void VoegKlantenCategorieToe(KlantenCategorie categorie);
    }
}
