using System.Collections;
using System.Linq;
using Actions;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotService : MonoBehaviour
{
    [SerializeField]private CardRepositoryScriptableObject cardsRepo;
    private  TurnService _turnService;
    private  PlayCard _playCard;
    private  Player _botPlayer;
    private CanClaimTrophy _canClaimTrophy;
    private ClaimTrophy _claimTrophy;

    public BotService With(TurnService turnService, PlayCard playCard, Player npc, CanClaimTrophy canClaimTrophy, ClaimTrophy claimTrophy)
    {
        _turnService = turnService;
        _playCard = playCard;
        _botPlayer = npc;
        _canClaimTrophy = canClaimTrophy;
        _turnService.OnTurnChange.AddListener(StartPlay);
        _claimTrophy = claimTrophy;

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
            foreach (var objective in ObjectiveService.Get().ToList())
            {
                if (_canClaimTrophy.Execute(objective.Name)){
                    _claimTrophy.Execute(objective.Name);
                }
            }

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