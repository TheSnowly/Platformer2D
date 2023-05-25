using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        GameObject.Find("CineManager").GetComponent<CineScreen>().Switch = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScreen>().IsCine = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = false;
        GameObject.Find("Clilc").GetComponent<Animator>().SetTrigger("fondu");
        GameObject.Find("Logo").GetComponent<Animator>().SetTrigger("Transi");
        GameObject.Find("Borders").SetActive(false);
        GameObject.Find("Borders2").SetActive(false);
        GameObject.Find("Button").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
