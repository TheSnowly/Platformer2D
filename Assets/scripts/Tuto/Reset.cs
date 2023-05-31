using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    [SerializeField] CardManager CardManager;
    // Start is called before the first frame update
    void Start()
    {
        CardManager.Deck.Clear();
        CardManager.shuffled_Deck.Clear();

    }
}
