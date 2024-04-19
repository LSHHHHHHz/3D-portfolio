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
            dataName.text = data.itemName;
            description.text = data.itemDescription;
            price.text = data.itemPrice.ToString();            
            
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
