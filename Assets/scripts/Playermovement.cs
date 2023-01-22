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
    [SerializeField] float base_Gravity = 4f;
    [SerializeField] float fastfalling_Gravity = 8f;

    //Double Jump variables
    [SerializeField] bool can_Double_Jump = false;
    bool double_Jumping = false;

    //run variables
    [SerializeField] float moveSpeed_Run = 2000.0f;
    [SerializeField] float moveSpeed_horizontal_default = 1000.0f;

    //Ennemy Slam variables
    bool ennemy_Slam = false;
    bool ennemy_Slam_Active = false;
    int slam_Force = 50;
    float stocked_Velocity_x;

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

        //ennemy Slam
        if (Input.GetKeyDown(KeyCode.C)) {
            ennemy_Slam = true;
        }

        //running
        if (Input.GetKeyDown(KeyCode.R)) {

            moveSpeed_horizontal = moveSpeed_Run;
            StartCoroutine(Run(3f));
        }

        //double jump
        if (Input.GetKeyDown(KeyCode.E)) {
            can_Double_Jump = true;
        }

        if (can_Double_Jump && can_jump == false) {
            double_Jumping = true;
            can_Double_Jump= false;
        }
   
        //jump
        if (Input.GetButtonDown("Jump") && can_jump)
        {
            current_Jump_Time = 0f;
            is_jumping = true;
        }

        //hold Jump
        if (Input.GetButton("Jump") && is_jumping) 
        {
            if (current_Jump_Time < max_Jump_Time) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                current_Jump_Time += Time.deltaTime;
            } else {
                    is_jumping = false;
            }
        }

        //finish jump
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

    //Run timer
    IEnumerator Run(float time) {
        yield return new WaitForSeconds(time);
        moveSpeed_horizontal = moveSpeed_horizontal_default;
    }

    void FixedUpdate()
    {

        //ennemy slam
        if (ennemy_Slam) {
            stocked_Velocity_x = rb.velocity.x;
            rb.velocity = new Vector2(rb.velocity.x / 3, 0);
            rb.AddForce(new Vector2(30 * (rb.velocity.x / Mathf.Abs(rb.velocity.x)), -slam_Force), ForceMode2D.Impulse);
            ennemy_Slam_Active = true;
            ennemy_Slam = false;
        }

        //jumping
        if (is_jumping && can_jump) 
        {             
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            can_jump = false;
        }

        //double jumping
        if (double_Jumping) 
        {             
            rb.velocity = new Vector2(rb.velocity.x / 3, 0);
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce * 2), ForceMode2D.Impulse);
            double_Jumping = false;
        }

        //horizontal movement
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.3f);

        //fastfalling
        if (fastFalling && can_jump == false)
        {
            rb.gravityScale = Mathf.Lerp(base_Gravity, fastfalling_Gravity, 1000f * Time.fixedDeltaTime); 
        }
        else
        {
            rb.gravityScale = base_Gravity;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check Ennemy for the ennemy slam
        if (other.tag == "Ennemy" && ennemy_Slam_Active) {
            rb.velocity = new Vector2(0, 2);
            rb.AddForce(new Vector2(stocked_Velocity_x * 4, 10), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {     
        ennemy_Slam_Active = false;
        can_jump = true;
    }
}
