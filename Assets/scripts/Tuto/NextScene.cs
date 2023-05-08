using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    public CardManager CardManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
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
            SceneManager.LoadScene("First_Level");
        }
    }
}
