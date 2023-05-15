using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelManager : MonoBehaviour
{
    [SerializeField] GameObject CardPrefab;
    [SerializeField] GameObject EndLevel;

    public CardManager CardManager;
    public Timer Timer;
    public CardClick CardClick;

    bool Switch;

    static public int NbOfLevelPlayed;
    [SerializeField] GameObject transi;


    // Start is called before the first frame update
    void Start()
    {
        Switch = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Switch == false && other.tag == "Player") {

            transi.GetComponent<Animator>().SetTrigger("EndTransi2");
            NbOfLevelPlayed += 1;
            Debug.Log(NbOfLevelPlayed);
            Timer.StopTime = true;
            EndLevel.SetActive(true);
            other.GetComponent<CharacterController>().CanMove = false;
            if (NbOfLevelPlayed < 5) {
                NextLevelChoice();
            }
            Switch = true;
        }
    }

    void NextLevelChoice() {

        if (CardManager.Deck.Count > 0) {
            int DS = CardManager.Deck.Count;

            for (int i = 0; i < DS; i++) {
                Destroy(GameObject.Find("Card_" + i));
            }

            for (int i = 0; i < DS; i++) {
                CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), CardManager.Deck.Peek());
                CardManager.Deck.Pop();
            }
        }

        for (int i = 1; i <= 2; i++) {

            GameObject Card = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

            string Random_Card = "";

            while(Random_Card == "Key" || Random_Card == "") {
                Random_Card = CardManager.shuffled_Deck[Random.Range(0, CardManager.shuffled_Deck.Count)];
            }

            Card.GetComponent<CardClick>().CardType = Random_Card;

            if (Random_Card == "Run") {
                Card.GetComponent<Image>().sprite = CardManager.card_RUN;
            } else if (Random_Card == "Double_Jump") {
                Card.GetComponent<Image>().sprite = CardManager.card_JUMP;
            } else if (Random_Card == "Ennemy_Slam") {
                Card.GetComponent<Image>().sprite = CardManager.card_SLAM;
            }

            Card.transform.position = new Vector3(i * 100, 200, -10);
        }

    }

}
