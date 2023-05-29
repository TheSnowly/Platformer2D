using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Thrown : MonoBehaviour
{
    public float Direction;
    public float positionY;
    int Speed;

    private void Start()
    {
        Speed = 1;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * Direction, 0) * 3500 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Breakable" || other.gameObject.tag == "Ennemy")
            {
                if (other.transform.parent != null) {
                    other.gameObject.transform.parent.gameObject.GetComponent<EnemyDie>().Direc = Direction;
                    other.gameObject.transform.parent.gameObject.GetComponent<EnemyDie>().Die();
                } else {
                    other.gameObject.GetComponent<EnemyDie>().Direc = Direction;
                    other.gameObject.GetComponent<EnemyDie>().Die();
                }
                Destroy(this.gameObject);
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
