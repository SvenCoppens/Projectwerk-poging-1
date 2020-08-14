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
        List<Reservatie> FindReservatieVoorKlantNaam(string klantNaam);
        List<Reservatie> FindReservatieVoorKlantNummer(int klantNummer);
        List<Reservatie> FindReservatieVoorDatum(DateTime datum);
        List<Reservatie> FindReservatieVoorKlantNaamEnDatum(string klantNaam, DateTime datum);
        List<Reservatie> FindReservatieVoorKlantNummerEnDatum(int klantNummer, DateTime datum);
        List<Reservatie> GetReservatiesVoorKlant(Klant klant);
        List<Reservatie> GetReservatiesVoorDatum(DateTime date);
        List<Reservatie> GetReservatiesVoorDatumEnKlant(Klant klant, DateTime datum);
        Klant FindKlantVoorKlantNummer(int klantNummer);
        //int GetNewReservatieNummer();
        int GetAantalReservatiesVoorKlantInJaar(Klant klant, int jaar);
        Limousine FindLimousineVoorId(int id);
    }
}
