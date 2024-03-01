namespace EventBus
{
    public class NextTurnButtonPressed : Event
    {
        public readonly CanTakeTurn CanTakeTurn;

        public NextTurnButtonPressed(CanTakeTurn canTakeTurn)
        {
            CanTakeTurn = canTakeTurn;
        }
    }
}