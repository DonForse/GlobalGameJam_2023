using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotService : MonoBehaviour
{
    [SerializeField]private CardRepositoryScriptableObject cardsRepo;
    private  TurnService _turnService;
    private  PlayCard _playCard;
    private  Player _botPlayer;

    public BotService With(TurnService turnService, PlayCard playCard, Player npc)
    {
        _turnService = turnService;
        _playCard = playCard;
        _botPlayer = npc;   
        _turnService.OnTurnChange.AddListener(StartPlay);

        return this;
    }

    private void StartPlay(PlayerEnum playerEnum)
    {
        if (playerEnum == PlayerEnum.Player) return;
        StartCoroutine(nameof(Play));

    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(4f);
        int cardPlayedCount = 0;

        foreach (var card in _botPlayer.PlayerHand.Cards.ToList())
        {      
            yield return new WaitForSeconds(1f);

            var cardDomain = cardsRepo.GetFromId(card.Name);
            if (_playCard.Execute(cardDomain, _botPlayer, (GenerationRow)Random.Range(0, 3)))
            {
                cardPlayedCount++;
                if (cardPlayedCount == 2)
                    break;
            }
        }
    }
}