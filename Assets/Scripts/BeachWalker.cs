using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeachWalker : MonoBehaviour
{
    public static event Action OnBeachWalkerClicked;
    public static event Action OnBeachWalkerOut;

    public static event Action OnLifeOver;

    public CharacterType characterType;

    private AudioSource audioSource;
    public List<AudioClip> audioClips;

    public float speed = 0.3f;
    public HealthBar healthBar;

    private TimerHelper timer;
    private bool isArriveClickPosition;

    /// <summary>
    /// /Start with params
    /// </summary>
    public Transform targetOnBeach;
    public Transform targetOut;
    private Transform currentTarget;

    public int timeToStayOnBeachTarget;
    private bool toResetTimer;
    private bool moveToBeachTarget;
    private bool moveOutTarget;
    private bool isArriveBeachTargetPosition;
    // END

    /// <summary>
    /// Sea - deep water params
    /// </summary>
    public Transform targetSecondOnWater;

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
        audioSource = GetComponent<AudioSource>();
        currentTarget = targetOnBeach;
    }

    // Update is called once per frame
    void Update()
    {
        if(characterType == CharacterType.beach_start_with)
        {
            BeachWalkers();
        }

        if(characterType == CharacterType.Sea_deep_water)
        {
            SeaDeepWaterChar();
        }
    }

    private void SeaDeepWaterChar()
    {
        if (healthBar.CurrentSliderValue <= 0)
        {
            OnLifeOver?.Invoke();
            //OnBeachWalkerOut?.Invoke();
            //Destroy(gameObject);
        }

        if (moveToBeachTarget)
        {
            MoveCharacterTowards(currentTarget);
            isArriveBeachTargetPosition = transform.position == currentTarget.position;
            if (isArriveBeachTargetPosition)
            {
                currentTarget = currentTarget == targetOnBeach ? targetSecondOnWater : targetOnBeach;
            }

            moveOutTarget = (int)timer.Get() > 0 && (int)timer.Get() % timeToStayOnBeachTarget == 0;
        }

        if (moveOutTarget)
        {
            moveToBeachTarget = false;
            MoveCharacterTowards(targetOut);
            isArriveBeachTargetPosition = false;

            if (transform.position == targetOut.position)
            {
                moveOutTarget = false;
                healthBar.OnStartDownloadHealth();
                isArriveClickPosition = true;
            }
        }
    }

    private void SeaDeepWaterCharacterClicked()
    {
        // todo check if the life slider > 0
        if(healthBar.CurrentSliderValue > 0)
        {
            Debug.Log("arrive SeaDeepWaterCharacterClicked");
            moveToBeachTarget = true;
            timer.Reset();
        }
    }

    private void BeachWalkers()
    {
        if (moveToBeachTarget)
        {
            MoveCharacterTowards(targetOnBeach);
            isArriveClickPosition = transform.position == targetOnBeach.position;
            if (isArriveClickPosition)
            {
                if (toResetTimer)
                {
                    healthBar.OnStartDownloadHealth();
                    timer.Reset();
                    toResetTimer = false;
                }

                moveOutTarget = (int)timer.Get() > 0 && (int)timer.Get() % timeToStayOnBeachTarget == 0;
            }
        }

        if (moveOutTarget)
        {
            moveToBeachTarget = false;
            MoveCharacterTowards(targetOut);
            isArriveClickPosition = false;

            if (transform.position == targetOut.position)
            {
                OnBeachWalkerOut?.Invoke();
            }
        }
    }



    private void SpeakWithBeachWalker()
    {
        int index = UnityEngine.Random.Range(0, audioClips.Count);
        moveOutTarget = false;

        audioSource.PlayOneShot(audioClips[index]);

        //moveOutTarget = true;
    }

    private void OnMouseDown()
    {
        // todo call function that: moveOutTarget = false; stay to "speak" for few seconds and then moveout = true;
        if(isArriveClickPosition)
        {
            SpeakWithBeachWalker();
            healthBar.OnStopDownloadHealth();
            OnBeachWalkerClicked?.Invoke();

            if(characterType == CharacterType.Sea_deep_water)
            {
                SeaDeepWaterCharacterClicked();
            }
        }
    }

    private void MoveCharacterTowards(Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}

public enum CharacterType
{
    Sea_shallow_water,
    Sea_deep_water,
    Sea_rider,
    beach_start_with
}

