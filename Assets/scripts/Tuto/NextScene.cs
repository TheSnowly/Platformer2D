using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    public CardManager CardManager;
    public CharacterController CharacterController;

    [SerializeField] GameObject Transi;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            Transi.GetComponent<Animator>().SetTrigger("EndTransi");
            if (CardManager.Deck.Count > 0) {
                CharacterController.CanMove = false;
                int DS = CardManager.Deck.Count;

                for (int i = 0; i < DS; i++) {
                    Destroy(GameObject.Find("Card_" + i));
                }

                for (int i = 0; i < DS; i++) {
                    CardManager.shuffled_Deck.Insert(Random.Range(0, CardManager.shuffled_Deck.Count), CardManager.Deck.Peek());
                    CardManager.Deck.Pop();
                }
            }
        }
    }
}
