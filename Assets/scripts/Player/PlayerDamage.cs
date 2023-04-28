using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    //Checkpoint variables  
    Vector2 CurrentCheckpoint;
    float timeElapsed;
    Vector3 ref_velocity;

    //death variables
    GameObject EnnemyWhoKilled;
    public bool isDying;

    // Start is called before the first frame update
    void Start()
    {
        //the first checkpoint is where the player spawn
        CurrentCheckpoint = gameObject.transform.position;

        timeElapsed = 10;

        ref_velocity = Vector3.zero;

        isDying = false;
    }

    void OnTriggerEnter2D(Collider2D col) {

        //When the player collide a Checkpoint, it stores his coordinates and destroy the gameobject so that the player cannot go back to a previous Checkpoint.
        if(col.gameObject.tag == "Checkpoint") {
            CurrentCheckpoint = gameObject.transform.position;
            Destroy(col.gameObject);
        }

        //When the player collide a Damage Zone, he dies.
        if(col.gameObject.tag == "Damage") {
            EnnemyWhoKilled = col.gameObject;
            EnnemyWhoKilled.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            isDying = true;
            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation() {

        timeElapsed = 1.5f;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Vector3 target = new Vector3(transform.position.x - 2f, transform.position.y + 2, 0);

        //while the timer isn't finished, the player moves a bit
        while(timeElapsed > 0){
           transform.position = Vector3.SmoothDamp(transform.position, target, ref ref_velocity, 0.6f); 
           timeElapsed -= Time.deltaTime;
           yield return null;
        }
        //then the player respawn
        Respawn();
    }

    //respawing
    void Respawn() {
        isDying = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        EnnemyWhoKilled.GetComponent<Collider2D>().enabled = true;
        gameObject.transform.position = CurrentCheckpoint;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
