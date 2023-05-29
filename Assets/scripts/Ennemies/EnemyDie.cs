using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyDie : MonoBehaviour
{

    public float Direc;

    private void Start()
    {
        Direc = -1;
    }
    public void Die() {
        if(GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().SetTrigger("Dead");
        } else
        {
            foreach (Transform child in this.transform)
            {
                if (child.GetComponent<Animator>() != null)
                {
                    child.GetComponent<Animator>().SetTrigger("Dead");
                }
            }
        }
        GameObject.Find("Player").GetComponent<CharacterController>().NoiseSource.GenerateImpulse();
        Rigidbody2D rb = GetComponentInChildren<Rigidbody2D>();
        DeleteAllComponents();
        rb.gravityScale = 7;
        rb.AddForce(new Vector2(10 * Direc, 10), ForceMode2D.Impulse);
        StartCoroutine(Waitr());
    }

    IEnumerator Waitr() {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    void DeleteAllComponents()
    {
        Destroy(GetComponent<MonoBehaviour>());
        Destroy(GetComponent<BoxCollider2D>());

        foreach (Transform child in this.transform)
        {
            Destroy(child.GetComponent<MonoBehaviour>());
            Destroy(child.GetComponent<BoxCollider2D>());
        }
    }
}
