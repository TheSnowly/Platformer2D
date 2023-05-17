using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] GameObject playerRef;
    Vector3 refVelocity = Vector3.zero;
    float smoothTime = 0.4f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(playerRef.transform.position.x - 2, playerRef.transform.position.y + 2, 0);
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition, ref refVelocity, smoothTime);
    }
}
