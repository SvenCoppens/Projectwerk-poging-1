using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary
{
    public class StaffelKorting
    {
        public StaffelKorting()
        {

        }
        public StaffelKorting(string naam,List<int> breekPunten, List<double> kortingsPercentages)
        {
            Naam = naam;
            SetKortingen(breekPunten, kortingsPercentages);
        }
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Kortingen { get; set; }
        public double BerekenKorting(int aantalReservaties)
        {
            string[] paren = Kortingen.Split(";");
            List<int> breekpunten = new List<int>();
            List<double> kortingen = new List<double>();
            foreach(string paar in paren)
            {
                string[] splitpaar = paar.Split(":");
                breekpunten.Add(int.Parse(splitpaar[0]));
                kortingen.Add(double.Parse(splitpaar[1]));
            }
            int index= 0;
            for(int i=0;i<breekpunten.Count;i++)
            {
                if (aantalReservaties > breekpunten[i])
                    index = i;
            }
            return kortingen[index];
        }
        public void SetKortingen(List<int> breekPunten, List<double> kortingPercentages)
        {
            if (breekPunten.Count != kortingPercentages.Count)
                throw new Exception("Aantal kortingsPercentages komt niet overeen met het aantal breekpunten");
            if (breekPunten[0] != 0)
                throw new Exception("Eerste breekpunt moet 0 zijn!");
            for(int i = 1; i < kortingPercentages.Count; i++)
            {
                if (kortingPercentages[i] <= kortingPercentages[i - 1])
                    throw new Exception("KortingsPercentages moeten groter zijn dan de vorige korting");
                if(breekPunten[i]<=breekPunten[i-1])
                    throw new Exception("Breekpunten moeten groter zijn dan het vorige breekPunt");
            }
            string kortingen = "";
            for(int i = 0; i < breekPunten.Count; i++)
            {
                kortingen += $"{breekPunten[i]}:{kortingPercentages[i]}";
                if (i < breekPunten.Count - 1)
                    kortingen += ";";
            }
            Kortingen = kortingen;
        }
    }
}
