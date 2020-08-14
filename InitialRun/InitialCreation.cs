using DataLayer;
using DomainLibrary;
using DomainLibrary.Enums;
using System;
using System.IO;

namespace InitialRun
{
    public class InitialCreation
    {
        public static void CreateInitialDatabaseLimousines()
        {
            //een controle implementeren dat dit programma enkel wordt gerund als de databank leeg is?

            //al de Limousines toevoegen aan de databank.
            ReservatieManager manager = new ReservatieManager(new ReservatieContext());
            manager.AddLimousine("Porsche Cayenne Limousine White",310,1500,1200,1600);
            manager.AddLimousine("Porsche Cayenne Limousine Electric Blue", 350, 1600, 1300, 1750);
            manager.AddLimousine("Mercedes SL 55 AMG Silver", 225, null, 700, 1000);
            manager.AddLimousine("Tesla Model X - White", 600, null, 2500, 2700);
            manager.AddLimousine("Tesla Model S - White", 500, null, 2000, 2200);

            manager.AddLimousine("Porsche Cayenne Limousine White", 300, 1500, 1000, null);
            manager.AddLimousine("Porsche Cayenne Limousine White", 300, 1500, 1000, null);
            manager.AddLimousine("Porsche Cayenne Limousine White", 300, 1500, 1000, null);
            manager.AddLimousine("Porsche Cayenne Limousine White", 300, 1500, 1000, null);
            manager.AddLimousine("Porsche Cayenne Limousine White", 300, 1500, 1000, null);

            manager.AddLimousine("Lincoln White XXL Navigator Limousine", 255, 790, 650, 950);
            manager.AddLimousine("Lincoln Pink Limousine", 180, 900, 500, 1000);
            manager.AddLimousine("Lincoln Black Limousine", 195, 850, 600, 1000);
            manager.AddLimousine("Hummer Limousine Yellow", 225, 1290, 790, 1500);
            manager.AddLimousine("Hummer Limousine Black", 195, 990, null,1100);
            manager.AddLimousine("Hummer Limousine White", 195, 990, null,null);
            manager.AddLimousine("Chrysler 300C Sedan - White", 175, null, 450, 600);
            manager.AddLimousine("Chrysler 300C Sedan - Black", 175, null, 450, 600);
            manager.AddLimousine("Chrysler 300C Limousine - White", 175, 800, 500, 1000);
            manager.AddLimousine("Chrysler 300C Limousine - Tuxedo Crème", 180, 800, 600, null);

            //al de bestaanden klanten toevoegen aan de databank
            using(StreamReader reader = new StreamReader(@"D:\Programmeren Data en Bestanden\Programmeren Projectwerk.klanten.txt"))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(",");
                    int klantNummer = int.Parse(splitLine[0]);
                    string naam = splitLine[1];
                    KlantenCategorie categorie = (KlantenCategorie)Enum.Parse(typeof(KlantenCategorie), splitLine[2],true);
                    string btw = null;
                    if (splitLine[3] != "")
                        btw = splitLine[3];
                    string adres = splitLine[4];

                    manager.AddKlant(klantNummer, naam, categorie, btw, adres);
                }
            }
        }
        public static void CreateInitialDatabaseKlanten()
        {
            string klantenBestand = @"D:\Programmeren Data en Bestanden\Programmeren Projectwerk\klanten.txt";

            using (StreamReader sr = new StreamReader(klantenBestand))
            {
                string line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    ReservatieManager manager = new ReservatieManager(new ReservatieContext());
                    string[] splitline = line.Split(",");
                    int klantnummer = int.Parse(splitline[0]);
                    string naam = splitline[1];
                    KlantenCategorie categorie = (KlantenCategorie)Enum.Parse(typeof(KlantenCategorie), splitline[2]);
                    string btwNummer = splitline[3];
                    string adres = splitline[4];
                    manager.AddKlant(naam, categorie, btwNummer, adres, klantnummer);
                }
            }
        }
    }
}
