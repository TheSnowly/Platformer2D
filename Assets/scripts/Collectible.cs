using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public CardManager CardManager;

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

            int C = Random.Range(1, 4);
            if (C==1) {
                CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Run");
            } else if (C==2) {
                CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Ennemy_Slam");
            } else if (C==3) {
                CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Double_Jump");
            } else if (C==4) {
                CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Key");
            }
            Destroy(this.gameObject);
        }
    }
}
