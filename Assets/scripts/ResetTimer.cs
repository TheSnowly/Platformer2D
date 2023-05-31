using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimer : MonoBehaviour
{

    [SerializeField] Timer Timer;
    [SerializeField] EndLevelManager EndLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        Timer.time = 180;
        EndLevelManager.NbOfLevelPlayed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
