using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCards : MonoBehaviour
{

    public CardManager CardManager;

    // Start is called before the first frame update
    void Start()
    {       
        CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Double_Jump");
        CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Double_Jump");
        CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Ennemy_Slam");
        CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Run");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
