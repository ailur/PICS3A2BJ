namespace UtilitiesLib
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(BJDBContext context) : base(context) { }

        public BJDBContext BJDBContext
        {
            get { return Context as BJDBContext; }
        }
    }

    public class DeckRepository : Repository<Deck>, IDeckRepository
    {
        public DeckRepository(BJDBContext context) : base(context) { }

        public BJDBContext BJDBContext
        {
            get { return Context as BJDBContext; }
        }
    }

    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(BJDBContext context) : base(context) { }

        public BJDBContext BJDBContext
        {
            get { return Context as BJDBContext; }
        }
    }

    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(BJDBContext context) : base(context) { }

        public BJDBContext BJDBContext
        {
            get { return Context as BJDBContext; }
        }
    }
}