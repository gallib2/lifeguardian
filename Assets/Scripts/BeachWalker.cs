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
    public int maxHealth;

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

    /// <summary>
    /// Sea - Rider water params
    /// </summary>
    public Sprite sprite;
    public Vector3 spriteScale;
    private bool isStandingOnSurf = true;
    private bool isAlreadySwitchSprite = false;

    private void Awake()
    {
        healthBar.InitialHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveToBeachTarget = true;
        toResetTimer = true;
        timer = new TimerHelper();
        audioSource = GetComponent<AudioSource>();
        currentTarget = targetOnBeach;
        spriteScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(characterType == CharacterType.beach_start_with)
        {
            BeachWalkers();
        }

        bool isInSea = characterType == CharacterType.Sea_deep_water || characterType == CharacterType.Sea_shallow_water || characterType == CharacterType.Sea_rider;

        if (isInSea)
        {
            SeaDeepWaterChar();
        }
    }

    private void SeaDeepWaterChar()
    {
        if (healthBar.CurrentSliderValue <= 0)
        {
            OnLifeOver?.Invoke();
            GetComponentInParent<SpawnWalkers>().LifeOver();
        }

        if (moveToBeachTarget)
        {
            isAlreadySwitchSprite = false;
            MoveCharacterTowards(currentTarget);
            isArriveBeachTargetPosition = transform.position == currentTarget.position;
            if (isArriveBeachTargetPosition)
            {
                //currentTarget = currentTarget == targetOnBeach ? targetSecondOnWater : targetOnBeach;

                if(currentTarget == targetOnBeach)
                {
                    currentTarget = targetSecondOnWater;
                    if(characterType == CharacterType.Sea_deep_water)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    if(sprite != null && isStandingOnSurf)
                    {
                        //isStandingOnSurf = false;
                        SwitchCharacterSprite();
                    }
                } 
                else
                {
                    currentTarget = targetOnBeach;
                    if (characterType == CharacterType.Sea_deep_water)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                    }
                    if (sprite != null && !isStandingOnSurf)
                    {
                        //isStandingOnSurf = true;
                        SwitchCharacterSprite();
                    }
                }
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

            if (characterType == CharacterType.Sea_rider)
            {
                if (!isAlreadySwitchSprite && !isStandingOnSurf)
                {
                    isAlreadySwitchSprite = true;
                    //isStandingOnSurf = true;
                    SwitchCharacterSprite();
                }
            }
        }
    }

    private void SwitchCharacterSprite()
    {
        isStandingOnSurf = !isStandingOnSurf;
        Sprite tempSprite = GetComponent<SpriteRenderer>().sprite;
        Vector3 tempScale = transform.localScale;
        GetComponent<SpriteRenderer>().sprite = sprite;
        transform.localScale = spriteScale;
        spriteScale = tempScale;
        sprite = tempSprite;
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

    private void SeaRiderClicked()
    {
        SeaDeepWaterCharacterClicked();
        currentTarget = targetSecondOnWater;
        //isStandingOnSurf = false;
        SwitchCharacterSprite();
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

            if (characterType == CharacterType.Sea_deep_water || characterType == CharacterType.Sea_shallow_water)
            {
                SeaDeepWaterCharacterClicked();
            }

            if(characterType == CharacterType.Sea_rider)
            {
                SeaRiderClicked();
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

