using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static event Action OnLifeOver;

    public float initialHealth;
    public int timeTo = 5;
    public Slider lifeSlider;
    public Transform target;
    private bool toStartDwonloadHelth;
    private bool toStopDwonloadHelth;
    private TimerHelper timer;

    private void OnEnable()
    {
        CharacterMovement.OnMoveToDanger += OnStartDownloadHealth;
        Item.OnItemClicked += OnStopDownloadHealth;
    }

    private void OnDisable()
    {
        CharacterMovement.OnMoveToDanger -= OnStartDownloadHealth;
        Item.OnItemClicked -= OnStopDownloadHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        timer = new TimerHelper();
        lifeSlider = GetComponent<Slider>();
        lifeSlider.maxValue = initialHealth;
    }

    void Update()
    {
        lifeSlider.transform.position = new Vector3(target.position.x, lifeSlider.transform.position.y, lifeSlider.transform.position.z);

        if(toStartDwonloadHelth && (int)timer.Get() > 0)
        {
            lifeSlider.value = initialHealth - (int)timer.Get();
            if(lifeSlider.value <= 0)
            {
                OnLifeOver.Invoke();
            }
        }
        else if(toStopDwonloadHelth)
        {
            lifeSlider.value = initialHealth;
            //StopDownloadHelth();
        }
    }

    private void OnStartDownloadHealth()
    {
        toStartDwonloadHelth = true;
        toStopDwonloadHelth = false;
        timer.Reset();
    }

    private void OnStopDownloadHealth(ItemObject item)
    {
        toStartDwonloadHelth = false;
        toStopDwonloadHelth = true;
    }

    private void StartDwonloadHelth()
    {
        //localScale.x -= healthDownloadPerSecond;
        //transform.localScale = localScale;
    }

    private void StopDownloadHelth()
    {

    }
}
