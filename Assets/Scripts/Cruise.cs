using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cruise : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public float speed;

    private Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = target1;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacterTowards(currentTarget);
        bool isArrivedTarget = transform.position == currentTarget.position;

        if (isArrivedTarget)
        {
            if(currentTarget == target1)
            {
                currentTarget = target2;
                transform.eulerAngles = new Vector3(0, -180, 0);
            } 
            else
            {
                currentTarget = target1;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void MoveCharacterTowards(Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}
