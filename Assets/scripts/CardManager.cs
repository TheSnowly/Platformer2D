using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public Stack<string> Deck = new Stack<string>();
    public List<string> shuffled_Deck = new List<string>();
    [SerializeField] GameObject CardPrefab;

    [SerializeField] Sprite card_RUN;
    [SerializeField] Sprite card_SLAM;
    [SerializeField] Sprite card_JUMP;

    int deck_Size;
    float duration = 1;
    float prev_Card_pos_x;
    Vector3 ref_velocity;

    //card name var
    int n;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 ref_velocity = Vector3.zero;
        n = 1;

        //creating cards
        shuffled_Deck.Insert(Random.Range(0, shuffled_Deck.Count), "Double_Jump");
        shuffled_Deck.Insert(Random.Range(0, shuffled_Deck.Count), "Ennemy_Slam");
        shuffled_Deck.Insert(Random.Range(0, shuffled_Deck.Count), "Double_Jump");
        shuffled_Deck.Insert(Random.Range(0, shuffled_Deck.Count), "Ennemy_Slam");


     
        //creating cards in the canvas and pushing them in a deck
        foreach (var item in shuffled_Deck)
        {
            Deck.Push(item);
            GameObject Card = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.name = "Card_" + n;
            n++;
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
        
        for(int i = 1; i <= deck_Size - 1; i++) {  
            prev_Card_pos_x = prev_Card_pos_x + card_Distance;
            StartCoroutine(MoveCards(new Vector3(prev_Card_pos_x + card_Distance, 100, 0), GameObject.Find("Card_" + i)));    
        }
        
        StartCoroutine(MoveCardsSmooth(new Vector3(630, 100, 0), GameObject.Find("Card_" + deck_Size)));
    }

    IEnumerator MoveCardsSmooth(Vector3 targetPosition, GameObject Card) {

        float target = targetPosition.x - 0.001f;

        while ((Card.transform.position.x != target) && (Card) && (Card.transform.rotation != Quaternion.Euler(0,0,0))) {
            Card.transform.position = Vector3.SmoothDamp(Card.transform.position, targetPosition, ref ref_velocity, 0.09f); 
            Card.transform.rotation = Quaternion.Lerp(Card.transform.rotation, Quaternion.Euler(0,0,0), 0.018f);
            if ((Card.transform.rotation.eulerAngles.y > 259) && (Card.transform.rotation.eulerAngles.y < 261)) {
                if (Deck.Peek() == "Double_Jump") {
                    Card.GetComponent<Image>().sprite = card_JUMP;
                } else if (Deck.Peek() == "Ennemy_Slam") {
                    Card.GetComponent<Image>().sprite = card_SLAM;
                } else if (Deck.Peek() == "Run") {
                    Card.GetComponent<Image>().sprite = card_RUN;
                }
            }       
            yield return null;
        }
        Card.transform.position = targetPosition;
        Card.transform.rotation = Quaternion.Euler(0,0,0);
        yield return null;
    }

    IEnumerator MoveCards(Vector3 targetPosition, GameObject Card) {
        float timeElapsed = 0;
        Vector3 startPos = Card.transform.position;
        
        while (timeElapsed < duration) {
            Card.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed/duration * 5);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;  
        yield return null;     
    }

    // Update is called once per frame
    void Update()
    {
    }
}
