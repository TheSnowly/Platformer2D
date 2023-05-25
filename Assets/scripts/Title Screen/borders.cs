using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borders : MonoBehaviour
{
    [SerializeField] GameObject Carreaux;

    void Start()
    {
        StartCoroutine(CaroSpawn());
    }

    IEnumerator CaroSpawn()
    {
        yield return new WaitForSeconds(0.7f);
        GameObject.Instantiate(Carreaux);
        StartCoroutine(CaroSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
