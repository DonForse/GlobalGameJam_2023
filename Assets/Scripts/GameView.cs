using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private Image startTurnBackground;


    [Header("Card Spawn Finish Animation")] [SerializeField]
    private Sprite[] Sprites;

    [SerializeField] private PhyisicalCardView cardPrefab;
    [SerializeField] private Transform spawnPosition;
    private static readonly int Turn = Animator.StringToHash("show_turn");

    public void ShowTurn(PlayerEnum playerEnum)
    {
        startTurnBackground.color = playerEnum == PlayerEnum.Player ? Color.blue : Color.red;
        turnText.text = playerEnum == PlayerEnum.Player ? $"TU TURNO!" : "TURNO DEL OPONENTE!";
        animator.SetTrigger(Turn);
    }

    public void WinGame(PlayerEnum won)
    {
        StartCoroutine(nameof(SpawnCards));
    }

    public IEnumerator SpawnCards()
    {
        var length = Sprites.Length;
        for (int i = 0; i < 20000; i++)
        {
            var spawned = Instantiate(this.cardPrefab, spawnPosition);
            var x = Random.Range(-0.5f, 0.5f);
            var y = Random.Range(-0.5f, 0.5f);
            var z = Random.Range(-0.5f, 0.5f);
            spawned.transform.localPosition += new Vector3(x, y, z);
            spawned.transform.localRotation = new Quaternion(Random.Range(0, 150), Random.Range(0, 150),
                Random.Range(0, 150), Random.Range(0, 150));

            spawned.SetSprite(Sprites[Random.Range(0,length)]);
            yield return new WaitForEndOfFrame();
        }
    }
}