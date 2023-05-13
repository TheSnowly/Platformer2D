using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTransi : MonoBehaviour
{
    void NextS() {
        SceneManager.LoadScene("First_Level");
    }
}
