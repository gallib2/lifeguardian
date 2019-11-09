using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public Slider lifeSlider;
    public Slider funSlider;
    public int scoreToDownloadPerTime;
    public int speed = 3;
    public int maxLifeValue = 100;

    private Vector3 seaTarget;
    private Vector3 beachTarget;
    private bool shouldMoveCamera;
    private bool moveToSea;
    private bool moveToBeach;

    private void OnEnable()
    {
        SwipeDetector.OnSwipe += SwipeDetected;
        HealthBar.OnLifeOver += DownloadScore;
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= SwipeDetected;
        HealthBar.OnLifeOver -= DownloadScore;
    }

    private void Start()
    {
        moveToSea = true;
        beachTarget = new Vector3(5.65f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        seaTarget = new Vector3(0f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        scoreToDownloadPerTime = 10;
        lifeSlider.maxValue = maxLifeValue;
    }

    private void Update()
    {
        if(shouldMoveCamera)
        {
            float step = speed * Time.deltaTime;

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

    private void DownloadScore()
    {
        lifeSlider.value -= scoreToDownloadPerTime;
    }


}
