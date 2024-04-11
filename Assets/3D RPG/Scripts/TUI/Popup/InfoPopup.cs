using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopup : MonoBehaviour, IPopup
{
    public Text dataName;
    public Text description;
    public Text price;
    public ItemInfo itemInfo;
    public void SetText(ItemInfo itemInfo)
    {
        if (description != null && itemInfo != null)
        {
            if (itemInfo.itemType == InfoType.HPPortion)
            {
                description.text = itemInfo.HPRecovery + "HP를 회복합니다.";
                price.text = itemInfo.itemPrice.ToString();
            }
            if (itemInfo.itemType == InfoType.MPPortion)
            {
                description.text = itemInfo.HPRecovery + "MP를 회복합니다.";
                price.text = itemInfo.itemPrice.ToString();
            }
            if (itemInfo.itemType == InfoType.Sword)
            {
                description.text = itemInfo.additionalAttack + "공격력을 증가시킵니다.";
                price.text = itemInfo.itemPrice.ToString();
            }
            if (itemInfo.itemType == InfoType.Shield)
            {
                description.text = itemInfo.additionalDefence + "방어력을 증가시킵니다.";
                price.text = itemInfo.itemPrice.ToString();
            }
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
