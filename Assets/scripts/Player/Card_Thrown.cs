using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Thrown : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player") {
            if (other.gameObject.tag == "Breakable" || other.gameObject.tag == "Ennemy")
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            } else {
                Destroy(this.gameObject);
            }
        }
    }
/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
*/
}
