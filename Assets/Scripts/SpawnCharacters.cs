using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacters : MonoBehaviour
{
    public GameObject characterPrefub;
    public List<GameObject> characters;

    public List<int> timesInTheWater;
    public List<int> timesOutsideTheWater;
    public List<Vector3> positions;
    public List<Vector3> dangerTargets;

    public int maxPeopleInTheWater;
    private int howManyInTheWater;


    //private TimerHelper timer;
    private Vector3 lowerLeftPosition = new Vector3(-3f, -5f, 0);
    private Vector3 lowerRightPosition = new Vector3(0.6f, -5f, 0);
    private Vector3 defaultStartPosition = new Vector3(-3f, -5f, 0);

    void Start()
    {
        // -1.3
        // -2.7

        int arrSize = timesInTheWater.Count;
        characters = new List<GameObject>(arrSize);

        //Instantiate(characterPrefub, transform);
        for (int i = 0; i < arrSize; i++)
        {
            float x_startPosition = Random.Range(lowerLeftPosition.x, lowerRightPosition.x);
            defaultStartPosition.x = x_startPosition;
            GameObject chararcter = Instantiate(characterPrefub,lowerLeftPosition, Quaternion.identity ,transform);
            KidPackController kidPackController = chararcter.GetComponent<KidPackController>();
            chararcter.transform.position = defaultStartPosition;
            kidPackController.InitPosition = defaultStartPosition;
            kidPackController.TargetPosition = positions[i];
            kidPackController.TimeInTheWater = timesInTheWater[i];
            kidPackController.TimeInOutsideWater = timesOutsideTheWater[i];
            kidPackController.DangerTarget = dangerTargets[i];

            // new Vector3(2f, 0, 0)

            //CharacterSettings characterSettings = new CharacterSettings(timeInTheWater, timeInOutsideWater, position, lowerLeftPosition, chararcter);
            //characters.Add(characterSettings);
        }
    }

}
