using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CineScreen : MonoBehaviour
{

    public bool Switch;
    Animator G;
    [SerializeField] GameObject Gritta;
    [SerializeField] AudioClip Whistle;
    [SerializeField] AudioClip Hit;

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
        Gritta.GetComponent<AudioSource>().PlayOneShot(Whistle);
        yield return new WaitForSeconds(1f);
        G.SetBool("Laugh", true);
        Gritta.GetComponent<GrittaLaugh>().IsLaughing = true;
        yield return new WaitForSeconds(2f);
        G.SetBool("Laugh", false);
        Gritta.GetComponent<GrittaLaugh>().IsLaughing = false;
        yield return new WaitForSeconds(2f);
        G.SetTrigger("Move");
        yield return new WaitForSeconds(1.15f);
        GameObject.Find("ZzzipoSleep").GetComponent<Animator>().SetBool("Bouge", true);
        Gritta.GetComponent<AudioSource>().PlayOneShot(Hit);
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
