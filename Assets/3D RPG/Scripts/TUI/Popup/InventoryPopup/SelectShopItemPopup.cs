using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SelectShopItemPopup : MonoBehaviour,IPopup
{
    public ItemInfo selectedShopItemInfoData;
    public Text countText;
    public int itemCount;
    public Text priceText;
    public int itemprice;

    public Action onItemPurchased;

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
        for (int i = 0; i < PlayerData.instance.playerItemInventoryData.Length; i++)
        {
            if (PlayerData.instance.playerItemInventoryData[i].GetData() == null)
            {
                PlayerData.instance.playerItemInventoryData[i].SetData(new ItemInstance
                {
                    itemInfo = selectedShopItemInfoData,
                    count = itemCount,
                    upgradeLevel = 1
                });
                PlayerData.instance.playerItemInventoryData[i].SetItemType(selectedShopItemInfoData.itemType);
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
