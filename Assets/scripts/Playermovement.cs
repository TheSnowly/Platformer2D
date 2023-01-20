using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float horizontal_value;
    Vector2 ref_velocity = Vector2.zero;

    //Jump variables
    float jumpForce = 10f;
    bool fastFalling = false;
    float max_Jump_Time = 0.3f;
    float current_Jump_Time;
    [SerializeField] bool is_jumping = false;
    [SerializeField] bool can_jump = false;
    Vector2 moveDirectionJump = Vector2.zero;
    bool CanContinueJump
    {
        get
        {
            return current_Jump_Time <= max_Jump_Time && can_jump == false;
        }
    }


    [SerializeField] float moveSpeed_horizontal = 1000.0f;
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
            current_Jump_Time = 0f;
            is_jumping = true;
        }

        if (Input.GetButton("Jump") && is_jumping) 
        {
            if (current_Jump_Time < max_Jump_Time) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                current_Jump_Time += Time.deltaTime;
            } else {
                is_jumping = false;
            }
        }

        if (Input.GetButtonUp("Jump")) {
            is_jumping = false;
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

        //jumping
        if (is_jumping && can_jump)
        {             
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            can_jump = false;
        }

        //horizontal movement
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.3f);

        //fastfalling
        if (fastFalling && can_jump == false)
        {
            rb.gravityScale = Mathf.Lerp(4f, 7f, 1000f * Time.fixedDeltaTime); 
        }
        else
        {
            rb.gravityScale = 4f;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {     
        can_jump = true;
    }
}
