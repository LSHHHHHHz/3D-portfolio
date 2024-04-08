using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public DetectEnemy detectEnemy;
    ShopPopup shopPopup;
    ShopNPC npc;
    DialogueTriggerPopup dialogueTriggerPopupInstatnce;
    private void Update()
    {
        if(npc != null)
        {
            if (shopPopup == null && Input.GetButtonDown("Check"))
            {
                UnityEngine.Debug.Log("shopPopup == null");
                shopPopup = Instantiate(PopupFactory.instance.portionShopPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<ShopPopup>();
                shopPopup.itemDB = npc.itemDB;
                shopPopup.SetData();
            }
            else if (shopPopup != null && Input.GetButtonDown("Check"))
            {
                UnityEngine.Debug.Log("shopPopup != null");
                //여기 shopPopup.SetData(npc.itemDB) 이렇게 수정
                shopPopup.itemDB = npc.itemDB;
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
        UnityEngine.Debug.Log("Trigger Stay");
        if (other.CompareTag("ShopNPC"))
        {
            UnityEngine.Debug.Log("Trigger Stay2");

            var buttonDown = Input.GetButtonDown("Check");
            UnityEngine.Debug.Log($"button down : ${buttonDown}");

            //if (shopPopup == null && buttonDown)
            //{
            //    UnityEngine.Debug.Log("shopPopup == null");
            //    shopPopup = Instantiate(PopupFactory.instance.portionShopPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<ShopPopup>();
            //    shopPopup.itemDB = other.GetComponent<ShopNPC>().itemDB;
            //    shopPopup.SetData();
            //}
            //else if (shopPopup != null && buttonDown)
            //{
            //    UnityEngine.Debug.Log("shopPopup != null");
            //    shopPopup.itemDB = other.GetComponent<ShopNPC>().itemDB;
            //    shopPopup.SetData();
            //    shopPopup.OpenPopupUI();
            //}
        }
    }
    private void OnTriggerExit(Collider other)
    {
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
