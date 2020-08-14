using DomainLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class ReservatieContext : DbContext
    {
        public DbSet<Limousine> Limousines { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
    }
}
