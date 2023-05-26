using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZzzipoSleep : MonoBehaviour
{

    [SerializeField] Animator Cam;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetTrigger("SleepTri");
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Sleep", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0){
            Cam.SetTrigger("NoSleep");
            GameObject.Find("Player").GetComponent<Animator>().SetBool("Sleep", false);
            Destroy(this.gameObject);
        }
    }
}
