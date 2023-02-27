using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug : MonoBehaviour
{

    public CardManager CardManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            Stored();
        }
    }

    void Stored() {
        Debug.Log("Stored :");  
        foreach (var item in CardManager.shuffled_Deck) {
            Debug.Log(item);
        }
    }
}
