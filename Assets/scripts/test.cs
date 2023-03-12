using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{

    [SerializeField] GameObject CardPrefab;
    public CardManager CardManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            SceneManager.LoadScene("Level1");
        }
    }

    void Reset() {

        if (CardManager.Deck.Count > 0) {

            int N = CardManager.Deck.Count;


            foreach (var item in CardManager.Deck) {
                CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), CardManager.Deck.Peek());
                CardManager.Deck.Pop();
            }
            /*
            for (int i = 1; i <= N; i++) {
                Destroy(GameObject.Find("Card_" + i));
            }
            */
        }

        int n = 1;

        foreach (var item in CardManager.shuffled_Deck)
        {
            CardManager.Deck.Push(item);
            GameObject Card = GameObject.Instantiate(CardPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0), GameObject.FindGameObjectWithTag("Canvas").transform);
            Card.name = "Card_" + n;
            n++;
        }

        CardManager.shuffled_Deck.Clear();
        CardManager.PlaceCards();
    }
}
