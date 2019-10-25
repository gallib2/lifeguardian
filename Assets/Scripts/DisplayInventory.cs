using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    Dictionary<InventorySlot, GameObject> itemDisplay = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.prefub, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            itemDisplay.Add(inventory.container[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN), Y_START +(-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COLUMN)), 0f);
    }

    private void UpdateDisplay()
    {
        int lastIndex = inventory.container.Count - 1;
        //CreateDisplay();
        var obj = Instantiate(inventory.container[lastIndex].item.prefub, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(lastIndex);
        itemDisplay.Add(inventory.container[lastIndex], obj);
    }


    private void OnEnable()
    {
        Lifeguard.OnItemAddToInventory += UpdateDisplay;
    }

    private void OnDisable()
    {
        Lifeguard.OnItemAddToInventory -= UpdateDisplay;
    }
}
