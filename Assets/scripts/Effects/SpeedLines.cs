using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLines : MonoBehaviour
{

    GameObject Player;
    ParticleSystem SpeedL;


    // Start is called before the first frame update
    void Start()
    {
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
        VeloLife.speedModifier = (Player.GetComponent<Rigidbody2D>().velocity.x/5) + 0.01f;

        if (Player.GetComponent<Rigidbody2D>().velocity.x <= 0)
        {
            GetComponent<ParticleSystemRenderer>().material.color = Color.Lerp(Color.white, new Color(255, 255, 255, 0), 0.5f);
        }
    }
}

