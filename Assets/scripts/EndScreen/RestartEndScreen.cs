using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartEndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            GameObject.Find("Restart").GetComponent<TextMeshProUGUI>().text = "Restart";
            GameObject.Find("A").GetComponent<SpriteRenderer>().enabled = true;
        } else
        {
            GameObject.Find("Restart").GetComponent<TextMeshProUGUI>().text = "Click anywhere to restart";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetJoystickNames().Length > 0 && Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("TUTO");
        }

        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("TUTO");
        }
    }
}
