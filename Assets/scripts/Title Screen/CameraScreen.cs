using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreen : MonoBehaviour
{

    public bool IsCine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCine)
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.Find("Trcamera").transform.position, 0.0008f);
        }
    }
}
