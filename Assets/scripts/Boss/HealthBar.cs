using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] CardManager CardManager;
    float Life;

    public void SetLife()
    {
        Life = 1f / CardManager.Deck.Count;
        Debug.Log(Life);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = Mathf.Lerp(GetComponent<Image>().fillAmount, Life * CardManager.Deck.Count, 0.05f);
    }
}
