using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cine2 : MonoBehaviour
{

    [SerializeField]
    Animator CamAnim;
    [SerializeField]
    Animator Cinemode;
    [SerializeField]
    GameObject BlackScreen;
    [SerializeField]
    GameObject Ghead;
    [SerializeField]
    GameObject BossBG;
    [SerializeField]
    GameObject Health;
    [SerializeField] GameObject Health2;

    [SerializeField] AudioClip FeedBack;
    [SerializeField] AudioClip IntroBossM;
    [SerializeField] AudioClip BossM;

    Animator Grittanim;
    GameObject Gritta;

    Rigidbody2D rb;
    GameObject Player;
    Animator PlayerAnimator;
    CharacterController characterController;

    bool Switch = true;


    // Start is called before the first frame update
    void Start()
    {
        
        Gritta = GameObject.Find("Gritta");
        Player = GameObject.Find("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        PlayerAnimator = Player.GetComponent<Animator>();
        characterController = Player.GetComponent<CharacterController>();
        Grittanim = GameObject.Find("Gritta").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Switch) {
            Cinemode.SetBool("Cine", true);
            characterController.CanMove = false;
            CamAnim.SetBool("Cam", true);
            CineManager();
            Switch = false;
        }
    }


    void CineManager()
    {
        Stop();
        MoveAnim();
        StartCoroutine(Move(Player, new Vector3(Player.transform.position.x + 10, Player.transform.position.y, 0), 1f));
    }

    void Stop()
    {
        rb.velocity = Vector2.zero;
        PlayerAnimator.SetBool("Run", false);
        PlayerAnimator.SetBool("Fall", false);
        PlayerAnimator.SetBool("Idle", true);
    }

    void MoveAnim()
    {
        PlayerAnimator.SetBool("Run", true);
        PlayerAnimator.SetBool("Idle", false);
    }

    IEnumerator Move(GameObject Char, Vector3 targetPos, float time)
    {
        while(time > 0)
        {
            Char.transform.position = Vector3.Lerp(Char.transform.position, targetPos, 1f*Time.deltaTime);
            time -= Time.deltaTime;
            yield return null;
        }
        if (Char.name == "Player")
        {
            Stop();
            StartCoroutine(Wait(1));
        } else
        {
            yield return null;
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Grittanim.SetBool("Laugh", true);
        Gritta.GetComponent<GrittaLaugh>().IsLaughing = true;
        yield return new WaitForSeconds(2);
        Grittanim.SetBool("Laugh", false);
        Gritta.GetComponent<GrittaLaugh>().IsLaughing = false;
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().PlayOneShot(FeedBack);
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Ghead.GetComponent<Animator>().SetTrigger("Turn");
        yield return new WaitForSeconds(5f);
        GetComponent<AudioSource>().PlayOneShot(IntroBossM);
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(BossM);
        GetComponent<AudioSource>().loop = true;
        BossBG.SetActive(true);
        Health.SetActive(true);
        Health2.SetActive(true);
        GameObject.Find("BossManager").GetComponent<BossManager>().GiveCard();
    }

}
