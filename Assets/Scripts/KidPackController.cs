using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidPackController : MonoBehaviour
{
    public static event Action OnLifeOver;

    public bool isInTheWater;
    public List<AudioClip> audioOnSafePlace;
    public List<AudioClip> audioOnDangerPlace;

    public Vector3 TargetPosition { get; set; }
    public Vector3 InitPosition { get; set; }
    public int TimeInTheWater { get; set; }
    public int TimeInOutsideWater { get; set; }
    public bool ArrivedWaterPosition { get; set; }
    public bool ArrivedOutsidePosition { get; set; }

    public bool IsInDangerZone { get; set; }

    public int MoveToDangerPerXSeconds { get; set; }
    public int minTimeToStartMoveToDanger;
    public int maxTimeToStartMoveToDanger;
    private bool moveToDanger = false;
    public Vector3 DangerTarget { get; set; }

    private AudioSource audioSource;
    private TimerHelper timer;
    public float speed = 0.2f; // todo
    private bool continueCheckTimeToExit = true;
    private bool continueCheckTimeToEnter = true;
    private bool danger_continueCheckTime = true;

    private Slider lifeSlider;


    // Start is called before the first frame update
    void Start()
    {
        timer = new TimerHelper();
        audioSource = GetComponent<AudioSource>();
        lifeSlider = GetComponentInChildren<Slider>();
        //DangerTarget = new Vector3(2f, 0, 0); // todo kidItem.dangerZone.transform.position; (GetComponent<Item>().item as CharacterObject;)
        //dangerTarget = new Vector3(2f, 0, 0); // todo kidItem.dangerZone.transform.position; (GetComponent<Item>().item as CharacterObject;)
        minTimeToStartMoveToDanger = 8;
        maxTimeToStartMoveToDanger = 25;
        MoveToDangerPerXSeconds = UnityEngine.Random.Range(minTimeToStartMoveToDanger, maxTimeToStartMoveToDanger);
        Debug.Log("moveToDangerPerXSeconds: " + MoveToDangerPerXSeconds);
    }

    private void FixedUpdate()
    {
        if(lifeSlider.value <= 0)
        {
            OnLifeOver?.Invoke();
            Destroy(gameObject);
        }

        if(!IsInDangerZone)
        {
            if (!isInTheWater)
            {
                EnterToWater();
            }
            else
            {
                ExitWater();
            }
        }


        if (ArrivedWaterPosition)
        {
            moveToDanger = danger_continueCheckTime && (int)timer.Get() > 0 && ((int)timer.Get() % MoveToDangerPerXSeconds) == 0;
            if (!danger_continueCheckTime || moveToDanger)
            {
                danger_continueCheckTime = false;
                MoveCharacterTowards(transform, DangerTarget);
            }
            else
            {
                GetComponentInChildren<CharacterMovement>().Patrol();
            }
        }
    }

    private void MoveCharacterTowards(Transform _transform, Vector3 _target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(_transform.position, _target, step);
    }

    public void MoveToSafty()
    {
        // TODO the time that takes to return to safty is the time of the ui OR remove the ui for now
        if (IsInDangerZone)
        {
            int clipsSize = audioOnDangerPlace.Count - 1;
            int soundToPlay = UnityEngine.Random.Range(0, clipsSize);

            audioSource.PlayOneShot(audioOnDangerPlace[soundToPlay]);
            danger_continueCheckTime = true;
            transform.position = TargetPosition; // todo maybe use the MoveCharacterTowards
        }
        else
        {
            int clipsSize = audioOnSafePlace.Count - 1;
            int soundToPlay = UnityEngine.Random.Range(0, clipsSize);

            audioSource.PlayOneShot(audioOnSafePlace[soundToPlay]);
        }
    }

    public void EnterToWater()
    {
        bool needToEnterWater = continueCheckTimeToEnter && (int)timer.Get() % TimeInOutsideWater == 0;
        bool canEnterWater = (!continueCheckTimeToEnter || needToEnterWater);
        //bool canEnterWater = (howManyInTheWater < maxPeopleInTheWater) && (!continueCheckTimeToEnter || needToEnterWater);

        if (canEnterWater)
        {
            // Enter the water
            Vector3 target = TargetPosition;
            continueCheckTimeToEnter = false;
            ArrivedWaterPosition = MoveObject(target);

            if (ArrivedWaterPosition)
            {
                //howManyInTheWater++;
                continueCheckTimeToExit = true;
            }
        }
    }

    private void ExitWater()
    {
        bool needToExitWater = continueCheckTimeToExit && (int)timer.Get() % TimeInTheWater == 0;

        if (!continueCheckTimeToExit || needToExitWater)
        {
            // Exit form water
            continueCheckTimeToExit = false;
            Vector3 target = InitPosition;

            ArrivedOutsidePosition = MoveObject(target);

            if (ArrivedOutsidePosition)
            {
                //howManyInTheWater--;
                continueCheckTimeToEnter = true;
            }
        }
    }

    public bool MoveObject(Vector3 target)
    {
        float step = speed * Time.deltaTime;
        bool arrivePosition = false;
        ArrivedOutsidePosition = false;
        ArrivedWaterPosition = false;


        transform.position = Vector2.MoveTowards(transform.position, target, step);
        //GetComponentInChildren<CharacterMovement>().ArrivedPatrolPosition = false;
        arrivePosition = transform.position == target;

        if (arrivePosition)
        {
            isInTheWater = !isInTheWater;
            //GetComponentInChildren<CharacterMovement>().ArrivedPatrolPosition = true;
        }

        return arrivePosition;

    }
}
