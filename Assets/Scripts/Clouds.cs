using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    public float speed;
    public Transform targetOutside;

    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = new Vector3(-16.66f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacterTowards(targetOutside);

        if (transform.position == targetOutside.position)
        {
            transform.position = new Vector3(-16.66f, 0f, 0f); ;
        }

    }

    private void MoveCharacterTowards(Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }










    //public float speed;
    //public GameObject cloudsBlock_1;
    //public GameObject cloudsBlock_2;
    //public Transform targetOutside;

    //private Vector3 initPosition;
    //private Transform currentTarget;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    currentTarget = targetOutside;
    //    initPosition = new Vector3(0f, 0f, 0f);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    MoveCharacterTowards(cloudsBlock_1, currentTarget);
    //    MoveCharacterTowards(cloudsBlock_2, currentTarget);

    //    if(cloudsBlock_1.transform.position == targetOutside.position)
    //    {
    //        cloudsBlock_1.transform.position = initPosition;
    //    }

    //}

    //private void MoveCharacterTowards(GameObject cloudBlock ,Transform target)
    //{
    //    float step = speed * Time.deltaTime;
    //    transform.position = Vector2.MoveTowards(cloudBlock.transform.position, target.position, step);
    //}
}
