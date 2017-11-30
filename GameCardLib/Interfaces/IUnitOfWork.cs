using System;

namespace GameCardLib
{
    public interface IUnitOfWork : IDisposable
    {
        ICardRepository Cards { get; }
        IDeckRepository Decks { get; }
        IGameRepository Games { get; }
        IPlayerRepository Players { get; }
        int Complete();
    }

}