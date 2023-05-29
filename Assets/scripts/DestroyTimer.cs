using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{

    [SerializeField] float Time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait(Time));
    }

    IEnumerator Wait(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
