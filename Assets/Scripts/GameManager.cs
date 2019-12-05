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
    public int life_scoreToUploadPerTime;
    public int fun_scoreToUploadPerTime;
    public int fun_ScoreBonusLevel;
    public int speed = 3;
    public int maxLifeValue = 100;
    public int maxFunValue = 100;

    private Animator anim;
    private Vector3 seaTarget;
    private Vector3 beachTarget;
    private Vector3 findEffiTarget;
    private bool shouldMoveCamera;
    private bool moveToSea;
    private bool moveToBeach;
    private bool moveToFindEffi;

    private void OnEnable()
    {
        SwipeDetector.OnSwipe += SwipeDetected;
        //KidPackController.OnLifeOver += DownloadScore;
        BeachWalker.OnLifeOver += DownloadScore;
        BeachWalker.OnBeachWalkerClicked += UploadFunScore;
        BeachWalker.OnLostKidClicked += LostKidClicked;
        LostKid.OnLostKidFound += LostKidFound;
        EffiManager.OnLostkidOver += LostKidOver;
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= SwipeDetected;
        //KidPackController.OnLifeOver -= DownloadScore;
        BeachWalker.OnLifeOver -= DownloadScore;
        BeachWalker.OnBeachWalkerClicked -= UploadFunScore;
        BeachWalker.OnLostKidClicked -= LostKidClicked;
        LostKid.OnLostKidFound -= LostKidFound;
        EffiManager.OnLostkidOver -= LostKidOver;
    }

    private void Start()
    {
        moveToSea = true;
        beachTarget = new Vector3(11.28f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        seaTarget = new Vector3(-0.09f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        findEffiTarget = new Vector3(11.28f, 11.0f, mainCamera.transform.position.z);
        scoreToDownloadPerTime = 10;
        fun_scoreToUploadPerTime = 10;
        life_scoreToUploadPerTime = 20;
        lifeSlider.maxValue = maxLifeValue;
        anim = lifeSlider.GetComponent<Animator>();
    }

    private void Update()
    {
        if(lifeSlider.value <= 0)
        {
            Debug.Log("GAME OVER! ");
        }

        bool isFunSliderFull = funSlider.value >= funSlider.maxValue;
        if (isFunSliderFull)
        {
            lifeSlider.value += life_scoreToUploadPerTime;
            funSlider.value = 0;
        }

        if(shouldMoveCamera)
        {
            float step = speed * Time.deltaTime;

            if(moveToFindEffi)
            {
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, findEffiTarget, step);
                shouldMoveCamera = mainCamera.transform.position != findEffiTarget;
            }
            else
            {
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

    }

    private void LostKidClicked()
    {
        moveToFindEffi = true;
        shouldMoveCamera = true;
        moveToBeach = false;
        moveToSea = false;
    }

    private void LostKidFound()
    {
        moveToFindEffi = false;
        shouldMoveCamera = true;
        moveToBeach = true;

        funSlider.value += fun_ScoreBonusLevel;
    }

    private void LostKidOver()
    {
        moveToFindEffi = false;
        shouldMoveCamera = true;
        moveToBeach = true;

        // TODO add losing sounds or somthing like that...
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
        anim.SetBool("isValueChanged", true);
        Debug.Log("Download Score");
    }

    private void UploadFunScore()
    {
        funSlider.value += fun_scoreToUploadPerTime;
    }


}
