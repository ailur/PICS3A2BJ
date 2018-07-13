using GameCardLib;

namespace DAL
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        /// <summary>
        /// CardRepository Default Constructor
        /// </summary>
        /// <param name="context">Context to initialize</param>
        public CardRepository(BJDBContext context) : base(context) { }

        /// <summary>
        /// Get Context
        /// </summary>
        public BJDBContext BJDBContext
        {
            get { return Context as BJDBContext; }
        }
    }

    public class DeckRepository : Repository<Deck>, IDeckRepository
    {
        /// <summary>
        /// DeckRepository Default Constructor
        /// </summary>
        /// <param name="context">Context to initialize</param>
        public DeckRepository(BJDBContext context) : base(context) { }

        /// <summary>
        /// Get Context
        /// </summary>
        public BJDBContext BJDBContext
        {
            get { return Context as BJDBContext; }
        }
    }

    public class GameRepository : Repository<Game>, IGameRepository
    {
        /// <summary>
        /// GameRepository Default Constructor
        /// </summary>
        /// <param name="context">Context to initialize</param>
        public GameRepository(BJDBContext context) : base(context) { }

        /// <summary>
        /// Get Context
        /// </summary>
        public BJDBContext BJDBContext
        {
            get { return Context as BJDBContext; }
        }
    }

    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        /// <summary>
        /// PlayerRepository Default Constructor
        /// </summary>
        /// <param name="context">Context to initialize</param>
        public PlayerRepository(BJDBContext context) : base(context) { }

        /// <summary>
        /// Get Context
        /// </summary>
        public BJDBContext BJDBContext
        {
            get { return Context as BJDBContext; }
        }
    }
}