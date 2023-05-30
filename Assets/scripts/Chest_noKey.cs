using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_noKey : MonoBehaviour
{
    [SerializeField] GameObject Card_prefab;
    int Num_Of_Card;

    bool Switch;

    // Start is called before the first frame update
    void Start()
    {
        Switch = true;
        Num_Of_Card = 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Switch && collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Trigger");
            Switch = false;
        }
    }

    void Give_Card()
    {
        for(int i = 1; i <= Num_Of_Card; i++)
        {
            GameObject Card = Instantiate(Card_prefab, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
            Card.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5),5), ForceMode2D.Impulse);
        }
    }
}
