using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemInfo itemInfo;
    public int slotNumber;
    public Image icon;
    public Text itemName;
    public Text itemDescription;
    public Text itemPrice;
    public Text statsInfo;
    public Text countText;
    public Sprite nullImage;

    private void Awake()
    {
        if (itemInfo == null)
        {
            icon.sprite = nullImage;
        }
    }

    public void SetData(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
        icon.sprite = itemInfo.iconImage;
        if(itemName != null)
        {
            itemName.text = itemInfo.itemName;
        }
        if(itemDescription != null)
        {
            itemDescription.text = itemInfo.itemDescription;
        }
        if(itemPrice != null)
        {
            itemPrice.text = itemInfo.itemPrice.ToString();
        }
        if (statsInfo != null)
        {
            if (itemInfo.itemSort == ItemSort.HPProtion)
            {
                statsInfo.text = itemInfo.HPRecovery + "HP를 회복합니다.";
            }
            if (itemInfo.itemSort == ItemSort.MPPortion)
            {
                statsInfo.text = itemInfo.HPRecovery + "MP를 회복합니다.";
            }
            if (itemInfo.itemSort == ItemSort.Sword)
            {
                statsInfo.text = itemInfo.additionalAttack + "공격력을 증가시킵니다.";
            }
            if (itemInfo.itemSort == ItemSort.Shield)
            {
                statsInfo.text = itemInfo.additionalDefence + "방어력을 증가시킵니다.";
            }
        }
    }
    public void SetData(ItemInstance itemInstance)
    {
        if(itemInstance == null)
        {
            ClearData();
            return;
        }
        SetData(itemInstance.itemInfo);
        if(countText !=null)
        {
            countText.gameObject.SetActive(true);
            countText.text = itemInstance.count.ToString();
        }
    }
    public void ClearData()
    {
        itemInfo = null;
        icon.sprite = nullImage;
        if(countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }

    public bool IsEmpty()
    {
        return itemInfo == null;
    }

}
