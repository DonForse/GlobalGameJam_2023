using UnityEngine.Events;

public interface IPlayCardStrategy
{
    bool Is(Card card);
    bool CanPlay(Card card, Player player);
    void Execute(Card card,Player player, GenerationRow row);
    UnityEvent CardPlayed { get; }
}