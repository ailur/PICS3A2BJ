using System.Configuration;
using System.Data.Entity;
using GameCardLib;

namespace DAL
{
    public class BJDBContext : DbContext
    {
        public BJDBContext() : base(ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString) { }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}