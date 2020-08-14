using DomainLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary
{
    public class Limousine
    {
        public Limousine() { }
        public Limousine(string naam,double eersteUurPrijs,double? nightlifePrijs,double? weddingPrijs, double? wellnessPrijs)
        {
            Naam = naam;
            EersteUurPrijs = eersteUurPrijs;
            NightlifePrijs = nightlifePrijs;
            WeddingPrijs = weddingPrijs;
            WellnessPrijs = wellnessPrijs;
        }
        public int Id { get; set; }
        public string Naam { get; set; }
        public double EersteUurPrijs { get; set; }
        public double? NightlifePrijs { get; set; }
        public double? WeddingPrijs { get; set; }
        public double? WellnessPrijs { get; set; }


        public override string ToString()
        {
            return Naam;
        }

        public ICollection<Reservatie> Reservaties { get; set; }
    }
}
