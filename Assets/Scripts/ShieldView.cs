using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShieldView : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    
    public void OnShieldCalled(PlayCard playCard, Player player, Action<bool> callBack)
    {
        Show();
        yesButton.onClick.AddListener(() =>
        {
            Hide();
            callBack(true);
            playCard.Execute(GetAShieldCard(player), player, GenerationRow.Board);
        });
        noButton.onClick.AddListener(() =>
        {
            Hide();
            callBack(false);
        });
    }

    private static Card GetAShieldCard(Player player)
    {
        return player.PlayerHand.GetShieldCard();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}