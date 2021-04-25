using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public List<Transform> itemSlotList = new List<Transform>();
    public int slotActive = -1;
    public bool hasItems = false;

    private void Awake()
    {
        if ( Manager.Instance.InventoryUIManager == null)
            Manager.Instance.InventoryUIManager = this;
        else
        {
            slotActive = Manager.Instance.InventoryUIManager.slotActive;
        }

        
        foreach (Transform item in GetComponentsInChildren<Transform>(true))
        {
            if ( item.name.StartsWith("Item_"))
            {
                itemSlotList.Add(item);
            }
        }

        
    }

    private void Start()
    {
        InputManager.Instance.onN1Pressed.AddListener(SelectSlot1);
        InputManager.Instance.onN2Pressed.AddListener(SelectSlot2);
        Manager.Instance.Player.inventory.inventoryUpdate.AddListener(OnInventoryUpdate);
        OnInventoryUpdate();
    }

    private void SelectSlot1()
    {
        if ( slotActive == 0 )
        {
            slotActive = -1;
            Deselect();
            return;
        }

        slotActive = 0;
        SelectSlot("Item_1");
    }

    private void SelectSlot2()
    {
        if (slotActive == 1)
        {
            slotActive = -1;
            Deselect();
            return;
        }

        slotActive = 1;
        SelectSlot("Item_2");
    }

    private void SelectSlot(string item_active)
    {
        foreach (Transform item in itemSlotList)
        {
            if (item.name.StartsWith(item_active))
            {
                item.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 200);
            }
            else
            {
                item.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            }
        }
    }

    private void Deselect()
    {
        foreach (Transform item in itemSlotList)
        {
            if (item.name.StartsWith("Item_"))
            {
                item.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            }
        }
    }

    private void SetHealthPotions(int count)
    {
        var obj = GetComponentByName("Item_1");
        

        if (obj != null)
        {
            Manager.Potions = count;
            if (count == 0)
            {
                slotActive = -1;
                obj.SetActive(false);
                Deselect();
                return;
            }


            obj.SetActive(true);
            obj.transform.GetChild(1).GetComponent<TMP_Text>().text = "" + count;
        }

        
    }

    private void SetKeys(int count)
    {
        var obj = GetComponentByName("Item_2");
        if (obj != null)
        {
            obj.SetActive(true);
            obj.transform.GetChild(1).GetComponent<TMP_Text>().text = "" + count;
        }
    }

    private void OnInventoryUpdate()
    {
        if (Manager.Instance.Player.inventory.Inventory.Count <= 0) return;
        if (Manager.Instance.Player.inventory.Inventory.ContainsKey(CollectableTypes.HealthPot))
            SetHealthPotions(Manager.Instance.Player.inventory.Inventory[CollectableTypes.HealthPot]);

        if (Manager.Instance.Player.inventory.Inventory.ContainsKey(CollectableTypes.Key))
            SetKeys(Manager.Instance.Player.inventory.Inventory[CollectableTypes.Key]);

        if (Manager.Instance.Player.inventory.Inventory.Count > 0)
            hasItems = true;
    }

    private GameObject GetComponentByName(string name)
    {
        GameObject result = null;
        for (int i = 0; i < itemSlotList.Count; i++)
        {
            if (itemSlotList[i].name == name)
            {
                result = itemSlotList[i].gameObject;
                break;
            }
        }
        return result;
    }
}
