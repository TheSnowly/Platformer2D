using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrittaLaugh : MonoBehaviour
{

    [SerializeField] float time;
    float curTime;
    [SerializeField] AudioClip Laugh;

    public bool IsLaughing;

    private void Start()
    {
        curTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLaughing)
        {
            curTime -= Time.deltaTime;

            if (curTime < 0)
            {
                GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1);
                GetComponent<AudioSource>().PlayOneShot(Laugh);
                curTime = time;
            }
        }
    }
}
