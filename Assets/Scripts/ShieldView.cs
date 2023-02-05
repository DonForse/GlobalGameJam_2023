using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShieldView : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void OnEnable()
    {
        throw new NotImplementedException();
    }

    public void OnShieldCalled(PlayCard playCard, Player player, HandView handView, Action<bool> callBack)
    {
        Show();
        yesButton.onClick.AddListener(() =>
        {
            Hide();
            if (playCard.Execute(GetAShieldCard(player), player, GenerationRow.Board));
                handView.RemoveCard(GetAShieldCard(player));

            callBack(true);
            yesButton.onClick.RemoveAllListeners();
        });
        noButton.onClick.AddListener(() =>
        {
            Hide();
            callBack(false);
            noButton.onClick.RemoveAllListeners();
        });
    }

    private static Card GetAShieldCard(Player player)
    {
        return player.PlayerHand.GetShieldCard();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}