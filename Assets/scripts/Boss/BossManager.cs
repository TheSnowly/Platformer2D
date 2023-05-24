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
    [SerializeField] GameObject Cardtransform;

    [SerializeField] GameObject BossBG;
    [SerializeField] GameObject BossBlack;
    [SerializeField] GameObject Health;

    bool Switch;
    bool boss;

    int DS;
    public int NbCardThrow;

    // Start is called before the first frame update
    void Start()
    {
        NbCardThrow = 0;
        boss = false;
        Switch = true;
        CharacterController = GameObject.Find("Player").GetComponent<CharacterController>();
        CardManager.Boss = true;
    }

    public void GiveCard()
    {
        DS = CardManager.shuffled_Deck.Count;

        //creating cards in the canvas and pushing them in a deck
        for (int i = CardManager.shuffled_Deck.Count - 1; i >= 0; i--)
        {
            CardManager.Deck.Push(CardManager.shuffled_Deck[i]);
            GameObject Card = GameObject.Instantiate(CardManager.CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.GetComponent<Image>().sprite = CardManager.card_BACK;
            Card.name = "Card_" + i;
            Card.GetComponent<CardManagerSingle>().CardNb = i;

        }

        boss = true;

        CardManager.shuffled_Deck.Clear();
        CardManager.PlaceCards();
        CharacterController.CanMove = false;
        HealthBar.SetLife();
    }

    // Update is called once per frame
    void Update()
    {

        if (DS==NbCardThrow && Switch == true && boss == true)
        {
            Switch = false;
            BossBG.SetActive(false);
            BossBlack.SetActive(false);
            Health.SetActive(false);
            GameObject.Find("Explosion").GetComponent<Animator>().SetTrigger("Explo");
            GameObject.Find("Gritta").GetComponent<Animator>().SetTrigger("Dieps");
        }

        if (CardManager.Deck.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                GameObject Card = GameObject.Instantiate(CardThrow, Cardtransform.transform.position, Quaternion.Euler(-680, -352, 512), GameObject.FindGameObjectWithTag("MainCamera").transform);
                CharacterController.CardManage();
                CardManager.PlaceCards();
            }
        }
    }
}
