using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PouchCards : MonoBehaviour
{

    public CharacterController CharacterController;
    bool Switch;
    [SerializeField] GameObject ChoiceCardTuto;
    [SerializeField] GameObject text;
    [SerializeField] GameObject PouchGot;

    [SerializeField] GameObject RB;
    [SerializeField] GameObject LB;
    [SerializeField] GameObject A;

    bool PouchController = false;
    bool canchoose = false;

    // Start is called before the first frame update
    void Start()
    {
        Switch = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Switch && collision.tag == "Player")
        {
            Switch = false;
            StartCoroutine(Wait());
        }
    }

    private void Update()
    {
        if(PouchController && canchoose)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PouchController = false;
                GiveCardController("Jump");
            } else if (Input.GetButtonDown("Fire2"))
            {
                PouchController = false;
                GiveCardController("Ennemy_Slam");
            } else if (Input.GetButtonDown("Jump"))
            {
                PouchController = false;
                GiveCardController("Run");
            }
        }
    }

    void GiveCardController(string CardType)
    {
        for (int i = 1; i <= 3; i++)
        {
            Destroy(GameObject.Find("CardCh_" + i));
        }

        Destroy(text);
        Destroy(GameObject.Find("Black"));
        Destroy(GameObject.Find("PouchGot"));

        for (int i = 0; i <= 2; i++)
        {
            CardManager.Deck.Push(CardType);
            GameObject Card = GameObject.Instantiate(GameObject.Find("DECK").GetComponent<CardManager>().CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.name = "Card_" + i;
            Card.GetComponent<CardManagerSingle>().CardNb = i;
        }
        GameObject.Find("DECK").GetComponent<CardManager>().PlaceCards();
        CharacterController.CanMove = true;

        Destroy(A);
        Destroy(LB);
        Destroy(RB);
        this.gameObject.SetActive(false);
    }

    IEnumerator Wait()
    {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Idle", true);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Fall", false);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Run", false);
        CharacterController.CanMove = false;
        PouchGot.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject.Find("Black").GetComponent<Animator>().SetTrigger("Transi");
        yield return new WaitForSeconds(0.5f);
        PouchCard();

    }

    void PouchCard() {

        if (Input.GetJoystickNames().Length > 0)
        {
            text.GetComponent<TextMeshProUGUI>().text = "Choose your starter cards";
        } else
        {
            text.GetComponent<TextMeshProUGUI>().text = "Choose your starter cards (left click)";
        }
        text.SetActive(true);

        for(int i = 1; i <= 3; i++) {
            GameObject Card = GameObject.Instantiate(ChoiceCardTuto, new Vector3((Screen.width/4)*i, Screen.height/4, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

            if (Input.GetJoystickNames().Length > 0)
            {
                if (i == 1)
                {
                    RB = GameObject.Instantiate(RB, new Vector3(Card.transform.position.x, Card.transform.position.y +100, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                } else if (i == 2)
                {
                    LB = GameObject.Instantiate(LB, new Vector3(Card.transform.position.x, Card.transform.position.y +100, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                } else if (i == 3)
                {
                    A = GameObject.Instantiate(A, new Vector3(Card.transform.position.x, Card.transform.position.y +100, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                }                
            }
            Card.GetComponent<ChoiceCardTuto>().CardNB = i;
            Card.name = "CardCh_" + i;
        }
        if (Input.GetJoystickNames().Length > 0)
        {
            PouchController = true;
        }
            canchoose = true;
    }

}
