using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceCardTuto : MonoBehaviour
{

    public int CardNB;
    string CardTypeTuto;
    public CardManager CardManager;
    public CharacterController CharacterController;

    [SerializeField] GameObject PouchCards;

    // Start is called before the first frame update
    void Start()
    {
        if (CardNB == 1) {
            this.GetComponent<Image>().sprite = CardManager.card_JUMP;
            CardTypeTuto = "Double_Jump";
        } else if (CardNB == 2) {
            this.GetComponent<Image>().sprite = CardManager.card_SLAM;
            CardTypeTuto = "Ennemy_Slam";
        } else if (CardNB == 3) {
            this.GetComponent<Image>().sprite = CardManager.card_RUN;
            CardTypeTuto = "Run";
        }
    }

    public void OnClickTuto() {

        for (int i = 1; i <= 3; i++) {
            Destroy(GameObject.Find("CardCh_" + i));
        }

        for (int i = 0; i <= 2; i++) {
            CardManager.Deck.Push(CardTypeTuto);
            GameObject Card = GameObject.Instantiate(CardManager.CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.name = "Card_" + i;
            Card.GetComponent<CardManagerSingle>().CardNb = i;
        }  
        CardManager.PlaceCards();
        CharacterController.CanMove = true;

        PouchCards.SetActive(false);
    }

}
