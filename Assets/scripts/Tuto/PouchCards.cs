using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouchCards : MonoBehaviour
{

    public CharacterController CharacterController;
    bool Switch;
    [SerializeField] GameObject ChoiceCardTuto;

    // Start is called before the first frame update
    void Start()
    {
        Switch = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Switch && collision.tag == "Player")
        {
            PouchCard();
            Switch = false;
        }
    }

    void PouchCard() {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        CharacterController.CanMove = false;

        if (CardManager.Deck.Count != 0) {
            int DS = CardManager.Deck.Count;
            for(int i = 0; i <= DS - 1; i++){
                Destroy(GameObject.Find("Card_" + i));
                CardManager.Deck.Pop();
            }
        }

        CardManager.shuffled_Deck.Clear();

        for(int i = 1; i <= 3; i++) {
            GameObject Card = GameObject.Instantiate(ChoiceCardTuto, new Vector3(200 * i, 150, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.GetComponent<ChoiceCardTuto>().CardNB = i;
            Card.name = "CardCh_" + i;
        }
    }

}
