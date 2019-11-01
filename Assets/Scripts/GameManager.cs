using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public int speed = 3;

    private Vector3 seaTarget;
    private Vector3 beachTarget;
    private bool shouldMoveCamera;
    private bool moveToSea;
    private bool moveToBeach;

    private void Start()
    {
        moveToSea = true;
        beachTarget = new Vector3(5.65f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        seaTarget = new Vector3(0f, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }

    private void Update()
    {
        if(shouldMoveCamera)
        {
            Debug.Log("inside if ");
            Debug.Log("moveToSea " + moveToSea);
            Debug.Log("moveTobech " + moveToBeach);
            float step = speed * Time.deltaTime;
            //shouldMoveCamera = false; // todo change this after arrive to position;!!!

            if (moveToSea)
            {
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, seaTarget, step);
                shouldMoveCamera = mainCamera.transform.position != seaTarget;
            }
            else if (moveToBeach)
            {
                //5.65
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, beachTarget, step);
                shouldMoveCamera = mainCamera.transform.position != beachTarget;
            }
        }

    }

    private void SwipeDetected(SwipeData swipeData)
    {
        Debug.Log("shouldMoveCamera: " + shouldMoveCamera);
        // check if camera on the beach && the player swipe right -> move camera to the sea
        if(swipeData.Direction == SwipeDirection.Right && moveToBeach)
        {
            shouldMoveCamera = true;
            moveToSea = true;
            moveToBeach = false;
        }
        // check if camera on the sea && the player swipe left -> move camera to beach
        else if (swipeData.Direction == SwipeDirection.Left && moveToSea)
        {
            shouldMoveCamera = true;
            moveToSea = false;
            moveToBeach = true;
        }
    }

    private void OnEnable()
    {
        SwipeDetector.OnSwipe += SwipeDetected;
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= SwipeDetected;
    }
}
