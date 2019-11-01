using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    public float distance;

    public Transform groundDetection;

    private CharacterObject kidItem;
    private TimerHelper timer;
    private bool movingRight = true;
    private bool moveToDanger = false;
    private bool continueCheckTime = true;
    private Vector3 target;
    private Vector3 initPosition;

    private void OnEnable()
    {
        Item.OnItemClicked += MoveToSafty;
    }

    private void OnDisable()
    {
        Item.OnItemClicked -= MoveToSafty;
    }


    private void Start()
    {
        kidItem = GetComponent<Item>().item as CharacterObject;
        target = new Vector3(2f, 0, 0); //kidItem.dangerZone.transform.position;
        timer = new TimerHelper();
        initPosition = transform.position;

    }

    private void Update()
    {
        moveToDanger = continueCheckTime && (int)timer.Get() > 0 && ((int)timer.Get() % 10) == 0;

        if(!continueCheckTime || moveToDanger)
        {
            float step = speed * Time.deltaTime;
            continueCheckTime = false;
            transform.position = Vector2.MoveTowards(transform.position, target, step); 
        } 
        else
        {
            Patrol();
        }
    }

    void MoveToSafty(ItemObject item)
    {
        //moveToDanger = false;
        // todo move item to regular position after X seconds.
        // can do it with variable and move to init position on update function.
        continueCheckTime = true;
        transform.position = initPosition;
    }

    private void Patrol()
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

}
