using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerTargettingEnemy playerTargettingEnemy;
    public PlayerStatus playerStatus;
    public CameraFollow cameraFollow;
    NPCShopUI shopPopup;
    ShopNPC npc;
    bool closedShopNPC = false;
    DialogueTriggerPopup dialogueTriggerPopupInstatnce;
    private void Update()
    {
        if(npc != null && closedShopNPC)
        {
            if (shopPopup == null && Input.GetButtonDown("Check"))
            {
                shopPopup = Instantiate(PopupFactory.instance.ShopPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<NPCShopUI>();
                shopPopup.shopNumer = npc.shopNum;
                shopPopup.SetData();
            }
            else if (shopPopup != null && Input.GetButtonDown("Check"))
            {
                shopPopup.shopNumer = npc.shopNum;
                shopPopup.SetData();
                shopPopup.OpenPopupUI();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ShopNPC"))
        {
            npc=other.GetComponent<ShopNPC>();
        }
        if (npc != null)
        {
            closedShopNPC = true;
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
    private void OnTriggerExit(Collider other)
    {
        closedShopNPC = false;
        npc = null;
        if (shopPopup != null)
        {
            shopPopup.ClosePopupUI();
        }
        if (dialogueTriggerPopupInstatnce != null)
        {
            dialogueTriggerPopupInstatnce.ClosePopupUI();
        }
    }
}
