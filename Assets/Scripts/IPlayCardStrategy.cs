public interface IPlayCardStrategy
{
    bool Is(Card card);
    void Execute(Card card,Player player, GenerationRow row);
}