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

    public AudioClip backgroundAudio;
    public AudioClip successAudio;
    public AudioClip failedAudio;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(toStartTimer)
        {
            int remainTime = timeToFind - (int)timer.Get();
            textTimer.text = remainTime.ToString();

            if(remainTime <= 4)
            {
                audioSource.volume -= 0.01f;
            }

            if(remainTime == 3)
            {
                audioSource.Stop();
                audioSource.volume = 1f;
                audioSource.PlayOneShot(failedAudio);

            }

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
        audioSource.PlayOneShot(backgroundAudio);
        timer.Reset();
    }

    private void LostKidFound()
    {
        ResetSettings();
        audioSource.PlayOneShot(successAudio);
    }

    private void ResetSettings()
    {
        toStartTimer = false;
        textTimer.text = string.Empty;
        audioSource.Stop();
        audioSource.volume = 1f;
    }
}
