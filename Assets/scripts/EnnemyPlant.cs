using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPlant : MonoBehaviour
{

    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite ClosedSprite;
    [SerializeField] GameObject Damage;
    string CurrentState;
    bool CanCouroutine;
    bool ispremierforme;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = "Open";
        StartCoroutine(ChangeState(4f));
        ispremierforme = true;
    }

    IEnumerator ChangeState(float time) {
        CanCouroutine = false;
        float timeElapsed = time;
        while (timeElapsed > 0) {
            timeElapsed -= Time.deltaTime;
            yield return null;
        }
        ispremierforme = !ispremierforme;
        StartCoroutine(ChangeState(4f));
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
