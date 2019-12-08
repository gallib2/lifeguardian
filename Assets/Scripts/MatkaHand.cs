using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatkaHand : MonoBehaviour
{
    public float speed;
    public Transform handMatkaTarget1;
    public Transform handMatkaTarget2;
    public bool toMoveOutHandMatka;

    public bool IsArrivedPosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toMoveOutHandMatka)
        {
            MoveCharacterTowards(handMatkaTarget1);
            IsArrivedPosition = transform.position == handMatkaTarget1.position;
            if (IsArrivedPosition)
            {
                toMoveOutHandMatka = false;
            }
        }
        else
        {
            MoveCharacterTowards(handMatkaTarget2);
            IsArrivedPosition = false;
            //if (transform.position == handMatkaTarget2.position)
            //{
            //    IsArrivedPosition = false;
            //}
        }
    }

    public void StartMoveHand()
    {
        toMoveOutHandMatka = true;
    }

    private void MoveCharacterTowards(Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}
