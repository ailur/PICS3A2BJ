namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Context of the Unit of Work
        /// </summary>
        private readonly BJDBContext _context;

        /// <summary>
        /// Cards repository
        /// </summary>
        public ICardRepository Cards { get; }
        /// <summary>
        /// Decks repository
        /// </summary>
        public IDeckRepository Decks { get; }
        /// <summary>
        /// Games repository
        /// </summary>
        public IGameRepository Games { get; }
        /// <summary>
        /// Players repository
        /// </summary>
        public IPlayerRepository Players { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">Context to use</param>
        public UnitOfWork(BJDBContext context)
        {
            _context = context;
            Cards = new CardRepository(_context);
            Decks = new DeckRepository(_context);
            Games = new GameRepository(_context);
            Players = new PlayerRepository(_context);
        }
        /// <summary>
        /// Saves the changes to the context
        /// </summary>
        /// <returns></returns>
        public int Complete()
        {
            return _context.SaveChanges();
        }
        /// <summary>
        /// Disposes the context
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}