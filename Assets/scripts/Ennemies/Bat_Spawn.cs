using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Spawn : MonoBehaviour
{

    [SerializeField] GameObject BatPrefab;
    [SerializeField] int time;
    bool CanSummon = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Summon(time));
    }

    IEnumerator Summon(int Time) {
        while (CanSummon == false) {
            Instantiate(BatPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(Time);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
