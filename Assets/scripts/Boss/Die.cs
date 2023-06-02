using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait(0.2f));
    }

    IEnumerator Wait(float time)
    {
        GameObject.Find("Player").GetComponent<CharacterController>().NoiseSource.GenerateImpulse();
        yield return new WaitForSeconds(time);
        GameObject.Find("BossManager").GetComponent<AudioSource>().pitch = GameObject.Find("BossManager").GetComponent<BossManager>().Pitch;
        GameObject.Find("BossManager").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("BossManager").GetComponent<BossManager>().OrchestraHit);
        GameObject.Find("BossManager").GetComponent<BossManager>().Pitch = GameObject.Find("BossManager").GetComponent<BossManager>().Pitch + 0.2f;
        GameObject.Find("BossManager").GetComponent<BossManager>().NbCardThrow += 1;
        int c = Random.Range(1, 4);
        while (c == GameObject.Find("BossManager").GetComponent<BossManager>().precedent)
        {
            c = Random.Range(1, 4);
        }
            if (c==1)
            {
                GameObject.Find("Boss").GetComponent<Animator>().SetTrigger("HitU");
            } else if (c==2)
            {
                GameObject.Find("Boss").GetComponent<Animator>().SetTrigger("HitR");
            } else if (c == 3)
            {
                GameObject.Find("Boss").GetComponent<Animator>().SetTrigger("HitL");
            }
        GameObject.Find("BossManager").GetComponent<BossManager>().precedent = c;
        GameObject.Find("BossManager").GetComponent<BossManager>().time = 3;
        Destroy(this.gameObject);
    }
}
