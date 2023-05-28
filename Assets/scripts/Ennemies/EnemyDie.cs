using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public void Die() {
        Rigidbody2D rb = GetComponentInChildren<Rigidbody2D>();
        rb.gravityScale = 7;
        rb.AddForce(new Vector2(-10,10), ForceMode2D.Impulse); 
        StartCoroutine(Waitr());
    }

    IEnumerator Waitr(){
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
