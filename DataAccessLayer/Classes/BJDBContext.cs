using System.Configuration;
using System.Data.Entity;
using GameCardLib;

namespace DAL
{
    public class BJDBContext : DbContext
    {
        public BJDBContext() : base(ConfigurationManager.ConnectionStrings["Default"].ConnectionString) { }
        /// <summary>
        /// Games DbSet
        /// </summary>
        public DbSet<Game> Games { get; set; }
        /// <summary>
        /// Players DbSet
        /// </summary>
        public DbSet<Player> Players { get; set; }
        /// <summary>
        /// Decks DbSet
        /// </summary>
        public DbSet<Deck> Decks { get; set; }
        /// <summary>
        /// Cards DbSet
        /// </summary>
        public DbSet<Card> Cards { get; set; }
    }
}