using DomainLibrary.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLibrary
{
    public class Klant
    {
        public Klant()
        {

        }
        public Klant(int klantNummer, string naam, KlantenCategorie categorie, string btwNummer, string adres)
        {
            KlantNummer = klantNummer;
            Naam = naam;
            Categorie = categorie;
            BtwNummer = btwNummer;
            Adres = adres;
        }
        [Key]
        public int KlantNummer { get; set; }
        public string Naam { get; set; }
        public KlantenCategorie Categorie { get; set; }
        public string BtwNummer {get;set;}
        public string Adres { get; set; }
        public override string ToString()
        {
            return Naam;
        }
    }
}
