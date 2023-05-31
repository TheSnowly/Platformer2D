using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerManagerTuto : MonoBehaviour
{
    //Controller
    [SerializeField] GameObject Joystick;
    [SerializeField] GameObject A1;
    [SerializeField] GameObject A2;
    [SerializeField] GameObject LB1;
    [SerializeField] GameObject LB2;
    [SerializeField] GameObject LB3;

    //Clavier
    [SerializeField] GameObject AD;
    [SerializeField] GameObject Space1;
    [SerializeField] GameObject Space2;
    [SerializeField] GameObject LC1;
    [SerializeField] GameObject LC2;
    [SerializeField] GameObject LC3;

    // Start is called before the first frame update
    void Start()
    {
        if(Input.GetJoystickNames().Length > 0)
        {
            Joystick.SetActive(true);
            A1.SetActive(true);
            A2.SetActive(true);
            GameObject.Find("TextJump").GetComponent<TextMeshProUGUI>().text = "BUTTON A";
            GameObject.Find("TextUse").GetComponent<TextMeshProUGUI>().text = "Use LEFT BUMPER to use the card ability";
            LB1.SetActive(true);
            LB2.SetActive(true);
            LB3.SetActive(true);
        } else
        {
            AD.SetActive(true);
            Space1.SetActive(true);
            Space2.SetActive(true);
            GameObject.Find("TextJump").GetComponent<TextMeshProUGUI>().text = "SPACE BAR";
            GameObject.Find("TextUse").GetComponent<TextMeshProUGUI>().text = "Use LEFT CLICK to use the card ability";
            LC1.SetActive(true);
            LC2.SetActive(true);
            LC3.SetActive(true);
        }
    }
}
