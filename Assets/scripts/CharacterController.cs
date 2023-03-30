using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sr;

    //Deck variable
    public CardManager CardManager;
    GameObject DestroyCard;

    //collision variable
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform feetPos;

    //run variable
    float moveSpeed_Run = 1300.0f;
    float moveSpeed_horizontal_default = 1000.0f;

    // jumping variables
    [SerializeField] bool can_Still_Jump;
    [SerializeField] bool is_jumping;
    float jumpForce = 10f;
    float max_Jump_Time = 0.2f;
    float current_Jump_Time;

    //coyote time variables
    float coyoteTime = 0.15f;
    float coyoteTimeTimer;

    //jump buffer variables
    float jumpBuffer = 0.15f;
    float jumpBufferTimer;

    //fastfalling variables
    bool fastFalling;
    float base_Gravity = 4f;
    float fastfalling_Gravity = 8f;

    //Ennemy Slam variables
    bool ennemy_Slam_Active = false;
    int slam_Force = 50;
    float stocked_Velocity_x;

    // x movement variables
    float horizontal_value;
    float moveSpeed_horizontal = 1000.0f;

    Vector2 RayPoint;
    [SerializeField] GameObject Card_Thrown_prefab;
    //Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Input.mousePosition;
        Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(mouse);

        //Debug.Log(RayPoint);

        //Value for the player x movement
        horizontal_value = Input.GetAxis("Horizontal");

        //flip character sprite depending on the direction the character goes in
        if(horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;

        //Checking if the player touch the floor
        isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.2f, ground);

        //jump buffer timer
        if (Input.GetButtonDown("Jump")) {
            jumpBufferTimer = jumpBuffer;
        } else {
            jumpBufferTimer -= Time.deltaTime;
        }

        //checking if the player is jumping and can still do the hold 
        if (Input.GetButtonUp("Jump") && isGrounded == false) {
            can_Still_Jump = false;
        }
        if (isGrounded) {
            coyoteTimeTimer = coyoteTime;
            can_Still_Jump = true;
            ennemy_Slam_Active = false;
        } else {
            coyoteTimeTimer -= Time.deltaTime;
        }
        is_jumping = (Input.GetButton("Jump") && isGrounded == false && current_Jump_Time < max_Jump_Time && can_Still_Jump) ? true : false;

        //resetting few variables when jumping and first jump
        if (jumpBufferTimer > 0f && coyoteTimeTimer > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferTimer = 0f;
            coyoteTimeTimer = 0f;
            current_Jump_Time = 0f;
        }

        //Cheking if the player is able to fast fall
        fastFalling = (isGrounded == false && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) ? true : false;

        //make a card effect
        if (CardManager.Deck.Count != 0) {

            if (Input.GetMouseButtonDown(0)) {
                if (CardManager.Deck.Peek() == "Ennemy_Slam")
                {
                    Destroy(GameObject.Find("Card_" + (CardManager.Deck.Count-1)));
                    CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), CardManager.Deck.Peek());
                    CardManager.Deck.Pop();
                    ennemy_Slam();
                    CardManager.PlaceCards();
                }
                else if (CardManager.Deck.Peek() == "Double_Jump")
                {
                    Destroy(GameObject.Find("Card_" + (CardManager.Deck.Count - 1)));
                    CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), CardManager.Deck.Peek());
                    CardManager.Deck.Pop();
                    double_Jump();
                    CardManager.PlaceCards();
                }
                else if (CardManager.Deck.Peek() == "Run")
                {
                    Destroy(GameObject.Find("Card_" + (CardManager.Deck.Count - 1)));
                    CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), CardManager.Deck.Peek());
                    CardManager.Deck.Pop();
                    //StartCoroutine(RunTimer(3f));
                    CardManager.PlaceCards();
                }
            }

            if (Input.GetMouseButtonDown(1)) {
                Instantiate(Card_Thrown_prefab, transform.position, Quaternion.identity);
            }
        }
    }

    void FixedUpdate()
    {       
        Jump();
        Move();
        FastFall();

    }

    //ennemy slam
    private void ennemy_Slam() {
        stocked_Velocity_x = rb.velocity.x;
        rb.velocity = new Vector2(rb.velocity.x / 3, 0);
        rb.AddForce(new Vector2(30 * (rb.velocity.x / Mathf.Abs(rb.velocity.x)), -slam_Force), ForceMode2D.Impulse);
        ennemy_Slam_Active = true;
    }

    private void Run() {
        StartCoroutine(RunTimer(3f));
    }

    //Run timer
    IEnumerator RunTimer(float time)
    {
        moveSpeed_horizontal = moveSpeed_Run;
        yield return new WaitForSeconds(time);
        moveSpeed_horizontal = moveSpeed_horizontal_default;
    }

    //double Jump
    private void double_Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x / 3, 0);
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce * 2), ForceMode2D.Impulse);
    }

    //fast falling
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

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check Ennemy for the ennemy slam
        if (other.tag == "Ennemy" && other.tag != "Damage") {

            if (ennemy_Slam_Active)
            {
                rb.velocity = new Vector2(0, 2);
                rb.AddForce(new Vector2(stocked_Velocity_x * 4, 10), ForceMode2D.Impulse);
                Destroy(other.gameObject);
                StartCoroutine(Wait(0.1f));

            } else
            {
                Debug.Log("boup");
                rb.velocity = new Vector2(rb.velocity.x / 3, 0);
                rb.AddForce(new Vector2(rb.velocity.x/2, jumpForce * 1.5f), ForceMode2D.Impulse);
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        ennemy_Slam_Active = false;
    }

}
