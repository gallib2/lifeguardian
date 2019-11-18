using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static event Action OnMoveToDanger;

    public List<AudioClip> audioOnSafePlace;
    public List<AudioClip> audioOnDangerPlace;
    public Transform groundDetection;
    public float speed = 0.5f;
    public float distance;
    //public int minTimeToStartMoveToDanger;
    //public int maxTimeToStartMoveToDanger;

    //private int moveToDangerPerXSeconds;
    private AudioSource audioSource;
    private TimerHelper timer;
    private bool movingRight = true;
    //private bool moveToDanger = false;
    //private bool continueCheckTime = true;
    //private Vector3 dangerTarget;
    //private Vector3 initPosition;
    private bool isInDangerZone = false;

    private Vector3 positionToEnterWater;
    private bool needToEnterWater; // false when arrive position
    private bool needToExitWater; // false when arrive position

    public Vector3 InitPosition { get; set; }
    public bool ArrivedPatrolPosition { get; set; }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //dangerTarget = new Vector3(2f, 0, 0); // todo kidItem.dangerZone.transform.position; (GetComponent<Item>().item as CharacterObject;)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "flagsZone_right")
        {
            isInDangerZone = true;
            GetComponentInParent<KidPackController>().IsInDangerZone = true;
            OnMoveToDanger?.Invoke();
            GetComponent<Item>().healthBar.OnStartDownloadHealth();
            //healthBar.OnStartDownloadHealth();
            //GetComponentInChildren<HealthBar>().OnStartDownloadHealth();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "flagsZone_right")
        {
            //moveToDangerPerXSeconds = UnityEngine.Random.Range(5, 15);
            isInDangerZone = false;
            KidPackController kidPackController = GetComponentInParent<KidPackController>();
            if (kidPackController)
            {
                kidPackController.IsInDangerZone = false;
                kidPackController.MoveToDangerPerXSeconds = UnityEngine.Random.Range(5, 15);
            }
        }
    }

    public void Patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public void OnCharcterDead()
    {
        Destroy(gameObject);
    }

}
