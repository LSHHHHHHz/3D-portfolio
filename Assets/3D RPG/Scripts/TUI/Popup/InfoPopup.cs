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
            if (data.itemName == "기초HP포션")
            {                
                description.text = (data as PortionData).hpRecovery + "HP를 회복합니다.";
                price.text = data.itemPrice.ToString();
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
