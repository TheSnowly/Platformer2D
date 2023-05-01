using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSpawner : MonoBehaviour
{
    [SerializeField] GameObject CardSpawned; 
    GameObject Card;

    // Start is called before the first frame update
    void Start()
    {
        Card = null;
        StartCoroutine(ChangeState(2f));
    }

    IEnumerator ChangeState(float time) {
        float timeElapsed = time;
        while (timeElapsed > 0) {
            timeElapsed -= Time.deltaTime;
            yield return null;
        }
        if (Card == null) {
            Card = GameObject.Instantiate(CardSpawned, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        }
        StartCoroutine(ChangeState(4f));
    }
}
