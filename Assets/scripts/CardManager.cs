using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    public Stack Deck = new Stack();
    [SerializeField] GameObject CardPrefab;

    int card_Distance;
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
        Deck.Push("Double_Jump");
        Deck.Push("Run");
        Deck.Push("Ennemy_Slam");
        Deck.Push("Double_Jump");
     
        //creating cards in the canvas and stocking them in their own Stack
        foreach (var item in Deck)
        {
            GameObject Card = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.name = "Card_" + n;
            n++;
        }

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
        while ((Card.transform.position != targetPosition) && (Card)) {
            Card.transform.position = Vector3.SmoothDamp(Card.transform.position, targetPosition, ref ref_velocity, 0.09f);          
            yield return null;
        }
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
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    /* Au lieu de mettre la ligne 8 en public
     Cr�er 2 m�thodes, une push et une pull
    
    public void pushCard(carte)
    {
        Deck.Push(carte);
    }

    public void pullCard(carte)
    {
        Deck.Pull(carte);
    }

    comme �a t'as aucune variable en public donc elles sont "prot�g�es"
    fin, jsp si tu vois ce que je veux dire
    */
}
