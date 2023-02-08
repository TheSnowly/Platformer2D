using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    public Stack Deck = new Stack();

    public Stack DeckCanvas = new Stack();

    [SerializeField] GameObject CardPrefab;

    int n;
    float cardX = 433f;

    // Start is called before the first frame update
    void Start()
    {
        n = 1;

        Deck.Push("Double_Jump");
        Deck.Push("Run");
        Deck.Push("Ennemy_Slam");
        Deck.Push("Double_Jump");
     

        foreach (var item in Deck)
        {
            GameObject Card = GameObject.Instantiate(CardPrefab, new Vector3(cardX, 95f, 0f), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.name = "Card_" + n;
            DeckCanvas.Push(Card);
            cardX += 180f;
            Debug.Log(DeckCanvas.Count);
            n++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    /* Au lieu de mettre la ligne 8 en public
     Créer 2 méthodes, une push et une pull
    
    public void pushCard(carte)
    {
        Deck.Push(carte);
    }

    public void pullCard(carte)
    {
        Deck.Pull(carte);
    }

    comme ça t'as aucune variable en public donc elles sont "protégées"
    fin, jsp si tu vois ce que je veux dire
    */
}
