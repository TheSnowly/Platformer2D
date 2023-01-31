using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sr;

    //collision variable
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform feetPos;

    // jumping variables
    [SerializeField] bool can_Still_Jump;
    [SerializeField] bool is_jumping;
    float jumpForce = 10f;
    float max_Jump_Time = 0.3f;
    float current_Jump_Time;

    //fastfalling variables
    bool fastFalling;
    float base_Gravity = 4f;
    float fastfalling_Gravity = 8f;

    // x movement variables
    float horizontal_value;
    float moveSpeed_horizontal = 1000.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Value for the player x movement
        horizontal_value = Input.GetAxis("Horizontal");

        //flip character sprite depending on the direction the character goes in
        if(horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;

        //Checking if the player touch the floor
        isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.4f, ground);

        //checking if the player is jumping and can still do the hold 
        if (Input.GetButtonUp("Jump") && isGrounded == false) {
            can_Still_Jump = false;
        }
        if (isGrounded) {
            can_Still_Jump = true;
        }
        is_jumping = (Input.GetButton("Jump") && isGrounded == false && current_Jump_Time < max_Jump_Time && can_Still_Jump) ? true : false;

        //resetting few varuables when jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            current_Jump_Time = 0f;
        }

        fastFalling = (isGrounded == false) ? true : false;

    }

    void FixedUpdate()
    {       
        Jump();
        Move();
        FastFall();

    }


    private void FastFall()
    {
        if (fastFalling)
        {
            rb.gravityScale = Mathf.Lerp(base_Gravity, fastfalling_Gravity, 1000f * Time.fixedDeltaTime);
        }
        else
        {
            rb.gravityScale = base_Gravity;
        }
    }

    //hold jump
    private void Jump() {
        if (is_jumping) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            current_Jump_Time += Time.fixedDeltaTime;
        }
    }

    //x movement
    private void Move() {
        Vector2 ref_velocity = Vector2.zero;
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.08f);
    }
}
