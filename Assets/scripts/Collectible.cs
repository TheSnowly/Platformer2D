using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectible : MonoBehaviour
{

    [SerializeField] public CardManager CardManager;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {

            int C = UnityEngine.Random.Range(1, 5);
            if (C==1) {
                CardManager.Deck.Push("Run");
            } else if (C==2) {
                CardManager.Deck.Push("Double_Jump");
            } else if (C==3) {
                CardManager.Deck.Push("Ennemy_Slam");
            } else if (C==4) {
                CardManager.Deck.Push("Key");
            }

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
