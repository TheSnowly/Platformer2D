using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Timee : MonoBehaviour
{

    [SerializeField] Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        float timeCleared = 120 - timer.timed;
        GameObject.Find("TimeC").GetComponent<TextMeshProUGUI>().text = "Cleared in: " + string.Format("{0:00}:{1:00}", timeCleared/60, timeCleared % 60); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
