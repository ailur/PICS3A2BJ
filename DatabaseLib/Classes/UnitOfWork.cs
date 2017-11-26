using GameCardLib.DatabaseLib.Repositories;

namespace GameCardLib.DatabaseLib.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BJDBContext _context;

        public ICardRepository Cards { get; private set; }
        public IDeckRepository Decks { get; private set; }
        public IGameRepository Games { get; private set; }
        public IPlayerRepository Players { get; private set; }

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