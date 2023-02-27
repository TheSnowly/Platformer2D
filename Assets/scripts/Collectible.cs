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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), "Run");
            Destroy(this.gameObject);
        }
    }
}
