using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyCroco : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float speed = 0.5f;
    int direction;

    [SerializeField] LayerMask ground;
    [SerializeField] Transform changing_Side_Pos;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(changing_Side_Pos.position, 0.2f, ground);

        if(!(isGrounded)) {
            transform.localScale = new Vector3(-transform.localScale.y, transform.localScale.y, transform.localScale.z);
            direction = direction * -1;
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }
}
