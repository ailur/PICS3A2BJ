using System;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Cards repository
        /// </summary>
        ICardRepository Cards { get; }
        /// <summary>
        /// Decks repository
        /// </summary>
        IDeckRepository Decks { get; }
        /// <summary>
        /// Games repository
        /// </summary>
        IGameRepository Games { get; }
        /// <summary>
        /// Players repository
        /// </summary>
        IPlayerRepository Players { get; }
        /// <summary>
        /// Saves the changes to the context
        /// </summary>
        /// <returns></returns>
        int Complete();
    }

}