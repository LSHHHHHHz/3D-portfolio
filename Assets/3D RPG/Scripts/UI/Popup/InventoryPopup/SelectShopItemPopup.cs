using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class SelectShopItemPopup : MonoBehaviour,IPopup
{
    public ItemInfo selectedShopItemInfoData;
    public Text countText;
    public int itemCount;
    public Text priceText;
    public int itemprice;
    private void Start()
    {
        itemCount = 1;
        itemprice = selectedShopItemInfoData.itemPrice;
    }
    private void OnEnable()
    {
        if (selectedShopItemInfoData != null)
        {
            itemCount = 1;
            itemprice = selectedShopItemInfoData.itemPrice;
        }
    }
    private void Update()
    {
        countText.text = itemCount + "°³";
        priceText.text = (itemprice * itemCount) +"¿ø";
    }
    public void ItemCountPlus()
    {
        if (selectedShopItemInfoData.itemPrice * (itemCount + 1) > PlayerCurrency.Instance.coin)
        {
            return;
        }
        itemCount++;
    }
    public void ItemCountMinus()
    {
        itemCount--;
        if (itemCount <= 1)
        {
            itemCount = 1;
        }
    }
    public void BuyItem()
    {
        ItemInventoryManager.instance.AddItem(selectedShopItemInfoData, itemCount);
        PlayerCurrency.Instance.coin -= (itemprice * itemCount);
        gameObject.SetActive(false);
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
