using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffiManager : MonoBehaviour
{
    public static event Action OnLostkidOver;

    public GameObject lostKid;
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

            if(remainTime == 0)
            {
                OnLostkidOver?.Invoke();
                ResetSettings();
            }
        }
    }

    private void LostKidClicked()
    {
        float chosenX = UnityEngine.Random.Range(minXPosition, maxXPosition);
        float chosenY = UnityEngine.Random.Range(minYPosition, maxYPosition);
        lostKid.transform.position = new Vector3(chosenX, chosenY, 0f);
        toStartTimer = true;
        timer.Reset();
    }

    private void LostKidFound()
    {
        ResetSettings();
    }

    private void ResetSettings()
    {
        toStartTimer = false;
        textTimer.text = string.Empty;
    }
}
