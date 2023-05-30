using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class Timer : MonoBehaviour
{

    static public float time = 180;
    public int timed;
    [SerializeField] TextMeshProUGUI timeText;
    public bool StopTime;

    // Start is called before the first frame update
    void Start()
    {
        timed = (int)time;
        StopTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(StopTime == false) {
            time -= Time.deltaTime;
        }
        DisplayTime(time);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay <= 0)
        {
            timeToDisplay = 0;
            GameObject.Find("Player").GetComponent<CharacterController>().NoiseSource.GenerateImpulse();
            GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Player").GetComponent<CapsuleCollider2D>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerDamage>().isDying = true;
            GameObject.Find("Player").GetComponent<CharacterController>().CanMove = false;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0;
            GameObject.Find("Player").GetComponent<Animator>().SetBool("Die", true);
            GameObject.Find("Player").GetComponent<PlayerDamage>().TimerGameOver();
            this.gameObject.SetActive(false);
        }

        float minutes = Mathf.FloorToInt(timeToDisplay/60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
