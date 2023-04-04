using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    [SerializeField] GameObject Player; 

    private float tParam;

    private Vector2 objectPosition;

    private float speedModifier;

    [SerializeField]
    private bool coroutineAllowed;

    [SerializeField]
    bool Restart;

    float random;

    float RestartPos;

    // Start is called before the first frame update
    void Start()
    {
        Restart = false;
        tParam = 0f;
        coroutineAllowed = true;
        random = UnityEngine.Random.Range(0.3f, 0.5f);
        speedModifier = random;
        RestartPos = Player.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        float e = Player.transform.position.y * 100;
        float d = Convert.ToInt32(e);
        float f = (float)d / 100;
        /*
        if (Restart && f == RestartPos)
        {
            Restart = false;
            coroutineAllowed = true;
        }
        */

        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute());
        }
    }

    private IEnumerator GoByTheRoute()
    {
        coroutineAllowed = false;

        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        Vector2 p0 = Player.transform.position;
        Vector2 p1 = new Vector2(Player.transform.position.x - 1.5f, Player.transform.position.y - 2f);
        Vector2 p2 = new Vector2(Player.transform.position.x - 6.5f, Player.transform.position.y + 2f);
        Vector2 p3 = new Vector2(Player.transform.position.x - 8f, Player.transform.position.y);

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        coroutineAllowed = true;

        //Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 10), ForceMode2D.Impulse);
        //Restart = true;

    }
}
