using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject playerRef;
    Vector3 refVelocity = Vector3.zero;
    float smoothTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float VeloX = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x/5;
        float VeloY = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y/4;
        Vector3 targetPosition;

        if (VeloY >= 0 ) {
             targetPosition = new Vector3(playerRef.transform.position.x + VeloX, playerRef.transform.position.y + 4, -10);
        } else {
             targetPosition = new Vector3(playerRef.transform.position.x + VeloX, playerRef.transform.position.y + (4 - (float)Math.Sqrt(VeloY*VeloY)), -10);
        }
        if (GameObject.Find("Player").GetComponent<PlayerDamage>().isDying == false)
        {
            gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition, ref refVelocity, smoothTime);
        }
    }
}