using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private Image startTurnBackground; 
    private static readonly int Turn = Animator.StringToHash("show_turn");

    public void ShowTurn(PlayerEnum playerEnum)
    {
        startTurnBackground.color = playerEnum == PlayerEnum.Player ? Color.blue : Color.red;
        turnText.text = playerEnum == PlayerEnum.Player ? $"TU TURNO!" : "TURNO DEL OPONENTE!";
        animator.SetTrigger(Turn);
    }
}