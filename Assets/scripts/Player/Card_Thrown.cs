using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Thrown : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(3500* Time.deltaTime, 0); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Breakable" || other.gameObject.tag == "Ennemy")
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player") {
            if (other.gameObject.tag == "Breakable")
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            } else {
                Destroy(this.gameObject);
            }
        }
    }
}
