using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectible : MonoBehaviour
{

    [SerializeField] public CardManager CardManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {

            int C = UnityEngine.Random.Range(1, 4);
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

        Array.Resize(ref CardManager.Game_Cards, CardManager.Game_Cards.Length + 1);

        for (int i = CardManager.Game_Cards.Length - 2; i >= 0; i--) {
            CardManager.Game_Cards[i] = CardManager.Game_Cards[i + 1];
            GameObject.Find("Card_" + i).name = "Card_" + (i + 1);
        }
        CardManager.Game_Cards[0] = GameObject.Instantiate(CardManager.CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
        CardManager.Game_Cards[0].name = "Card_" + 0;
        CardManager.PlaceCards();
    }
}
