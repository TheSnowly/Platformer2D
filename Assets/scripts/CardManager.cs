using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    Stack Deck = new Stack();

    // Start is called before the first frame update
    void Start()
    {
        Deck.Push("DJ");
        Deck.Push("TAMEEZER");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
