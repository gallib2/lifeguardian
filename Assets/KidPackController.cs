using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidPackController : MonoBehaviour
{
    public bool isInTheWater;
    //public Vector3 position;
    //public Vector3 initPosition {get;set;};
    public Vector3 TargetPosition { get; set; }
    public Vector3 InitPosition { get; set; }
    public int TimeInTheWater { get; set; }
    public int TimeInOutsideWater { get; set; }
    public bool ArrivedWaterPosition { get; set; }
    public bool ArrivedOutsidePosition { get; set; }

    private TimerHelper timer;
    public float speed = 1f; // todo
    private bool continueCheckTimeToExit = true;
    private bool continueCheckTimeToEnter = true;

    // Start is called before the first frame update
    void Start()
    {
        timer = new TimerHelper();
    }

    private void FixedUpdate()
    {
        bool needToEnterWater = continueCheckTimeToEnter && (int)timer.Get() % TimeInOutsideWater == 0;

        if (!isInTheWater)
        {
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
        else
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
    }

    public bool MoveObject(Vector3 target)
    {
        float step = speed * Time.deltaTime;
        bool arrivePosition = false;
        ArrivedOutsidePosition = false;
        ArrivedWaterPosition = false;


        transform.position = Vector2.MoveTowards(transform.position, target, step);
        GetComponentInChildren<CharacterMovement>().ArrivedPatrolPosition = false;
        arrivePosition = transform.position == target;

        if (arrivePosition)
        {
            isInTheWater = !isInTheWater;
            GetComponentInChildren<CharacterMovement>().ArrivedPatrolPosition = true;
        }

        return arrivePosition;

    }
}
