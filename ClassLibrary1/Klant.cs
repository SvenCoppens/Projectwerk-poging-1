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
        //klantnummer zou bepaald moeten worden vanuit de database, dus niet echt behandeld in de constructor????
        [Key]
        public int KlantNummer { get; set; }
        public string Naam { get; set; }
        public KlantenCategorie Categorie { get; set; }
        public string BtwNummer {get;set;}
        public string Adres { get; set; }
    }
}
