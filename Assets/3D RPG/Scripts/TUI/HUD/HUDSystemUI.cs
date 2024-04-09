using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HUDSystemUI : MonoBehaviour
{
    SkillInventoryPopup skillInventoryPopup;
    PlayerInventoryPopup playerInventoryPopup;

    public void RunItemGacha()
    {
        if (skillInventoryPopup == null)
        {
            skillInventoryPopup = Instantiate(PopupFactory.instance.skillInventoryPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<SkillInventoryPopup>();
        }
        else
        {
            skillInventoryPopup.gameObject.SetActive(true);
        }
    }

    public void OpenPlayerInventory()
    {
        if (playerInventoryPopup == null)
        {
            playerInventoryPopup = Instantiate(PopupFactory.instance.playerInventoryPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<PlayerInventoryPopup>();
        }
        else
        {
            playerInventoryPopup.gameObject.SetActive(true);
        }
    }
}
