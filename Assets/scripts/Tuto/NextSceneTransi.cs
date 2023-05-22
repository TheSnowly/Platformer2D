using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTransi : MonoBehaviour
{

    [SerializeField] GameObject BG;

    void NextS() {
        SceneManager.LoadScene("First_Level");
    }

    void ShowBG() {
        BG.SetActive(true);
        GameObject.Find("Black").GetComponent<Animator>().SetTrigger("transi");
    }
}
