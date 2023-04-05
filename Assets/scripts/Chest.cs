using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public CharacterController CharacterController;
    public CardManager CardManager;
    [SerializeField] GameObject Card_prefab;
    int Num_Of_Card;

    // Start is called before the first frame update
    void Start()
    {
        Num_Of_Card = Random.Range(3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && CardManager.Deck.Peek() == "Key")
        {
            CharacterController.Key.gameObject.SetActive(false);
            CharacterController.CardManage();
            CardManager.PlaceCards();
            Give_Card();
        }
    }

    void Give_Card()
    {
        for(int i = 1; i <= Num_Of_Card; i++)
        {
            GameObject Card = Instantiate(Card_prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Card.GetComponent<Rigidbody2D>().AddForce(new Vector2(3, 0));
        }
    }
}
