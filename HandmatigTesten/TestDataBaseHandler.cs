using DataLayer;
using DomainLibrary.Interfaces;

namespace HandmatigTesten
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