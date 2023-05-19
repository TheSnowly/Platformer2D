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
        if (CardManager.Deck.Count > 0)
        {
            if (collision.gameObject.tag == "Player" && CharacterController.Key.gameObject.activeSelf)
            {
                CharacterController.Key.gameObject.SetActive(false);
                Give_Card();
                CharacterController.CardManage();
                CardManager.PlaceCards();
            }
        }
    }

    void Give_Card()
    {
        for(int i = 1; i <= Num_Of_Card; i++)
        {
            GameObject Card = Instantiate(Card_prefab, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
            Card.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5),5), ForceMode2D.Impulse);
        }
        Debug.Log("oh waw");
        Destroy(GetComponent<Chest>());
    }
}
