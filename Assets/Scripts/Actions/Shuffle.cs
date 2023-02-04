using System.Linq;
using UnityEngine;

public class Shuffle
{
    private Deck _deck;

    public Shuffle(Deck deck)
    {
        _deck = deck;
    }

    public void Execute()
    {
        var shuffledCards = _deck.Cards.OrderBy(x => Random.Range(0, 1000)).ToList();
        _deck.Cards.Clear();
        foreach (var card in shuffledCards)
            _deck.Cards.Push(card);
    }
}