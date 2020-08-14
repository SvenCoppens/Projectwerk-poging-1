using DomainLibrary;
using DomainLibrary.Enums;
using DomainLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayer
{
    public class ReservatieDatabaseHandler : ReservatieContext, iReservatieDatabaseHandler
    {
        private string _connectionString;
        public ReservatieDatabaseHandler(string db = "Production") : base()
        {
            Dictionary<string, string> connectionStrings = new Dictionary<string, string>();
            //@"ConnectionStrings.txt"
            using (StreamReader reader = new StreamReader(@"C:\Users\Sven\source\repos\Projectwerk poging 1\DataLayer\ConnectionStrings.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitline = line.Split(":");
                    connectionStrings.Add(splitline[0], splitline[1]);
                }
            }
            if (connectionStrings.ContainsKey(db))
            {
                _connectionString = connectionStrings[db];
            }
            else
                _connectionString = connectionStrings["Production"];
            Database.EnsureCreated();
        }
        //public DbSet<Limousine> Limousines { get; set; }
        //public DbSet<Klant> Klanten { get; set; }
        //public DbSet<Reservatie> Reservaties { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString != null)
                optionsBuilder.UseSqlServer(_connectionString);
            else throw new Exception("Missing ConnectionString in ReservatieContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klant>().HasKey(t => t.KlantNummer);
            modelBuilder.Entity<Klant>().Property(a => a.KlantNummer).ValueGeneratedNever(); //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }


        public void AddKlant(Klant klant)
        {
            Klanten.Add(klant);
            SaveChanges();
        }
        public void AddLimousine(Limousine limousine)
        {
            Limousines.Add(limousine);
            SaveChanges();
        }
        public void AddReservatie(Reservatie reservatie)
        {
            Reservaties.Add(reservatie);
            SaveChanges();
        }
        public bool BestaatKlantNummer(int klantNummer)
        {
            return Klanten.Any(x => x.KlantNummer == klantNummer);
        }
        public int GetAantalKlanten()
        {
            return Klanten.Count();
        }
        public int GetAantalLimousines()
        {
            return Limousines.Count();
        }
        public List<Limousine> GetLimousinesWithReservaties()
        {
            return Limousines.Include(l=>l.Reservaties).ToList();
        }
        public List<Limousine> GetBeschikbareLimousines(DateTime start, DateTime eind)
        {
            List < Limousine > result = new List<Limousine>();
            foreach(Limousine limo in Limousines)
            {
                bool beschikbaar = false;

                limo.Reservaties.OrderBy(x => x.StartMoment);
                foreach(Reservatie res in limo.Reservaties)
                {
                    //beginuur nieuwe reservatie mag niet vallen tussen: beginuur reservatie en 6u na einduur reservatie
                    if (!(start > res.StartMoment && start < res.StartMoment.AddHours(res.AantalUur + 6)))
                    {
                        if (eind < res.StartMoment.AddHours(-6))
                            beschikbaar = true;
                        else beschikbaar = false;
                    }
                    else beschikbaar = false;
                }
                if (beschikbaar)
                    result.Add(limo);
            }
            return result;
        }
        public int GetNewKlantNummer()
        {
            int HoogsteGetal = Klanten.OrderByDescending(x => x.KlantNummer).First().KlantNummer;
            return HoogsteGetal++;
        }
        public int GetNewReserveringsNummer()
        {
            int huidigReserveringsNummer = Reservaties.OrderByDescending(x => x.ReserveringsNummer).First().ReserveringsNummer;
            return huidigReserveringsNummer++;
        }
        public List<Klant> FindKlantVoorBtwNummer(string btwNummer)
        {
            return Klanten.Where(k => k.BtwNummer == btwNummer).ToList();
        }
        public List<Klant> FindKlantVoorNaam(string naam)
        {
            return Klanten.Where(k => k.Naam == naam).ToList();
        }
        public List<Reservatie> FindReservatieVoorKlantNaam(string klantNaam)
        {
            return Reservaties.Where(r => r.Klant.Naam.Contains(klantNaam)).ToList();
        }
        public List<Reservatie> FindReservatieVoorKlantNummer(int klantNummer)
        {
            return Reservaties.Where(r => r.Klant.KlantNummer == klantNummer).ToList();
        }
        public List<Reservatie> FindReservatieVoorDatum(DateTime datum)
        {
            return Reservaties.Where(r => r.StartMoment.Date == datum.Date).ToList();
        }
        public List<Reservatie> FindReservatieVoorKlantNaamEnDatum(string klantNaam, DateTime datum)
        {
            return Reservaties.Where(r => r.StartMoment.Date == datum.Date && r.Klant.Naam.Contains(klantNaam)).ToList();
        }
        public List<Reservatie> FindReservatieVoorKlantNummerEnDatum(int klantNummer, DateTime datum)
        {
            return Reservaties.Where(r => r.StartMoment.Date == datum.Date && r.Klant.KlantNummer == klantNummer).ToList();
        }
        public List<Reservatie> GetReservatiesVoorKlant(Klant klant)
        {
            return Reservaties.Where(r => r.Klant.Equals(klant)).ToList();
        }
        public List<Reservatie> GetReservatiesVoorDatum(DateTime datum)
        {
            return Reservaties.Where(r => r.StartMoment.Date == datum).ToList();
        }
        public List<Reservatie> GetReservatiesVoorDatumEnKlant(Klant klant, DateTime datum)
        {
            return Reservaties.Where(r => r.Klant == klant && r.StartMoment.Date == datum).ToList();
        }
        public Klant FindKlantVoorKlantNummer(int klantNummer)
        {
            return Klanten.Find(klantNummer);
        }
        //public int GetNewReservatieNummer()
        //{
        //    var temp = Reservaties.OrderByDescending(r => r.ReserveringsNummer);
        //    return temp.First().ReserveringsNummer + 1;
        //}
        public int GetAantalReservatiesVoorKlantInJaar(Klant klant, int jaar)
        {
            return Reservaties.Where(r => r.Klant == klant && r.StartMoment.Year == jaar).Count();
        }

        public Limousine FindLimousineVoorId(int id)
        {
            return Limousines.Find(id);
        }
    }
}
