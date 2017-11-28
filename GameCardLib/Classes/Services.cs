namespace GameCardLib
{
    public class CardService// : Service<Card>, ICardService
    {
        private UnitOfWork unitOfWork = null;

        public CardService(BJDBContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public CardService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddCard(Card card)
        {
            unitOfWork.Cards.Add(card);
            unitOfWork.Cards.Save();
        }
    }

    //public class DeckService : Service<Deck>, IDeckService
    //{
    //}

    //public class GameService : Service<Game>, IGameService
    //{
    //}

    //public class PlayerService : Service<Player>, IPlayerService
    //{
    //}
}