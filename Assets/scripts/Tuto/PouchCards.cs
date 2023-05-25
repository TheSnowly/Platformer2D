using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouchCards : MonoBehaviour
{

    public CharacterController CharacterController;
    bool Switch;
    [SerializeField] GameObject ChoiceCardTuto;
    [SerializeField] GameObject text;
    [SerializeField] GameObject PouchGot;

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

        text.SetActive(true);

        for(int i = 1; i <= 3; i++) {
            GameObject Card = GameObject.Instantiate(ChoiceCardTuto, new Vector3(100 + (100 * i), 50, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.GetComponent<ChoiceCardTuto>().CardNB = i;
            Card.name = "CardCh_" + i;
        }
    }

}
