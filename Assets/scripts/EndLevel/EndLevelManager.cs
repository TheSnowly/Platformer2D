using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndLevelManager : MonoBehaviour
{
    [SerializeField] GameObject CardPrefab;
    [SerializeField] GameObject EndLevel;
    [SerializeField] GameObject BG;
    [SerializeField] GameObject Progresse;
    [SerializeField] GameObject ZzzipoProgress;
    [SerializeField] GameObject TextProgress;

    [SerializeField] public GameObject LB;
    [SerializeField] public GameObject RB;
    [SerializeField] public GameObject Confirm;

    [SerializeField] GameObject[] ProgressPoints = new GameObject[6];

    public CardManager CardManager;
    public Timer Timer;
    public CardClick CardClick;

    bool Switch;

    static public int NbOfLevelPlayed;
    [SerializeField] GameObject transi;

    public string nextlvl;


    // Start is called before the first frame update
    void Start()
    {
        Switch = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Switch == false && other.tag == "Player") {
            GameObject.Find("ChronoTimer").GetComponent<Animator>().SetTrigger("Fin");
            GameObject.Find("Slider").GetComponent<Animator>().SetTrigger("End");
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
            transi.GetComponent<Animator>().SetTrigger("EndTransi2");
            NbOfLevelPlayed += 1;
            Timer.StopTime = true;
            EndLevel.SetActive(true);
            other.GetComponent<CharacterController>().CanMove = false;
            NextLevelChoice();
            Switch = true;
        }
    }

    public void Progress()
    {
        Progresse.SetActive(true);
        EndLevel.SetActive(false);
        ZzzipoProgress.transform.position = new Vector3(ProgressPoints[NbOfLevelPlayed - 1].transform.position.x, ProgressPoints[NbOfLevelPlayed - 1].transform.position.y + 3, 0);
        StartCoroutine(MovePlayer());
    }

    IEnumerator MovePlayer()
    {
        float i = 2f;

        while (i > 0)
        {
            i -= Time.deltaTime;
            ZzzipoProgress.transform.position = Vector3.Lerp(ZzzipoProgress.transform.position, new Vector3(ProgressPoints[NbOfLevelPlayed].transform.position.x, ProgressPoints[NbOfLevelPlayed].transform.position.y + 3, 0), 1f*Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        TextProgress.GetComponent<TextMeshProUGUI>().text = (5 - NbOfLevelPlayed) + " more to go!";
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Transi").GetComponent<Animator>().SetTrigger("EndTransi3");
        TextProgress.GetComponent<TextMeshProUGUI>().text = "";
        Progresse.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        if (nextlvl == "Double_Jump")
        {
            SceneManager.LoadScene("JUMP_Level");
        }
        else if (nextlvl == "Ennemy_Slam")
        {
            SceneManager.LoadScene("SLAM_Level");
        }
        else if (nextlvl == "Run")
        {
            SceneManager.LoadScene("RUN_Level");
        }
    }

    void NextLevelChoice() {

        if (CardManager.Deck.Count > 0) {
            int DS = CardManager.Deck.Count;

            for (int i = 0; i < DS; i++) {
                Destroy(GameObject.Find("Card_" + i));
            }

            for (int i = 0; i < DS; i++) {
                CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), CardManager.Deck.Peek());
                CardManager.Deck.Pop();
            }
        }

        if (NbOfLevelPlayed == 5)
        {
            SceneManager.LoadScene("BossRoom");
        }   

        for (int i = 1; i <= 2; i++) {

            GameObject Card = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);


            Card.name = "CardEnd_" + i;

            string Random_Card = "";

            while(Random_Card == "Key" || Random_Card == "") {
                Random_Card = CardManager.shuffled_Deck[Random.Range(0, CardManager.shuffled_Deck.Count)];
            }

            Card.GetComponent<CardClick>().CardType = Random_Card;
            Card.GetComponent<CardClick>().CardNb = i;

            if (Random_Card == "Run") {
                Card.GetComponent<Image>().sprite = CardManager.card_RUN;
            } else if (Random_Card == "Double_Jump") {
                Card.GetComponent<Image>().sprite = CardManager.card_JUMP;
            } else if (Random_Card == "Ennemy_Slam") {
                Card.GetComponent<Image>().sprite = CardManager.card_SLAM;
            }

            Card.transform.position = new Vector3(50 + (i * 150), Screen.height/3, -10);
            if (Input.GetJoystickNames().Length > 0)
            {
                if (i == 1)
                {
                    LB = GameObject.Instantiate(LB, new Vector3(Card.transform.position.x, Card.transform.position.y + 100, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                } else if (i == 2)
                {
                    RB = GameObject.Instantiate(RB, new Vector3(Card.transform.position.x, Card.transform.position.y + 100, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                }
            }
        }

    }

}
