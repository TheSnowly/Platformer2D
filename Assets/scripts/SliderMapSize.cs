using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMapSize : MonoBehaviour
{

    GameObject Player;
    float MapSize;
    [SerializeField]
    Slider Slider;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        MapSize = Vector3.Distance(Player.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Slider.GetComponent<Slider>().value = 1 - (Vector3.Distance(Player.transform.position, transform.position)/MapSize);
        Mathf.Clamp(Slider.GetComponent<Slider>().value, 0, 1);
    }
}
