using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    //Checkpoint variables  
    Vector2 CurrentCheckpoint;
    float timeElapsed;
    Vector3 ref_velocity;

    //death variables
    public bool isDying;
    public bool TimerOut = false;


    // Start is called before the first frame update
    void Start()
    {
        //the first checkpoint is where the player spawn
        CurrentCheckpoint = gameObject.transform.position;

        ref_velocity = Vector3.zero;

        isDying = false;
    }

    void OnTriggerEnter2D(Collider2D col) {

        //When the player collide a Checkpoint, it stores his coordinates and destroy the gameobject so that the player cannot go back to a previous Checkpoint.
        if(col.gameObject.tag == "Checkpoint") {
            CurrentCheckpoint = gameObject.transform.position;
            Destroy(col.gameObject);
        }
    }

    public void TimerGameOver()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Vector3 target = new Vector3(transform.position.x - 2f, transform.position.y + 2, 0);
        GameObject.Find("TransiDeath").GetComponent<Animator>().SetTrigger("Transi");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }

    public IEnumerator DeathAnimation() {

        timeElapsed = 1f;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Vector3 target = new Vector3(transform.position.x - 2f, transform.position.y + 2, 0);
        GameObject.Find("TransiDeath").GetComponent<Animator>().SetTrigger("Transi");
        if (!TimerOut)
        {
            //while the timer isn't finished, the player moves a bit
            while (timeElapsed > 0){
               transform.position = Vector3.SmoothDamp(transform.position, target, ref ref_velocity, 0.6f); 
               timeElapsed -= Time.deltaTime;
               yield return null;
            }

            //then the player respawn
            Respawn();
        } else
        {
            //yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("GameOver");
        }
    }

    //respawing
    void Respawn() {
        isDying = false;
        GetComponent<Animator>().SetBool("Die", false);
        GetComponent<CharacterController>().CanMove = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.transform.position = CurrentCheckpoint;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
