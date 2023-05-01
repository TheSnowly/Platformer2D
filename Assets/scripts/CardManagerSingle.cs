using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManagerSingle : MonoBehaviour
{

    public CardManager CardManager;

    public int CardNb;

    float duration = 1;

    public void Place() {
        
        int DS = CardManager.Deck.Count;

        int card_Distance;

        if (DS >= 1) {
            card_Distance = 200 / DS;
        } else {
            card_Distance = 200;
        }

        if (CardNb == (DS - 1)) {
            StartCoroutine(MoveFirstCard(new Vector3(630, 100, 0), this.gameObject));
        } else {
            StartCoroutine(MoveCards(new Vector3(50 + (card_Distance * CardNb), 100, 0), this.gameObject));
        }
    } 

    IEnumerator MoveFirstCard(Vector3 targetPosition, GameObject Card) {

        float target = targetPosition.x - 0.001f;
        
        while (Card != null) {
            if ((Card.transform.position.x != target) && (!(Card.transform.rotation.eulerAngles.y > -5 && Card.transform.rotation.eulerAngles.y < 5))) {
                
                Card.transform.rotation = Quaternion.Lerp(Card.transform.rotation, Quaternion.Euler(0,0,0), 10f * Time.deltaTime);

                Card.transform.position = Vector3.Lerp(Card.transform.position, targetPosition, 10f * Time.deltaTime);

                if ((Card.transform.rotation.eulerAngles.y > 250) && (Card.transform.rotation.eulerAngles.y < 270)) {
                    if (CardManager.Deck.Peek() == "Double_Jump") {
                        Card.GetComponent<Image>().sprite = CardManager.card_JUMP;
                    } else if (CardManager.Deck.Peek() == "Ennemy_Slam") {
                        Card.GetComponent<Image>().sprite = CardManager.card_SLAM;
                    } else if (CardManager.Deck.Peek() == "Run") {
                        Card.GetComponent<Image>().sprite = CardManager.card_RUN;
                    } else if (CardManager.Deck.Peek() == "Key") {
                        Card.GetComponent<Image>().sprite = CardManager.card_KEY;
                    }
                }
            } else {
                break;
            }
            yield return null;
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
}
