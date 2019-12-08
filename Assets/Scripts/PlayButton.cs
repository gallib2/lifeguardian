using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public Transform target;
    public float speed;
    bool toStartMove;


    // Update is called once per frame
    void Update()
    {
        if(toStartMove)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            Debug.Log("transform.position: " + transform.position);
        }
    }

    public void OnButtonClick()
    {
        toStartMove = true;
    }
}
