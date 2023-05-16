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
        var VeloShape = SpeedL.shape;

        main.startSizeY = 3 + Player.GetComponent<Rigidbody2D>().velocity.x/10;
        Emi.rateOverTime = Player.GetComponent<Rigidbody2D>().velocity.x/2;

        if (Player.GetComponent<Rigidbody2D>().velocity.x <= 0)
        {
            GetComponent<ParticleSystemRenderer>().material.color = Color.Lerp(GetComponent<ParticleSystemRenderer>().material.color, new Color(255, 255, 255, 0), 0.01f);
            VeloShape.radius = Mathf.Lerp(VeloShape.radius, 15, 0.01f);
            main.simulationSpeed = Mathf.Lerp(main.simulationSpeed, 0.01f, 0.01f);

        } else
        {
            GetComponent<ParticleSystemRenderer>().material.color = Color.Lerp(GetComponent<ParticleSystemRenderer>().material.color, new Color(255, 255, 255, 50), 0.01f);
            VeloShape.radius = Mathf.Lerp(VeloShape.radius, 10, 0.0001f * Player.GetComponent<Rigidbody2D>().velocity.x);
            main.simulationSpeed = Mathf.Lerp(main.simulationSpeed, 1 + (Player.GetComponent<Rigidbody2D>().velocity.x/4), 0.001f);
        }
    }
}

