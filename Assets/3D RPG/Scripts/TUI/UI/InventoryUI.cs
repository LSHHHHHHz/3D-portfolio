using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotsTransform; 
    private InventoryData inventoryData;

    private void Start()
    {
        inventoryData = new InventoryData();
        RefreshSlots();
    }

    private void RefreshSlots()
    {
        foreach (SlotData slot in inventoryData.slotDatas)
        {
            GameObject slotObject = Instantiate(slotPrefab, slotsTransform);
            SlotUI slotUI = slotObject.GetComponentInChildren<SlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(slot);
            }
        }
    }
}
