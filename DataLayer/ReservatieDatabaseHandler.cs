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
            modelBuilder.Entity<Klant>().Property(a => a.KlantNummer).ValueGeneratedNever();

        }


        public void VoegKlantToe(Klant klant)
        {
            Klanten.Add(klant);
            SaveChanges();
        }
        public void VoegLimousineToe(Limousine limousine)
        {
            Limousines.Add(limousine);
            SaveChanges();
        }
        public void VoegReservatieToe(Reservatie reservatie)
        {
            Reservaties.Add(reservatie);
            SaveChanges();
        }
        public int GeefAantalKlanten()
        {
            return Klanten.Count();
        }
        public int GeefAantalLimousines()
        {
            return Limousines.Count();
        }
        public List<Limousine> GeefLimousinesMetReservatie()
        {
            return Limousines.Include(l=>l.Reservaties).ToList();
        }
        public int GeefNieuwKlantNummer()
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
        public Klant VindKlantVoorBtwNummer(string btwNummer)
        {
            var find = Klanten.Where(k => k.BtwNummer == btwNummer);
            if (find != null && find.Count() != 0)
                return find.First();
            else return null;
        }
        public List<Klant> VindKlantVoorNaam(string naam)
        {
            return Klanten.Include(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(k => k.Naam.Contains(naam)).ToList();
        }
        public List<Reservatie> VindReservatiesVoorKlantNaam(string klantNaam)
        {
            return Reservaties.Include(r=>r.Limousine).Include(r=>r.Klant).ThenInclude(k=>k.Categorie).ThenInclude(c=>c.StaffelKorting).Where(r => r.Klant.Naam.Contains(klantNaam)).ToList();
        }
        public List<Reservatie> VindReservatiesVoorKlantNummer(int klantNummer)
        {
            return Reservaties.Include(r => r.Limousine).Include(r => r.Klant).ThenInclude(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(r => r.Klant.KlantNummer == klantNummer).ToList();
        }
        public List<Reservatie> VindReservatiesVoorDatum(DateTime datum)
        {
            return Reservaties.Include(r => r.Limousine).Include(r => r.Klant).ThenInclude(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(r => r.StartMoment.Date == datum.Date).ToList();
        }
        public List<Reservatie> VindReservatiesVoorKlantNaamEnDatum(string klantNaam, DateTime datum)
        {
            return Reservaties.Include(r => r.Limousine).Include(r => r.Klant).ThenInclude(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(r => r.StartMoment.Date == datum.Date && r.Klant.Naam.Contains(klantNaam)).ToList();
        }
        public List<Reservatie> VindReservatiesVoorKlantNummerEnDatum(int klantNummer, DateTime datum)
        {
            return Reservaties.Include(r => r.Limousine).Include(r => r.Klant).ThenInclude(k => k.Categorie).ThenInclude(c => c.StaffelKorting).Where(r => r.StartMoment.Date == datum.Date && r.Klant.KlantNummer == klantNummer).ToList();
        }
        public Klant VindVolledigeKlantVoorKlantNummer(int klantNummer)
        {
            return Klanten.Include(k=>k.Categorie).ThenInclude(c=>c.StaffelKorting).SingleOrDefault(k=>k.KlantNummer==klantNummer);
        }
        public int GeefAantalReservatiesVoorKlantInJaar(Klant klant, int jaar)
        {
            return Reservaties.Where(r => r.Klant == klant && r.StartMoment.Year == jaar).Count();
        }

        public Limousine VindLimousineVoorId(int id)
        {
            return Limousines.Find(id);
        }

        public DomainLibrary.KlantenCategorie VindKlantenCategorieVoorNaam(string klantenCategorie)
        {
            return KlantenCategorieen.Find(klantenCategorie);
        }

        public StaffelKorting VindStaffelKortingVoorNaam(string StaffelKortingNaam)
        {
            var find = StaffelKortingen.Where(r => r.Naam == StaffelKortingNaam);
            if (find != null && find.Count()!=0)
                return find.First();
            else return null;
        }

        public void VoegStaffelKortingToe(StaffelKorting staffelKorting)
        {
            StaffelKortingen.Add(staffelKorting);
            SaveChanges();
        }

        public void VoegKlantenCategorieToe(KlantenCategorie categorie)
        {
            KlantenCategorieen.Add(categorie);
        }

        public Reservatie VindReservatieVoorReservatieNummer(int reservatieNummer)
        {
            return Reservaties.Find(reservatieNummer);
        }
    }
}
