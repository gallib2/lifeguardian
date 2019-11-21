using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeachWalker : MonoBehaviour
{
    public static event Action OnBeachWalkerClicked;
    public static event Action OnBeachWalkerOut;

    private AudioSource audioSource;
    public List<AudioClip> audioClips;

    public Transform targetOnBeach;
    public Transform targetOut;
    public float speed = 0.3f;
    public int timeToStayOnBeachTarget;
    public HealthBar healthBar;

    private TimerHelper timer;
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
        audioSource = GetComponent<AudioSource>();
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

                moveOutTarget = (int)timer.Get() > 0 && (int)timer.Get() % timeToStayOnBeachTarget == 0;
            }
        }

        if(moveOutTarget)
        {
            moveToBeachTarget = false;
            MoveCharacterTowards(targetOut);

            if (transform.position == targetOut.position)
            {
                OnBeachWalkerOut?.Invoke();
            }
        }
    }

    private void SpeakWithBeachWalker()
    {
        int index = UnityEngine.Random.Range(0, audioClips.Count-1);
        moveOutTarget = false;

        audioSource.PlayOneShot(audioClips[index]);

        //moveOutTarget = true;
    }

    private void OnMouseDown()
    {
        // todo call function that: moveOutTarget = false; stay to "speak" for few seconds and then moveout = true;
        SpeakWithBeachWalker();
        healthBar.OnStopDownloadHealth();
        OnBeachWalkerClicked?.Invoke();
    }

    private void MoveCharacterTowards(Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}
