using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public CharacterController CharacterController;
    public CardManager CardManager;
    [SerializeField] GameObject Card_prefab;
    int Num_Of_Card;
    bool Lerp;

    // Start is called before the first frame update
    void Start()
    {
        Lerp = false;
        Num_Of_Card = Random.Range(3, 5);
    }

    private void Update()
    {
        if (Lerp == true)
        {
            CharacterController.Key.transform.position = Vector3.Lerp(CharacterController.Key.transform.position, transform.position, 2 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !CharacterController.Key.gameObject.activeSelf)
        {
            GameObject.Find("Locked").GetComponent<Animator>().SetTrigger("Lock");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && CharacterController.Key.gameObject.activeSelf)
        {
            if (CardManager.Deck.Count > 0)
            {
                CharacterController.Key.gameObject.GetComponent<MonoBehaviour>().enabled = false;
                Lerp = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetTrigger("Trigger");
    }

    void Give_Card()
    {
        if (CardManager.Deck.Count > 0)
        {
            Destroy(GameObject.Find("Card_" + (CardManager.Deck.Count - 1)));
            CardManager.Deck.Pop();
            CardManager.PlaceCards();
            Lerp = false;
            CharacterController.Key.SetActive(false);
            for(int i = 1; i <= Num_Of_Card; i++)
            {
                GameObject Card = Instantiate(Card_prefab, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Card.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5),5), ForceMode2D.Impulse);
            }
            Destroy(GetComponent<Chest>());
        }
    }
}
