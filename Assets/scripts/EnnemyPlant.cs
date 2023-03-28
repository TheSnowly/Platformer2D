using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPlant : MonoBehaviour
{

    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite ClosedSprite;
    [SerializeField] GameObject Damage;
    string CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = "Open";
        StartCoroutine(ChangeState(2f));
    }

    IEnumerator ChangeState(float time) {
        float timeElapsed = 5f;
        if (timeElapsed < 0) {
            timeElapsed -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("ça part de là");
        if (CurrentState == "Open") {
            GetComponent<SpriteRenderer>().sprite = ClosedSprite;
            GetComponent<BoxCollider2D>().enabled = false;
            Damage.GetComponent<BoxCollider2D>().enabled = true;
            CurrentState = "Closed";
            StartCoroutine(ChangeState(2f));
        }
        if (CurrentState == "Closed") {
            GetComponent<SpriteRenderer>().sprite = OpenSprite;
            GetComponent<BoxCollider2D>().enabled = true;
            Damage.GetComponent<BoxCollider2D>().enabled = false;
            CurrentState = "Open";
            StartCoroutine(ChangeState(2f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
