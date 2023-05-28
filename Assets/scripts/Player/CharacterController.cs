using System.Collections;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class CharacterController : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    //cinematics variable
    public bool CanMove;

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
    [SerializeField] bool is_jumping;
    float jumpForce = 15f;

    //coyote time variables
    float coyoteTime = 0.15f;
    float coyoteTimeTimer;

    //jump buffer variables
    float jumpBuffer = 0.15f;
    float jumpBufferTimer;

    //fastfalling variables
    bool fastFalling;
    float base_Gravity = 4f;
    float fastfalling_Gravity = 12f;

    //Ennemy Slam variables
    bool ennemy_Slam_Active = false;
    int slam_Force = 50;
    float stocked_Velocity_x;

    //x movement variables
    float horizontal_value;
    float moveSpeed_horizontal = 1000.0f;

    //Card throw variable
    [SerializeField] GameObject Card_Thrown_prefab;
    int DirectionThrow;

    //Key variable
    [SerializeField] public GameObject Key;

    //misc
    public PlayerDamage PlayerDamage;
    CinemachineImpulseSource NoiseSource;
    [SerializeField] ParticleSystem Particles;

    // Start is called before the first frame update
    void Start()
    {
        CanMove = true;
        NoiseSource = GetComponent<CinemachineImpulseSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove) {

            //Value for the player x movement
            horizontal_value = Input.GetAxis("Horizontal");

            //flip character sprite depending on the direction the character goes in
            if (horizontal_value > 0){
                sr.flipX = false;
                DirectionThrow = 1;
            } 
            else if (horizontal_value < 0){
                sr.flipX = true;
                DirectionThrow = -1;
            } 

            //Checking if the player touch the floor
            isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.2f, ground);

            //jump buffer timer
            if (Input.GetButtonDown("Jump")) {
                jumpBufferTimer = jumpBuffer;
            } else {
                jumpBufferTimer -= Time.deltaTime;
            }


            if (horizontal_value != 0 && isGrounded && rb.velocity.y == 0)
            {
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);
            } else if (horizontal_value == 0 && isGrounded && rb.velocity.y == 0)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
            } else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", false);
            }

            if (isGrounded) {
                coyoteTimeTimer = coyoteTime;
                ennemy_Slam_Active = false;
                animator.SetBool("Fall", false);
            } else {
                coyoteTimeTimer -= Time.deltaTime;
                animator.SetBool("Run", false);
            }

            if(Input.GetButton("Jump")) {
                rb.gravityScale = 4f;
            } else {
                rb.gravityScale = 6.5f;
            }

            //resetting few variables when jumping and first jump
            if (jumpBufferTimer > 0f && coyoteTimeTimer > 0f)
            {
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.5f);
                jumpBufferTimer = 0f;
                coyoteTimeTimer = 0f;
            }

            //Cheking if the player is able to fast fall
            fastFalling = (isGrounded == false && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) ? true : false;

            //checking if the player is falling, play the fall animation if yes
            if (rb.velocity.y < 0)
            {
                animator.SetBool("Fall", true);
            }

            //make a card effect
            if (CardManager.Deck.Count != 0) {

                if (CardManager.Deck.Peek() == "Key") {
                    Key.SetActive(true);
                } else {
                    Key.SetActive(false);
                }

                if (Input.GetMouseButtonDown(0)) {
                    if (CardManager.Deck.Peek() == "Ennemy_Slam")
                    {
                        CardManage();
                        ennemy_Slam();
                        CardManager.PlaceCards();
                    }
                    else if (CardManager.Deck.Peek() == "Double_Jump")
                    {
                        CardManage();
                        double_Jump();
                        CardManager.PlaceCards();
                    }
                    else if (CardManager.Deck.Peek() == "Run")
                    {
                        CardManage();
                        StartCoroutine(RunTimer(3f));
                        CardManager.PlaceCards();
                    }
                }

                if (Input.GetMouseButtonDown(1)) {
                    NoiseSource.GenerateImpulse();
                    GameObject thrown = Instantiate(Card_Thrown_prefab, new Vector2(transform.position.x + (1.5f * DirectionThrow), transform.position.y), Quaternion.identity);
                    thrown.GetComponent<Card_Thrown>().Direction = DirectionThrow;
                    thrown.GetComponent<Card_Thrown>().positionY = transform.position.y;
                    CardManage();
                    CardManager.PlaceCards();
                }
            }else {
                Key.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {   
        if (CanMove) {
            Move();
            FastFall();
        }    

    }

    //ennemy slam
    private void ennemy_Slam() {
        if (rb.velocity.x != 0) {
            stocked_Velocity_x = rb.velocity.x;
            rb.velocity = new Vector2(rb.velocity.x / 3, 0);
            rb.AddForce(new Vector2(30 * (rb.velocity.x / Mathf.Abs(rb.velocity.x)), -slam_Force), ForceMode2D.Impulse);
        } else {
            stocked_Velocity_x = 20;
            rb.AddForce(new Vector2(30, -slam_Force), ForceMode2D.Impulse);
        }
        ennemy_Slam_Active = true;
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
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce * 1.8f), ForceMode2D.Impulse);
    }

    //fast falling
    private void FastFall()
    {
        if (fastFalling)
        {
            rb.gravityScale = Mathf.Lerp(base_Gravity, fastfalling_Gravity, 1000f * Time.fixedDeltaTime);
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

        if (other.tag == "Ennemy" && other.tag != "Damage" && PlayerDamage.isDying == false && ennemy_Slam_Active == false)
        {
            Time.timeScale = 0.05f;
            StartCoroutine(TScale());
            Particles.Play(true);
            NoiseSource.GenerateImpulse();
            rb.velocity = new Vector2(rb.velocity.x / 3, 0);
            rb.AddForce(new Vector2(rb.velocity.x / 2, jumpForce * 1.5f), ForceMode2D.Impulse);
            if (other.transform.parent != null) {
                other.gameObject.transform.parent.gameObject.GetComponent<EnemyDie>().Die();
            } else {
                other.gameObject.GetComponent<EnemyDie>().Die();
            }

        } else if (ennemy_Slam_Active && (other.tag == "Damage" || other.tag == "Ennemy"))
        {
            Time.timeScale = 0.05f;
            StartCoroutine(TScale());
            Particles.Play(true);
            NoiseSource.GenerateImpulse();
            rb.velocity = new Vector2(0, 2);
            rb.AddForce(new Vector2(stocked_Velocity_x * 4, 10), ForceMode2D.Impulse);
            if (other.transform.parent != null) {
                other.gameObject.transform.parent.gameObject.GetComponent<EnemyDie>().Die();
            } else {
                other.gameObject.GetComponent<EnemyDie>().Die();
            }
            StartCoroutine(Wait(0.1f));

        } else if ((other.tag == "Damage" || other.tag == "Spike") && other.tag != "Ennemy" && PlayerDamage.isDying == false)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            PlayerDamage.isDying = true;
            PlayerDamage.EnnemyWhoKilled = other.gameObject;
            CanMove = false;
            rb.gravityScale = 0;
            StartCoroutine(PlayerDamage.DeathAnimation());
        }


    }

    //Card manager system, delete card when used and put the card in the shuffled deck
    public void CardManage() {
        Destroy(GameObject.Find("Card_" + (CardManager.Deck.Count - 1)));
        CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), CardManager.Deck.Peek());
        CardManager.Deck.Pop();
    }

    IEnumerator TScale() {
        yield return new WaitForSeconds(0.008f);
        Time.timeScale = 1f;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        ennemy_Slam_Active = false;
    }

}
