using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTuto : MonoBehaviour
{
    
    [SerializeField] public CardManager CardManager;
    [SerializeField] string CardTypeT;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {
            
            CardManager.Deck.Push(CardTypeT);

            NewCardCollectible();
            Destroy(this.gameObject);
        }
    }

    void NewCardCollectible() {

        for (int i = CardManager.Deck.Count - 2; i >= 0; i--) {
            GameObject.Find("Card_" + i).GetComponent<CardManagerSingle>().CardNb = GameObject.Find("Card_" + i).GetComponent<CardManagerSingle>().CardNb + 1;
            GameObject.Find("Card_" + i).name = "Card_" + (i + 1);
        }

        GameObject Card = GameObject.Instantiate(CardManager.CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
        Card.GetComponent<CardManagerSingle>().CardNb = 0;
        Card.name = "Card_" + 0;
        CardManager.PlaceCards();
    }
}
