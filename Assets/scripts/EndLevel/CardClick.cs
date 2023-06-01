using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CardClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string CardType;
    public string NextScene;
    public int CardNb;
    [SerializeField] Sprite Jump_image;
    [SerializeField] Sprite Slam_image;
    [SerializeField] Sprite Run_image;

    bool Pressed = false;

    private void Update()
    {
        if(Input.GetJoystickNames().Length > 0 && CardNb == 1 && Input.GetButtonDown("Fire1"))
        {
            GameObject.Find("ENDLEVEL").GetComponent<EndLevelManager>().Confirm.SetActive(true);
            ShowImage();
            Pressed = true;


        } else if (Input.GetJoystickNames().Length > 0 && CardNb == 2 && Input.GetButtonDown("Fire2"))
        {
            GameObject.Find("ENDLEVEL").GetComponent<EndLevelManager>().Confirm.SetActive(true);
            ShowImage();
            Pressed = true;
        }

        if(Input.GetJoystickNames().Length > 0 && Input.GetButtonDown("Jump") && Pressed)
        {
            OnClick();
        }

    }

    void ShowImage()
    {
        GameObject.Find("lvlimage").GetComponent<Image>().color = new Color32(255, 255, 225, 255);

        if (CardType == "Double_Jump")
        {
            GameObject.Find("lvlimage").GetComponent<Image>().sprite = Jump_image;
            GameObject.Find("lvlname").GetComponent<TextMeshProUGUI>().text = "Trial of height";
        }
        else if (CardType == "Ennemy_Slam")
        {
            GameObject.Find("lvlimage").GetComponent<Image>().sprite = Slam_image;
            GameObject.Find("lvlname").GetComponent<TextMeshProUGUI>().text = "Trial of stairs";
        }
        else if (CardType == "Run")
        {
            GameObject.Find("lvlimage").GetComponent<Image>().sprite = Run_image;
            GameObject.Find("lvlname").GetComponent<TextMeshProUGUI>().text = "Trial of endurance";
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        ShowImage();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (Input.GetJoystickNames().Length == 0)
        {
            GameObject.Find("lvlimage").GetComponent<Image>().color = new Color32(255,255,225,0);
            GameObject.Find("lvlname").GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void OnClick() {

        if (Input.GetJoystickNames().Length > 0)
        {
            Destroy(GameObject.Find("ENDLEVEL").GetComponent<EndLevelManager>().RB);
            Destroy(GameObject.Find("ENDLEVEL").GetComponent<EndLevelManager>().LB);
        }

        GameObject.Find("ENDLEVEL").GetComponent<EndLevelManager>().nextlvl = CardType;
        GameObject.Find("ENDLEVEL").GetComponent<EndLevelManager>().Progress();

        for (int i = 1; i <= 2; i++)
        {
            Destroy(GameObject.Find("CardEnd_" + i));
        }
    }

    IEnumerator Wait(float time)
    {
        if (CardType == "Double_Jump") {
            SceneManager.LoadScene("JUMP_Level");
        } else if (CardType == "Ennemy_Slam") {
            SceneManager.LoadScene("SLAM_Level");
        } else if (CardType == "Run") {
            SceneManager.LoadScene("RUN_Level");
        }
        yield return null;
    }

}
