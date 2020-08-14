using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary
{
    public class StaffelKorting
    {
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
        public void SetKortingen(List<int> breekPunten, List<int> kortingPercentages)
        {
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
