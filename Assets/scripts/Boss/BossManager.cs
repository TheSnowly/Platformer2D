using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{

    [SerializeField] CardManager CardManager;
    [SerializeField] HealthBar HealthBar;
    CharacterController CharacterController;

    [SerializeField] GameObject CardThrow;
    [SerializeField] GameObject Cardtransform;

    [SerializeField] GameObject BossBG;
    [SerializeField] GameObject BossBlack;
    [SerializeField] GameObject Health;

    public float time;

    bool Switch;
    bool boss;

    int DS;
    public int NbCardThrow;

    public int precedent;

    // Start is called before the first frame update
    void Start()
    {
        NbCardThrow = 0;
        boss = false;
        Switch = true;
        CharacterController = GameObject.Find("Player").GetComponent<CharacterController>();
        CardManager.Boss = true;
    }

    public void GiveCard()
    {
        DS = CardManager.shuffled_Deck.Count;

        //creating cards in the canvas and pushing them in a deck
        for (int i = CardManager.shuffled_Deck.Count - 1; i >= 0; i--)
        {
            CardManager.Deck.Push(CardManager.shuffled_Deck[i]);
            GameObject Card = GameObject.Instantiate(CardManager.CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.GetComponent<Image>().sprite = CardManager.card_BACK;
            Card.name = "Card_" + i;
            Card.GetComponent<CardManagerSingle>().CardNb = i;

        }

        boss = true;

        CardManager.shuffled_Deck.Clear();
        CardManager.PlaceCards();
        CharacterController.CanMove = false;
        HealthBar.SetLife();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("BS").GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("End_Scree");
    }

    // Update is called once per frame
    void Update()
    {
        if (BossBG.activeSelf == true)
        {
            if (boss && time > 0)
            {
                time -= Time.deltaTime;
            } else if (time < 0)
            {
                GameObject.Find("Boss").GetComponent<Animator>().SetTrigger("Idle");
            }
        }

        if (DS==NbCardThrow && Switch == true && boss == true)
        {
            Switch = false;
            BossBG.SetActive(false);
            BossBlack.SetActive(false);
            Health.SetActive(false);
            GameObject.Find("Explosion").GetComponent<Animator>().SetTrigger("Explo");
            GameObject.Find("Gritta").GetComponent<Animator>().SetTrigger("Dieps");
            StartCoroutine(Wait());
        }

        if (CardManager.Deck.Count > 0)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                GameObject.Find("ZzipoFIGHT").GetComponent<Animator>().SetTrigger("Trigger");
                GameObject.Find("Player").GetComponent<CharacterController>().NoiseSource.GenerateImpulse();
                GameObject Card = GameObject.Instantiate(CardThrow, Cardtransform.transform.position, Quaternion.Euler(-680, -352, 512));
                CharacterController.CardManage();
                CardManager.PlaceCards();
            }
        }
    }
}
