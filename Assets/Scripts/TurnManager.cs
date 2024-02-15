using Mirror;

public class TurnManager : NetworkBehaviour
{
    [Command(requiresAuthority = false)]
    public void NextTurn()
    {
        EventBus<OnStartTurn>.Publish(new OnStartTurn());
    }
}