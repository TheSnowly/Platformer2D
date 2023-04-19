using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    [SerializeField] public CardManager CardManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {

            int C = Random.Range(1, 4);
            if (C==1) {
                CardManager.NewCards.Push("Run");
            } else if (C==2) {
                CardManager.NewCards.Push("Ennemy_Slam");
            } else if (C==3) {
                CardManager.NewCards.Push("Double_Jump");
            } else if (C==4) {
                CardManager.NewCards.Push("Key");
            }
            Destroy(this.gameObject);
        }
    }
}
