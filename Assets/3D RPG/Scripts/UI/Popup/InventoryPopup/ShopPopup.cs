using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ShopPopup : MonoBehaviour,IPopup
{
    public ItemDB itemDB;
    public RectTransform slotsParent;
    ItemSlot[] itemShopSlots;
    public SelectShopItemPopup selectShopItemPopup;

    private void Awake()
    {
        itemShopSlots = GetChildItemSlots(slotsParent);
        SetData();
    }
    private void OnEnable()
    {
        SetData();
    }
    ItemSlot[] GetChildItemSlots(RectTransform parent)
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            ItemSlot slot = parent.GetChild(i).GetComponent<ItemSlot>();
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(() =>
            {
                if (selectShopItemPopup == null)
                {
                    selectShopItemPopup = Instantiate(PopupFactory.instance.selectedShopItemPopup, PopupFactory.instance.selectTransform).GetComponent<SelectShopItemPopup>();
                }
                else
                {
                    selectShopItemPopup.OpenPopupUI();
                }
                ItemInventoryManager.instance.SelectedItemInfoData(slot.itemInfo, PopupType.ItemShop);
            });
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    void SetData()
    {
        foreach (ItemInfo iteminfo in itemDB.itemDB)
        {
            if(itemDB.itemDB.Count== iteminfo.slotNumber-1)
            {
                return;
            }
            int num = iteminfo.slotNumber;
            ItemSlot slot = itemShopSlots[num - 1];
            slot.SetData(iteminfo);
        }
    }

    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenPopupUI()
    {
        gameObject.SetActive(true);
    }
}
