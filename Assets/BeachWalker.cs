using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeachWalker : MonoBehaviour
{
    public static event Action OnBeachWalkerClicked;

    public Transform targetOnBeach;
    public Transform targetOut;
    public float speed = 0.3f;
    public int timeToStayOnBeachTarget;
    public HealthBar healthBar;

    private TimerHelper timer;
    private bool continueCheckTimeOnBeach;
    private bool toResetTimer;
    private bool moveToBeachTarget;
    private bool moveOutTarget;

    private void Awake()
    {
        healthBar.InitialHealth = timeToStayOnBeachTarget;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveToBeachTarget = true;
        toResetTimer = true;
        timer = new TimerHelper();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveToBeachTarget)
        {
            MoveCharacterTowards(targetOnBeach);
            if(transform.position == targetOnBeach.position)
            {
                if (toResetTimer)
                {
                    healthBar.OnStartDownloadHealth();
                    timer.Reset();
                    toResetTimer = false;
                }
                //continueCheckTimeOnBeach = true;
                moveOutTarget = (int)timer.Get() > 0 && (int)timer.Get() % timeToStayOnBeachTarget == 0;
            }
        }

        if(moveOutTarget)
        {
            moveToBeachTarget = false;
            MoveCharacterTowards(targetOut);
        }
    }

    private void OnMouseDown()
    {
        healthBar.OnStopDownloadHealth();
        OnBeachWalkerClicked?.Invoke();
    }

    private void MoveCharacterTowards(Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}
