using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cine1 : MonoBehaviour
{

    [SerializeField]
    Animator CamAnim;
    [SerializeField]
    Animator Cinemode;

    Animator Grittanim;
    [SerializeField] GameObject Gritta;

    [SerializeField] AudioClip Whistle;

    Rigidbody2D rb;
    GameObject Player;
    Animator PlayerAnimator;
    CharacterController characterController;

    bool Switch = true;

    // Start is called before the first frame update
    void Start()
    {
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
        if (CardManager.Deck.Count != 0) {
            int DS = CardManager.Deck.Count;
            for(int i = 0; i <= DS - 1; i++){
                Destroy(GameObject.Find("Card_" + i));
                CardManager.Deck.Pop();
            }
        }

        CardManager.shuffled_Deck.Clear();

        Stop();
        MoveAnim();
        StartCoroutine(Move(Player, new Vector3(Player.transform.position.x + 10, Player.transform.position.y, 0), 1f));
    }

    void Stop()
    {
        rb.velocity = Vector2.zero;
        PlayerAnimator.SetBool("Run", false);
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
        yield return new WaitForSeconds(0.5f);
        Gritta.GetComponent<AudioSource>().PlayOneShot(Whistle);
        Grittanim.SetTrigger("Move");
        yield return new WaitForSeconds(0.5f);
        CamAnim.SetBool("Cam", false);
        Cinemode.SetBool("Cine", false);
        characterController.CanMove = true;
    }

}
