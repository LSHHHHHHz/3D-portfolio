using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    public Image itemIcon;
    public Text itemName;
    public Text itemDescription;
    public Text itemPrice;
    public Button button;

    private SlotData currentSlotData;
    private SelectShopItemPopup selectShopItemPopup;

    private void Awake()
    {
        button= GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if(selectShopItemPopup ==null)
            {
                selectShopItemPopup= Instantiate(PopupFactory.instance.selectedShopItemPopupPrefab, PopupFactory.instance.selectTransform).GetComponent<SelectShopItemPopup>();
                selectShopItemPopup.itemData = currentSlotData.item;
                AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickSlot);
            }
            else
            {
                selectShopItemPopup.OpenPopupUI();
                selectShopItemPopup.itemData = currentSlotData.item;
                AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickSlot);
            }
        });
    }
    public void SetData(SlotData slotData)
    {
        currentSlotData = slotData;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentSlotData != null && currentSlotData.item != null)
        {
            itemIcon.sprite = Resources.Load<Sprite>(currentSlotData.item.iconPath);
            itemName.text = currentSlotData.item.itemName;
            itemDescription.text = currentSlotData.item.itemDescription;
            itemPrice.text = currentSlotData.item.itemPrice.ToString();
        }
    }
    private void OpenBuyPopup()
    {

    }
}