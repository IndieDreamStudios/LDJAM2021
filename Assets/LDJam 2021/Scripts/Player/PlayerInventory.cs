using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<CollectableTypes, int> inventory = new Dictionary<CollectableTypes, int>();
    public Dictionary<CollectableTypes, int> Inventory => inventory;


    public UnityEvent inventoryUpdate;

    public void AddCollectable(CollectableTypes collectable)
    {
        if (inventory.ContainsKey(collectable))
            inventory[collectable] += 1;
        else
            inventory.Add(collectable, 1);

        inventoryUpdate?.Invoke();
    }

    public void UseCollectable(CollectableTypes collectable)
    {
        if (inventory.ContainsKey(collectable))
            inventory[collectable] -= 1;
        else
            inventory.Add(collectable, 0);

        inventoryUpdate?.Invoke();
    }

    public bool GetCollectable(CollectableTypes collectable)
    {
        return inventory.ContainsKey(collectable) && inventory[collectable] > 0;
    }

}
