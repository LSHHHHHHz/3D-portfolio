using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public DetectEnemy detectEnemy;
    ShopPopup shopPopup;
    DialogueTriggerPopup dialogueTriggerPopupInstatnce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopNPC"))
        {
            if (dialogueTriggerPopupInstatnce == null)
            {
                dialogueTriggerPopupInstatnce = Instantiate(PopupFactory.instance.dialogueTriggerPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<DialogueTriggerPopup>();
            }
            else
            {
                dialogueTriggerPopupInstatnce.OpenPopupUI();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ShopNPC"))
        {
            if (shopPopup == null && Input.GetButtonDown("Check"))
            {
                shopPopup = Instantiate(PopupFactory.instance.portionShopPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<ShopPopup>();
                shopPopup.itemDB = other.GetComponent<ShopNPC>().itemDB;
            }
            else if (shopPopup != null && Input.GetButtonDown("Check"))
            {
                shopPopup.itemDB = other.GetComponent<ShopNPC>().itemDB;
                shopPopup.OpenPopupUI();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        shopPopup.ClosePopupUI();
        dialogueTriggerPopupInstatnce.ClosePopupUI();
    }
}
