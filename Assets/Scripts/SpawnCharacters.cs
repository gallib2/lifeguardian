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
            chararcter.transform.position = lowerLeftPosition; // todo change to random between the rightLower x position
            chararcter.GetComponent<KidPackController>().InitPosition = lowerLeftPosition;
            chararcter.GetComponent<KidPackController>().TargetPosition = positions[i];
            chararcter.GetComponentInChildren<CharacterMovement>().InitPosition = positions[i];
            chararcter.GetComponent<KidPackController>().TimeInTheWater = timesInTheWater[i];
            chararcter.GetComponent<KidPackController>().TimeInOutsideWater = timesOutsideTheWater[i];

            //CharacterSettings characterSettings = new CharacterSettings(timeInTheWater, timeInOutsideWater, position, lowerLeftPosition, chararcter);
            //characters.Add(characterSettings);
        }
    }

}
