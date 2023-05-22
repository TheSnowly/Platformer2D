using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{

    [SerializeField] CardManager CardManager;
    [SerializeField] HealthBar HealthBar;
    CharacterController CharacterController;

    [SerializeField] GameObject CardThrow;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CardManager.shuffled_Deck.Count);
        CharacterController = GameObject.Find("Player").GetComponent<CharacterController>();
        CardManager.Boss = true;
    }

    public void GiveCard()
    {
        Debug.Log("là");
        Debug.Log(CardManager.shuffled_Deck.Count);

        //creating cards in the canvas and pushing them in a deck
        for (int i = CardManager.shuffled_Deck.Count - 1; i >= 0; i--)
        {
            CardManager.Deck.Push(CardManager.shuffled_Deck[i]);
            GameObject Card = GameObject.Instantiate(CardManager.CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.GetComponent<Image>().sprite = CardManager.card_BACK;
            Card.name = "Card_" + i;
            Card.GetComponent<CardManagerSingle>().CardNb = i;

        }

        CardManager.shuffled_Deck.Clear();
        CardManager.PlaceCards();
        CharacterController.CanMove = false;
        HealthBar.SetLife();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject Card = GameObject.Instantiate(CardThrow, new Vector3(-70, -6.5f, 10), Quaternion.Euler(80, -352, 511), GameObject.FindGameObjectWithTag("MainCamera").transform);
            CharacterController.CardManage();
            CardManager.PlaceCards();
        }
    }
}
