using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    public Stack Deck = new Stack();
    [SerializeField] GameObject CardPrefab;

    int deck_Size;

    //card name var
    int n;

    // Start is called before the first frame update
    void Start()
    {
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
            //cardX += 180f;
            n++;
        }

        PlaceCards();
    }

    private void PlaceCards()
    {
        deck_Size = Deck.Count;
        Vector3 ref_velocity = Vector3.zero;

        int card_Distance = 200 / deck_Size;
        float prev_Card_pos_x = 50f;

        for(int i = 1; i <= deck_Size - 1; i++) {
            
            while (GameObject.Find("Card_" + i).transform.position != new Vector3(prev_Card_pos_x + card_Distance, 100, 0)) {
                Debug.Log("ça bouge");
                GameObject.Find("Card_" + i).transform.position = Vector3.SmoothDamp(GameObject.Find("Card_" + i).transform.position, new Vector3(prev_Card_pos_x + card_Distance, 100, 0) , ref ref_velocity, 0.01f);
            }
            
            //GameObject.Find("Card_" + i).transform.position = new Vector3(prev_Card_pos_x + card_Distance, 100, 0);
            prev_Card_pos_x = GameObject.Find("Card_" + i).transform.position.x;
        }

        GameObject.Find("Card_" + deck_Size).transform.position = new Vector3(630, 100, 0);
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
