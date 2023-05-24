using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait(0.2f));
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Find("BossManager").GetComponent<BossManager>().NbCardThrow += 1;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
