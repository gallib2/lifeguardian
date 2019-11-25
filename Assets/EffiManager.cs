using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffiManager : MonoBehaviour
{
    public Text textTimer;
    public int timeToFind;
    public float minXPosition;
    public float maxXPosition;
    public float minYPosition;
    public float maxYPosition;

    private TimerHelper timer;
    private bool toStartTimer;

    private void OnEnable()
    {
        BeachWalker.OnLostKidClicked += LostKidClicked;
        LostKid.OnLostKidFound += LostKidFound;
    }

    private void OnDisable()
    {
        BeachWalker.OnLostKidClicked -= LostKidClicked;
        LostKid.OnLostKidFound -= LostKidFound;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = new TimerHelper();
        textTimer.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if(toStartTimer)
        {
            int remainTime = timeToFind - (int)timer.Get();
            textTimer.text = remainTime.ToString();
        }
    }

    private void LostKidClicked()
    {
        toStartTimer = true;
        timer.Reset();
    }

    private void LostKidFound()
    {
        toStartTimer = false;
        textTimer.text = string.Empty;
    }
}
