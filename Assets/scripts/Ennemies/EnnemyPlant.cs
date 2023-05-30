using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPlant : MonoBehaviour
{

    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite ClosedSprite;
    [SerializeField] GameObject Damage;
    bool ispremierforme;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeState(2f));
        ispremierforme = true;
    }

    IEnumerator ChangeState(float time) {
        float timeElapsed = time;
        while (timeElapsed > 0) {
            timeElapsed -= Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger("Switch");
        ispremierforme = !ispremierforme;
        StartCoroutine(ChangeState(2f));
    }



    // Update is called once per frame
    void Update()
    {
        if(ispremierforme)
        {
            GetComponent<SpriteRenderer>().sprite = OpenSprite;
            GetComponent<BoxCollider2D>().enabled = false;
            Damage.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = ClosedSprite;
            GetComponent<BoxCollider2D>().enabled = true;
            Damage.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
