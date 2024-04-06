using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestTest : MonoBehaviour
{
    SelectShopItemPopup popup;
    public void EquipShopPopup()
    {
        if (popup == null)
        {
            Instantiate(PopupFactory.instance.equipmentShopPopupPrefab, PopupFactory.instance.popupTransForm);
        }
        else
        {
            popup.gameObject.SetActive(true);
        }
    }
    public void PortionShopPopup()
    {
        if (popup == null)
        {
            Instantiate(PopupFactory.instance.portionShopPopupPrefab, PopupFactory.instance.popupTransForm);
        }
        else
        {
            popup.gameObject.SetActive(true);
        }
    }
}
