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

    public int maxPeopleInTheWater;
    private int howManyInTheWater;


    //private TimerHelper timer;
    private Vector3 lowerLeftPosition = new Vector3(-3f, -5f, 0);
    private Vector3 lowerRightPosition = new Vector3(2.5f, -5f, 0);

    void Start()
    {

        int arrSize = timesInTheWater.Count;
        characters = new List<GameObject>(arrSize);
        //Instantiate(characterPrefub, transform);
        for (int i = 0; i < arrSize; i++)
        {
            GameObject chararcter = Instantiate(characterPrefub,lowerLeftPosition, Quaternion.identity ,transform);
            KidPackController kidPackController = chararcter.GetComponent<KidPackController>();
            chararcter.transform.position = lowerLeftPosition; // todo change to random between the rightLower x position
            kidPackController.InitPosition = lowerLeftPosition;
            kidPackController.TargetPosition = positions[i];
            kidPackController.TimeInTheWater = timesInTheWater[i];
            kidPackController.TimeInOutsideWater = timesOutsideTheWater[i];
            kidPackController.DangerTarget = new Vector3(2f,-(i * 2), 0); // TODO dlete i*2!!

            // new Vector3(2f, 0, 0)

            //CharacterSettings characterSettings = new CharacterSettings(timeInTheWater, timeInOutsideWater, position, lowerLeftPosition, chararcter);
            //characters.Add(characterSettings);
        }
    }

}
