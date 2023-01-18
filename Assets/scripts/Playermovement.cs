using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float horizontal_value;
    Vector2 ref_velocity = Vector2.zero;

    float jumpForce = 10f;

    [SerializeField] float moveSpeed_horizontal = 700.0f;
    [SerializeField] float moveSpeed_vertical = 700.0f;
    [SerializeField] bool is_jumping = false;
    [SerializeField] bool can_jump = false;
    bool fastFalling = false;
    [Range(0, 1)][SerializeField] float smooth_time = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //Debug.Log(Mathf.Lerp(current, target, 0));
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_value = Input.GetAxis("Horizontal");

        if(horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;
   
        if (Input.GetButtonDown("Jump") && can_jump)
        {
            is_jumping = true;
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            fastFalling = true;
        } else
        {
            fastFalling = false;
        }

        
    }
    void FixedUpdate()
    {
        if (is_jumping && can_jump)
        {           
            is_jumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            can_jump = false;
        }
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.3f);

        if (fastFalling)
        {

            rb.gravityScale = Mathf.Lerp(1f, 6f, 2000f * Time.deltaTime);
            //Vector2 target_velocity_falling = new Vector2(rb.velocity.x , vertical_value * moveSpeed_vertical * Time.fixedDeltaTime);
            //rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity_falling, ref ref_velocity, 0.5f);
        }
        else
        {
            rb.gravityScale = 1f;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {     
        can_jump = true;
    }
}
