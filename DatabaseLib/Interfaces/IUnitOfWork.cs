﻿using System;
using GameCardLib.DatabaseLib.Repositories;

namespace GameCardLib.DatabaseLib.UnitOfWork
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