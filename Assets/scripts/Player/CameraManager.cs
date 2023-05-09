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
        float Velo = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x/5;
        Vector3 targetPosition = new Vector3(playerRef.transform.position.x + Velo, playerRef.transform.position.y, -10);
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition, ref refVelocity, smoothTime);
    }
}