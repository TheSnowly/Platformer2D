using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CineScreen : MonoBehaviour
{

    public bool Switch;
    Animator G;
    [SerializeField] GameObject Gritta;

    // Start is called before the first frame update
    void Start()
    {
        G = Gritta.GetComponent<Animator>();
        Switch = false;
    }

    IEnumerator CineManager()
    {
        yield return new WaitForSeconds(7f);
        Gritta.SetActive(true);
        G.SetTrigger("Move");
        yield return new WaitForSeconds(1f);
        G.SetBool("Laugh", true);
        yield return new WaitForSeconds(2f);
        G.SetBool("Laugh", false);
        yield return new WaitForSeconds(2f);
        G.SetTrigger("Move");
        yield return new WaitForSeconds(1.15f);
        GameObject.Find("ZzzipoSleep").GetComponent<Animator>().SetBool("Bouge", true);
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("ZzzipoSleep").GetComponent<Animator>().SetBool("Bouge", false);
        yield return new WaitForSeconds(2f);
        GameObject.Find("Square").GetComponent<Animator>().SetTrigger("Fondu");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TUTO");
    }

    // Update is called once per frame
    void Update()
    {
        if(Switch == true)
        {
            Switch = false;
            StartCoroutine(CineManager());
        }
    }
}
