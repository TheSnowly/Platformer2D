using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutoBubble : MonoBehaviour
{

    [SerializeField] CardManager CardManager;
    [SerializeField] GameObject Bubble;
    [SerializeField] GameObject Mouse;
    CharacterController CharacterController;

    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CardManager.Deck.Count > 0) {
            Bubble.GetComponent<TextMeshProUGUI>().text = "Great! You got a card, you can either use it or throw it, for now just use LEFT CLICK to throw it";
            Mouse.SetActive(true);
        } else {
            Bubble.GetComponent<TextMeshProUGUI>().text = "Go on the card to take it";
            Mouse.SetActive(false);
        }
    }
}
