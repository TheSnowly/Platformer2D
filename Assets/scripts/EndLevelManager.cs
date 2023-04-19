using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelManager : MonoBehaviour
{
    [SerializeField] GameObject CardPrefab;

    public CardManager CardManager;
    bool Switch;

    // Start is called before the first frame update
    void Start()
    {
        Switch = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !Switch)
        {
            collision.gameObject.SetActive(false);
            NewCard();
            Debug.Log("je suis là");

            Switch = true;
        }
    }

    void NewCard()
    {

        int Deck_Size = CardManager.NewCards.Count;
        string[] CardUsed = new string[Deck_Size - 1];
        float[] CardUsedPlace = new float[Deck_Size - 1];
        int DefaultPlace = 200;

        for (int i = 0; i < Deck_Size; i++)
        {
            GameObject Card = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

            if (CardManager.NewCards.Peek() == "Run")
            {
                Card.GetComponent<Image>().sprite = CardManager.card_RUN;
            } 
            else if (CardManager.NewCards.Peek() == "Double_Jump")
            {
                Card.GetComponent<Image>().sprite = CardManager.card_JUMP;
            }
            else if (CardManager.NewCards.Peek() == "Ennemy_Slam")
            {
                Card.GetComponent<Image>().sprite = CardManager.card_SLAM;
            }
            else if (CardManager.NewCards.Peek() == "Key")
            {
                Card.GetComponent<Image>().sprite = CardManager.card_KEY;
            }

            bool Switch = false;

            for (int u = 0; u < CardUsed.Length; u++)
            {
                if (CardManager.NewCards.Peek() == CardUsed[u])
                {
                    Card.transform.position = new Vector2((u + 1) * 100, CardUsedPlace[u] + 50);
                    CardUsedPlace[u] = Card.transform.position.y;
                    Switch = true;
                }
            }

            if (Switch == false)
            {
                Card.transform.position = new Vector2(DefaultPlace, 200);
                CardUsedPlace[i] = 200;
                CardUsed[i] = CardManager.NewCards.Peek();
                DefaultPlace += 200;
            }

            CardManager.NewCards.Pop();
        }
    }


}
