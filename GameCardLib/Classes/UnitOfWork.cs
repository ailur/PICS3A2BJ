namespace GameCardLib
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BJDBContext _context;

        public ICardRepository Cards { get; }
        public IDeckRepository Decks { get; }
        public IGameRepository Games { get; }
        public IPlayerRepository Players { get; }

        public UnitOfWork(BJDBContext context)
        {
            _context = context;
            Cards = new CardRepository(_context);
            Decks = new DeckRepository(_context);
            Games = new GameRepository(_context);
            Players = new PlayerRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}