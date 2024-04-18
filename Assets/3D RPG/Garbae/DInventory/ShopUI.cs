using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private int itemID;
    [SerializeField]
    private Data itemDB;

    public RectTransform slotsParent;
    ItemSlot[] itemShopSlots;
    public SelectShopItemPopup selectShopItemPopup;

    private void Awake()
    {
        itemShopSlots = GetChildItemSlots(slotsParent);
    }
    private void OnEnable()
    {
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
                    selectShopItemPopup = Instantiate(PopupFactory.instance.selectedShopItemPopupPrefab, PopupFactory.instance.selectTransform).GetComponent<SelectShopItemPopup>();
                   // selectShopItemPopup.selectedShopItemInfoData = slot.itemInfo;
                }
                else
                {
                  //  selectShopItemPopup.selectedShopItemInfoData = slot.itemInfo;
                    selectShopItemPopup.OpenPopupUI();
                }
            });
            slots.Add(slot);
        }
        return slots.ToArray();
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
