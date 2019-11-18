using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObject _item, int _amount)
    {
        //container.Add(new InventorySlot(_item, _amount));
        container.Add(new InventorySlot(_item, _amount));

        //bool hasItem = false;
        //for (int i = 0; i < container.Count; i++)
        //{
        //    if(container[i].item == _item)
        //    {
        //        container[i].AddAmount(_amount);
        //        hasItem = true;
        //        break;
        //    }
        //}

        //if(!hasItem)
        //{
        //    container.Add(new InventorySlot(_item, _amount));
        //}
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public TimerHelper timerHelper;
    public int timeToFinishWork;
    public int amount;

    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        timeToFinishWork = _item.timeToFinishWork;
        amount = _amount;
        timerHelper = new TimerHelper();
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
