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
    public class ReservatieDatabaseHandler : DbContext, iReservatieDatabaseHandler
    {
        public DbSet<Limousine> Limousines { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
        public DbSet<KlantenCategorie> KlantenCategorieen { get; set; }
        public DbSet<StaffelKorting> StaffelKortingen { get; set; }
        protected string _connectionString;
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
        }   //mag mogenlijks weg?
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
            int hoogsteGetal;
            try
            {
                hoogsteGetal = Klanten.OrderByDescending(x => x.KlantNummer).First().KlantNummer;
            }
            catch (InvalidOperationException)
            {
                hoogsteGetal = 0;
            }
            return ++hoogsteGetal;
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
            return Klanten.Include(k=>k.Categorie).Where(k => k.Naam.Contains(naam)).ToList();
        }
        public List<Reservatie> FindReservatieDetailsVoorKlantNaam(string klantNaam)
        {
            return Reservaties.Include(r=>r.Limousine).Include(r=>r.Klant).ThenInclude(k=>k.Categorie).ThenInclude(c=>c.StaffelKorting).Where(r => r.Klant.Naam.Contains(klantNaam)).ToList();
        }
        public List<Reservatie> FindReservatieDetailsVoorKlantNummer(int klantNummer)
        {
            return Reservaties.Include(r => r.Limousine).Include(r => r.Klant).ThenInclude(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(r => r.Klant.KlantNummer == klantNummer).ToList();
        }
        public List<Reservatie> FindReservatieDetailsVoorDatum(DateTime datum)
        {
            return Reservaties.Include(r => r.Limousine).Include(r => r.Klant).ThenInclude(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(r => r.StartMoment.Date == datum.Date).ToList();
        }
        public List<Reservatie> FindReservatieDetailsVoorKlantNaamEnDatum(string klantNaam, DateTime datum)
        {
            return Reservaties.Include(r => r.Limousine).Include(r => r.Klant).ThenInclude(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(r => r.StartMoment.Date == datum.Date && r.Klant.Naam.Contains(klantNaam)).ToList();
        }
        public List<Reservatie> FindReservatieDetailsVoorKlantNummerEnDatum(int klantNummer, DateTime datum)
        {
            return Reservaties.Include(r => r.Limousine).Include(r => r.Klant).ThenInclude(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(r => r.StartMoment.Date == datum.Date && r.Klant.KlantNummer == klantNummer).ToList();
        }
        public Klant FindVolledigeKlantVoorKlantNummer(int klantNummer)
        {
            return Klanten.Include(k=>k.Categorie).ThenInclude(c=>c.StaffelKorting).SingleOrDefault(k=>k.KlantNummer==klantNummer);
        }
        public int GetAantalReservatiesVoorKlantInJaar(Klant klant, int jaar)
        {
            return Reservaties.Where(r => r.Klant == klant && r.StartMoment.Year == jaar).Count();
        }

        public Limousine FindLimousineVoorId(int id)
        {
            return Limousines.Find(id);
        }

        public DomainLibrary.KlantenCategorie VindKlantenCategorieVoorNaam(string klantenCategorie)
        {
            return KlantenCategorieen.Find(klantenCategorie);
        }

        public StaffelKorting VindStaffelKortingVoorNaam(string naam)
        {
            var find = StaffelKortingen.Where(r => r.Naam == naam);
            if (find != null && find.Count()>0)
                return find.First();
            else return null;
        }

        public void VoegStaffelKortingToe(StaffelKorting staffelKorting)
        {
            StaffelKortingen.Add(staffelKorting);
        }

        public void VoegKlantenCategorieToe(KlantenCategorie categorie)
        {
            KlantenCategorieen.Add(categorie);
        }
    }
}
