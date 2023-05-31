using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    [SerializeField] GameObject Square;
    [SerializeField] float time;

    void Start()
    {
        StartCoroutine(CaroSpawn(time));
    }

    IEnumerator CaroSpawn(float Wtime)
    {

        yield return new WaitForSeconds(Wtime);
        GameObject.Instantiate(Square, GameObject.Find("BossBG").transform);
        StartCoroutine(CaroSpawn(time));
    }
}
