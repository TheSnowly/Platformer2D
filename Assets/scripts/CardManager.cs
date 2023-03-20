using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    static public Stack<string> Deck = new Stack<string>();
    static public List<string> shuffled_Deck = new List<string>();
    [SerializeField] GameObject CardPrefab;

    [SerializeField] Sprite card_RUN;
    [SerializeField] Sprite card_SLAM;
    [SerializeField] Sprite card_JUMP;

    int deck_Size;
    float duration = 1;
    float prev_Card_pos_x;
    Vector3 ref_velocity;

    // Start is called before the first frame update
    void Start()
    {

        ref_velocity = Vector3.zero;

        //creating cards
        shuffled_Deck.Insert(Random.Range(0, shuffled_Deck.Count), "Double_Jump");
        shuffled_Deck.Insert(Random.Range(0, shuffled_Deck.Count), "Ennemy_Slam");


        if (Deck.Count > 0) {
            for (int i = 1; i <= Deck.Count; i++) {
                shuffled_Deck.Insert(Random.Range(0, shuffled_Deck.Count), Deck.Peek());
                Deck.Pop();
            }
        }
     
        //creating cards in the canvas and pushing them in a deck
        for (int i = 1; i <= shuffled_Deck.Count; i++) {
            Deck.Push(shuffled_Deck[i - 1]);
            GameObject Card = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.name = "Card_" + i;
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
        //Debug.Log(Card.name);

        /*
        Vector3 start_Pos = Card.transform.position;

        float elapsedTime = 0f;

        while (Card != null) {
            if(elapsedTime < 0.5f) {
                float lerpFactor = Mathf.SmoothStep(0f, 1f, elapsedTime / 0.5f);

                elapsedTime += Time.deltaTime;
                
                Card.transform.position = Vector3.Lerp(start_Pos, targetPosition, lerpFactor);
                //transform.rotation = Quaternion.Slerp(startingRotation, _targetTransform.rotation, lerpFactor);

            } else {
                Debug.Log("lezgongue");
                break;
            }
            yield return null;
        }

        if (Card != null) {
            Card.transform.position = targetPosition;
        } else {
            yield return null;
        }
        */

        
        while (Card != null) {
            if ((Card.transform.position.x != target) || (!(Card.transform.rotation.eulerAngles.y > -1 && Card.transform.rotation.eulerAngles.y < 1))) {
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
