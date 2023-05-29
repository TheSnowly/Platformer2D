using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamEffect : MonoBehaviour
{
    [SerializeField] GameObject Remanant;

    SpriteRenderer Sr;
    public bool UsingEffect;
    float timer;
    [SerializeField] float Maxtimer;

    // Start is called before the first frame update
    void Start()
    {
        Sr = GetComponent<SpriteRenderer>();
        UsingEffect = false;
        timer = Maxtimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (UsingEffect == true)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timer = Maxtimer;
                GameObject Go = GameObject.Instantiate(Remanant, transform.position, Quaternion.identity);
                Go.GetComponent<SpriteRenderer>().sprite = Sr.sprite;
                Go.GetComponent<SpriteRenderer>().flipX = Sr.flipX;
            }
        }
    }
}
