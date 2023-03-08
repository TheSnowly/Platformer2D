using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform position1, position2;
    private float _speed = 3.0f;
    [SerializeField] private bool _switch = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (_switch == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, position1.position, _speed * Time.deltaTime);
        }
        else if (_switch == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, position2.position, _speed * Time.deltaTime);
        }

        if (transform.position == position1.position)
        {
            _switch = true;
        }
        else if (transform.position == position2.position)
        {
            _switch = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
