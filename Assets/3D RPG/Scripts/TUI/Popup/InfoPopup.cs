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
                description.text = itemInfo.HPRecovery + "HP�� ȸ���մϴ�.";
                price.text = itemInfo.itemPrice.ToString();
            }
            if (itemInfo.itemType == InfoType.MPPortion)
            {
                description.text = itemInfo.HPRecovery + "MP�� ȸ���մϴ�.";
                price.text = itemInfo.itemPrice.ToString();
            }
            if (itemInfo.itemType == InfoType.Sword)
            {
                description.text = itemInfo.additionalAttack + "���ݷ��� ������ŵ�ϴ�.";
                price.text = itemInfo.itemPrice.ToString();
            }
            if (itemInfo.itemType == InfoType.Shield)
            {
                description.text = itemInfo.additionalDefence + "������ ������ŵ�ϴ�.";
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
