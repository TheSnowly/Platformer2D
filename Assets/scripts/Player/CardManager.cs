using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public Stack<string> NewCards = new Stack<string>();

    static public Stack<string> Deck = new Stack<string>();
    static public List<string> shuffled_Deck = new List<string>();
    public GameObject[] Game_Cards;
    [SerializeField] GameObject CardPrefab;

    [SerializeField] public Sprite card_RUN;
    [SerializeField] public Sprite card_SLAM;
    [SerializeField] public Sprite card_JUMP;
    [SerializeField] public Sprite card_KEY;

    int deck_Size;
    float duration = 1;
    float prev_Card_pos_x;
    Vector3 ref_velocity;

    // Start is called before the first frame update
    void Start()
    {
        ref_velocity = Vector3.zero;

        if (Deck.Count > 0) {
            int DS = Deck.Count;
            for (int i = 0; i < DS; i++) {
                shuffled_Deck.Insert(Random.Range(0, shuffled_Deck.Count), Deck.Peek());
                Deck.Pop();
            }
        }

        Game_Cards = new GameObject[shuffled_Deck.Count];
     
        //creating cards in the canvas and pushing them in a deck
        for (int i = 0; i < shuffled_Deck.Count; i++) {
            Deck.Push(shuffled_Deck[i]);
            Game_Cards[i] = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Game_Cards[i].name = "Card_" + i;

        }


        shuffled_Deck.Clear();
        PlaceCards();

    }

    public void PlaceCards()
    {
        deck_Size = Deck.Count;
        int card_Distance;

        if (deck_Size >= 1) {
            card_Distance = 200 / deck_Size;
        } else {
            card_Distance = 200;
        }
        float prev_Card_pos_x = 50f;
        
        for(int i = 0; i < deck_Size - 1; i++) {
            prev_Card_pos_x = prev_Card_pos_x + card_Distance;
            StartCoroutine(MoveCards(new Vector3(prev_Card_pos_x + card_Distance, 100, 0), Game_Cards[i].gameObject));
        }
        StartCoroutine(MoveCardsSmooth(new Vector3(630, 100, 0), Game_Cards[Game_Cards.Length-1].gameObject));
    }

    IEnumerator MoveCardsSmooth(Vector3 targetPosition, GameObject Card) {

        float target = targetPosition.x - 0.001f;
        
        while (Card != null) {
            if ((Card.transform.position.x != target) && (!(Card.transform.rotation.eulerAngles.y > -1 && Card.transform.rotation.eulerAngles.y < 1))) {
                Card.transform.rotation = Quaternion.Lerp(Card.transform.rotation, Quaternion.Euler(0,0,0), 0.03f);
                Card.transform.position = Vector3.SmoothDamp(Card.transform.position, targetPosition, ref ref_velocity, 0.09f);
                if ((Card.transform.rotation.eulerAngles.y > 259) && (Card.transform.rotation.eulerAngles.y < 261)) {
                    if (Deck.Peek() == "Double_Jump") {
                        Card.GetComponent<Image>().sprite = card_JUMP;
                    } else if (Deck.Peek() == "Ennemy_Slam") {
                        Card.GetComponent<Image>().sprite = card_SLAM;
                    } else if (Deck.Peek() == "Run") {
                        Card.GetComponent<Image>().sprite = card_RUN;
                    } else if (Deck.Peek() == "Key") {
                        Card.GetComponent<Image>().sprite = card_KEY;
                    }
                }
            } else {
                break;
            }
            yield return null;
        }
        if (Card != null) {
            Card.transform.position = targetPosition;
            Card.transform.rotation = Quaternion.Euler(0,0,0);
        }   
    }

    IEnumerator MoveCards(Vector3 targetPosition, GameObject Card) {
        float timeElapsed = 0;
        Vector3 startPos = Card.transform.position;
        
        while (Card != null) {
            if (timeElapsed < duration) {
                Card.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed/duration * 5);
                timeElapsed += Time.deltaTime;
            } else {
                break;
            }
            yield return null;
        }
        if (Card != null) {
            transform.position = targetPosition;
        } else {
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
