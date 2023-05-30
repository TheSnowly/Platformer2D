using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    static float time = 180;
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
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay/60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
