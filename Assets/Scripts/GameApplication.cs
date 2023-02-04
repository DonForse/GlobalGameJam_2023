using UnityEngine;

public class GameApplication : MonoBehaviour
{

    [SerializeField] private CardRepositoryScriptableObject cardsRepo;
    void Start()
    {
        var deck = new Deck(cardsRepo);
        //deck.Initialize();
    }
}