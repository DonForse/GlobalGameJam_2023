public class BotService
{
    private readonly TurnService _turnService;

    public BotService(TurnService turnService)
    {
        _turnService = turnService;
    }
}