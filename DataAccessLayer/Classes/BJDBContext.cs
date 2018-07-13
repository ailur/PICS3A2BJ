using System.Data.Entity;
using GameCardLib;

namespace DAL
{
    public class BJDBContext : DbContext
    {
        public BJDBContext() : base("LocalDB") { }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}