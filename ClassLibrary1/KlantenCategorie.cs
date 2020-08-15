using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainLibrary
{
    public class KlantenCategorie
    {
        public KlantenCategorie()
        {

        }
        public KlantenCategorie(string naam,StaffelKorting staffelKorting)
        {
            Naam = naam;
            StaffelKorting = staffelKorting;
        }
        [Key]
        public string Naam { get; set; }
        public StaffelKorting StaffelKorting { get; set; }
        public ICollection<Klant> Klanten { get; set; }
        public override string ToString()
        {
            return Naam;
        }
    }
}
