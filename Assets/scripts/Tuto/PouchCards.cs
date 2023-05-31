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
        
    }

    IEnumerator Wait()
    {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        CharacterController.CanMove = false;
        PouchGot.SetActive(true);
        GameObject.Find("Black").GetComponent<Animator>().SetTrigger("Transi");
        yield return new WaitForSeconds(0.5f);
        PouchCard();

    }

    void PouchCard() {

        text.GetComponent<TextMeshProUGUI>().text = "Choose your starter cards";
        text.SetActive(true);

        for(int i = 1; i <= 3; i++) {
            GameObject Card = GameObject.Instantiate(ChoiceCardTuto, new Vector3((Screen.width/4)*i, Screen.height/4, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            if (i == 1)
            {
                GameObject.Instantiate(RB, new Vector3(Card.transform.position.x, Card.transform.position.y +100, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            } else if (i == 2)
            {
                GameObject.Instantiate(LB, new Vector3(Card.transform.position.x, Card.transform.position.y +100, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            } else if (i == 3)
            {
                GameObject.Instantiate(A, new Vector3(Card.transform.position.x, Card.transform.position.y +100, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            }
            Card.GetComponent<ChoiceCardTuto>().CardNB = i;
            Card.name = "CardCh_" + i;
        }
    }

}
