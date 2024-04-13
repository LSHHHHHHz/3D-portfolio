using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HUDSystemUI : MonoBehaviour
{
    SkillInventoryUI skillInventoryPopup;
    InventoryUI playerInventoryPopup;

    public void OpenPlayerSkillInventory()
    {
        if (skillInventoryPopup == null)
        {
            skillInventoryPopup = Instantiate(PopupFactory.instance.skillInventoryPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<SkillInventoryUI>();
        }
        else
        {
            skillInventoryPopup.OpenPopupUI();
        }
    }

    public void OpenPlayerInventory()
    {
        if (playerInventoryPopup == null)
        {
            playerInventoryPopup = Instantiate(PopupFactory.instance.InventoryPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<InventoryUI>();
        }
        else
        {
            playerInventoryPopup.OpenPopupUI();
        }
    }
}
