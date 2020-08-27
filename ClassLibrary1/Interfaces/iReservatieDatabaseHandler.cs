using DomainLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.Interfaces
{
    public interface iReservatieDatabaseHandler
    {
        void VoegLimousineToe(Limousine limousine);
        void VoegKlantToe(Klant klant);
        void VoegReservatieToe(Reservatie reservatie);
        int GeefNieuwKlantNummer();
        int GeefAantalLimousines();
        int GeefAantalKlanten();
        List<Limousine> GeefLimousinesMetReservatie();
        Klant VindKlantVoorBtwNummer(string btwNummer);
        List<Klant>  VindKlantVoorNaam(string naam);
        List<Reservatie> VindReservatiesVoorKlantNaam(string klantNaam);
        List<Reservatie> VindReservatiesVoorKlantNummer(int klantNummer);
        List<Reservatie> VindReservatiesVoorDatum(DateTime datum);
        List<Reservatie> VindReservatiesVoorKlantNaamEnDatum(string klantNaam, DateTime datum);
        List<Reservatie> VindReservatiesVoorKlantNummerEnDatum(int klantNummer, DateTime datum);
        Reservatie VindReservatieVoorReservatieNummer(int reservatieNummer);
        Klant VindVolledigeKlantVoorKlantNummer(int klantNummer);
        int GeefAantalReservatiesVoorKlantInJaar(Klant klant, int jaar);
        Limousine VindLimousineVoorId(int id);
        KlantenCategorie VindKlantenCategorieVoorNaam(string klantenCategorie);
        StaffelKorting VindStaffelKortingVoorNaam(string naam);
        void VoegStaffelKortingToe(StaffelKorting staffelKorting);
        void VoegKlantenCategorieToe(KlantenCategorie categorie);
    }
}
