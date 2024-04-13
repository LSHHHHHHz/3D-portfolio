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
    public ItemData data;
    public void SetText(ItemData data)
    {
        if (description != null && data != null)
        {
            //if (data.itemType == InfoType.HPPortion)
            //{
            //    description.text = data.HPRecovery + "HP�� ȸ���մϴ�.";
            //    price.text = data.itemPrice.ToString();
            //}
            //if (data.itemType == InfoType.MPPortion)
            //{
            //    description.text = data.HPRecovery + "MP�� ȸ���մϴ�.";
            //    price.text = data.itemPrice.ToString();
            //}
            //if (data.itemType == InfoType.Sword)
            //{
            //    description.text = data.additionalAttack + "���ݷ��� ������ŵ�ϴ�.";
            //    price.text = data.itemPrice.ToString();
            //}
            //if (data.itemType == InfoType.Shield)
            //{
            //    description.text = data.additionalDefence + "������ ������ŵ�ϴ�.";
            //    price.text = data.itemPrice.ToString();
            //}
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
