using DataLayer;
using DomainLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservatieTests
{
    class TestDataBaseHandler : ReservatieDatabaseHandler
    {
        public TestDataBaseHandler(bool keepExistingDB = false) : base("Test")
        {
            if (keepExistingDB)
            {
                Database.EnsureCreated();
            }
            else
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
    }
}
