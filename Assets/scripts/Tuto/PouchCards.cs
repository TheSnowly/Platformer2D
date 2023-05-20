using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouchCards : MonoBehaviour
{

    public CharacterController CharacterController;
    bool Switch;
    [SerializeField] GameObject ChoiceCardTuto;
    [SerializeField] GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        Switch = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Switch && collision.tag == "Player")
        {
            PouchCard();
            Switch = false;
        }
    }

    void PouchCard() {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        CharacterController.CanMove = false;

        text.SetActive(true);

        for(int i = 1; i <= 3; i++) {
            GameObject Card = GameObject.Instantiate(ChoiceCardTuto, new Vector3(300 * i, 150, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.GetComponent<ChoiceCardTuto>().CardNB = i;
            Card.name = "CardCh_" + i;
        }
    }

}
