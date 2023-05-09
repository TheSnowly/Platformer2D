using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLines : MonoBehaviour
{

    GameObject Player;
    ParticleSystem SpeedL;
    float Alpha;


    // Start is called before the first frame update
    void Start()
    {
        Alpha = 255;
        Player = GameObject.Find("Player");
        SpeedL = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = SpeedL.main;
        var Emi = SpeedL.emission;
        var VeloLife = SpeedL.velocityOverLifetime;

        Emi.rateOverTime = Player.GetComponent<Rigidbody2D>().velocity.x/2;
        VeloLife.speedModifier = (Player.GetComponent<Rigidbody2D>().velocity.x/5) + 0.001f;

        if (Player.GetComponent<Rigidbody2D>().velocity.x <= 0)
        {
            Alpha -= 100 * Time.deltaTime;

        } else if (Player.GetComponent<Rigidbody2D>().velocity.x > 0) {
            Alpha += 100 * Time.deltaTime;
        } else {
            Alpha = 0;
        }
        Debug.Log(Alpha);
        Color Cur = new Color(255, 255, 255, Alpha);
        GetComponent<ParticleSystemRenderer>().material.color = Cur;
    }
}

