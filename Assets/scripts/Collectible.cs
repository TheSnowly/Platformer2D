using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Collectible : MonoBehaviour
{

    [SerializeField] public CardManager CardManager;
    [SerializeField] bool Switch;

    bool Key;

    void Start() {
        CheckKey();
        Key = false;
        Switch = true;
    }

    void OnTriggerEnter2D(Collider2D other) {


        if (Switch == true && other.gameObject.tag == "Player") {

            Switch = false;

            if (other.gameObject.tag == "Player") {

                int C;
                if (Key == false)
                {
                    C = UnityEngine.Random.Range(1, 5);
                } else
                {
                    C = UnityEngine.Random.Range(1, 4);
                }

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
    }

    void CheckKey()
    {
        if (CardManager.Deck.Count > 0)
        {
            for (int i = 0; i <= CardManager.Deck.Count -1; i++)
            {
                if (CardManager.Deck.Peek() == "Key")
                {
                    Key = true;
                } 
            }
        }
        if (CardManager.shuffled_Deck.Count > 0)
        {
            for (int i = 0; i <= CardManager.shuffled_Deck.Count - 1; i++)
            {
                if (CardManager.shuffled_Deck[i] == "Key")
                {
                    Key = true;
                }
            }
        }
    }

    void NewCardCollectible() {

        for (int i = CardManager.Deck.Count - 2; i >= 0; i--) {
            GameObject.Find("Card_" + i).GetComponent<CardManagerSingle>().CardNb = GameObject.Find("Card_" + i).GetComponent<CardManagerSingle>().CardNb + 1;
            GameObject.Find("Card_" + i).name = "Card_" + (i + 1);
        }

        GameObject Card = GameObject.Instantiate(CardManager.CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
        Card.GetComponent<Image>().sprite = CardManager.card_BACK;
        Card.GetComponent<CardManagerSingle>().CardNb = 0;
        Card.name = "Card_" + 0;
        CardManager.PlaceCards();
    }
}
