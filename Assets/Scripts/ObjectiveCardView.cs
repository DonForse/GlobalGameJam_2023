using System;
using System.Collections.Generic;
using Actions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectiveCardView: MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject completed;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text ObjectiveText;

    public string CardId;
    private CanClaimTrophy _canClaimTrophy;
    private ClaimTrophy _claimTrophy;

    public void Init(CanClaimTrophy canClaimTrophy, ClaimTrophy claimTrophy)
    {
        _canClaimTrophy = canClaimTrophy;
        _claimTrophy = claimTrophy;
    }

    public void Setup(Sprite sprite, string name, int[] objectiveValues)
    {
        cardImage.sprite = sprite;
        // this.cardName.text = name;
        this.name = name;
        CardId = name;
        SetObjectives(objectiveValues);
    }

    private void SetObjectives(IReadOnlyList<int> objectivesValues)
    {
        if(HasRelative(objectivesValues[0]))
            AddFamiliar($"Grandparents: {objectivesValues[0]}\n");
        if(HasRelative(objectivesValues[1]))
            AddFamiliar($"Parents: {objectivesValues[1]}\n");
        if(HasRelative(objectivesValues[2]))
            AddFamiliar($"Children: {objectivesValues[1]}" );
    }

    private void AddFamiliar(string relative) => ObjectiveText.text += relative;

    private static bool HasRelative(int objectivesValue) => objectivesValue != 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_canClaimTrophy.Execute(CardId))
        {
            _claimTrophy.Execute(CardId);
            DeactivateCard();
        }
    }

    private void DeactivateCard()
    {
        cardImage.raycastTarget = false;
        ObjectiveText.raycastTarget = false;
        completed.SetActive(true);
    }
}