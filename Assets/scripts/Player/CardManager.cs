using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public bool Boss = false;

    public Stack<string> NewCards = new Stack<string>();

    static public Stack<string> Deck = new Stack<string>();
    static public List<string> shuffled_Deck = new List<string>();
    public GameObject CardPrefab;

    [SerializeField] public Sprite card_RUN;
    [SerializeField] public Sprite card_SLAM;
    [SerializeField] public Sprite card_JUMP;
    [SerializeField] public Sprite card_KEY;
    [SerializeField] public Sprite card_BACK;

    int deck_Size;
    float prev_Card_pos_x;
    Vector3 ref_velocity;

    void GiveCard()
    {
        ref_velocity = Vector3.zero;

        if (Boss == false)
        {
            //creating cards in the canvas and pushing them in a deck
            for (int i = shuffled_Deck.Count - 1; i >= 0; i--) {
                Deck.Push(shuffled_Deck[i]);
                GameObject Card = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
                Card.GetComponent<Image>().sprite = card_BACK;
                Card.name = "Card_" + i;
                Card.GetComponent<CardManagerSingle>().CardNb = i;
            }

            shuffled_Deck.Clear();
            PlaceCards();
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        GiveCard();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait(0.5f));
    }

    public void PlaceCards()
    {
        for(int i = 0; i <= Deck.Count - 1; i++) {
            GameObject.Find("Card_" + i).GetComponent<CardManagerSingle>().Place();
        }
    }
}
