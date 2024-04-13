using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class NPCShopUI : MonoBehaviour
{
    public int shopNumer;
    public GameObject slotPrefab;
    public Transform slotsTransform;
    private ShopData shopData;

    private void Start()
    {
        shopData = UserData.instance.shopData;
        RefreshSlots();
    }

    private void RefreshSlots()
    {
        List<SlotData> slots = shopData.GetSlotsByNumber(shopNumer);
        if(slots == null )
        {
            Debug.Log("데이터 없음");
            return;
        }
        foreach(Transform slot in slotsTransform)
        {
            Destroy(slot.gameObject);
        }

        foreach (SlotData slot in slots)
        {
            GameObject slotObject = Instantiate(slotPrefab, slotsTransform);
            ShopSlotUI slotUI = slotObject.GetComponent<ShopSlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(slot);
            }
        }
    }
}
