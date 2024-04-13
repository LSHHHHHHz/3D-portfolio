using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class NPCShopUI : MonoBehaviour,IPopup
{
    public int shopNumer;
    public GameObject slotPrefab;
    public Transform slotsTransform;
    private ShopData shopData;
    public void SetData()
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
        if(shopNumer ==1)
        {
            for(int i =0; i<slots.Count;i++)
            { slots[i].item= UserData.instance.portionShopData[i];
            }
        }
        if (shopNumer == 2)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].item = UserData.instance.equipShopData[i];
            }
        }
        foreach (Transform slot in slotsTransform)
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

    public void OpenPopupUI()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }
}

