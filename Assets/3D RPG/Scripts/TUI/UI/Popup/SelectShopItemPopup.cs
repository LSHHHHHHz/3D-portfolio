using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SelectShopItemPopup : MonoBehaviour,IPopup
{
    public ItemData itemData;
    public Text countText;
    public int itemCount;
    public Text priceText;
    public int itemprice;

    public Action onItemPurchased;

    private void Start()
    {
        itemCount = 1;
        itemprice = itemData.itemPrice;
    }
    private void OnEnable()
    {
        if (itemData != null)
        {
            itemCount = 1;
            itemprice = itemData.itemPrice;
        }
    }
    private void Update()
    {
        countText.text = itemCount + "°³";
        priceText.text = (itemprice * itemCount) +"¿ø";
    }
    public void ItemCountPlus()
    {
        if (itemData.itemPrice * (itemCount + 1) > PlayerCurrency.Instance.coin)
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
        for(int i=0; i< UserData.instance.inventoryData.slotDatas.Count; i++)
        {
            if (UserData.instance.inventoryData.slotDatas[i].GetItem().itemPrice == 0)
            {
                UserData.instance.inventoryData.slotDatas[i].AddItem(itemData, itemCount);
                break;
            }
        }
        GameManager.instance.ItemPurchased();
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
