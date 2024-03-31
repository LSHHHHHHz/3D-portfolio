using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPC : MonoBehaviour
{
    public NPCWarePoint warePoints;
    private float oringSpeed;
    private DialogueTriggerPopup dialogueTriggerPopupInstatnce;

    private void Awake()
    {
        warePoints= GetComponent<NPCWarePoint>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (dialogueTriggerPopupInstatnce == null)
            {
                dialogueTriggerPopupInstatnce = Instantiate(PopupFactory.instance.dialogueTriggerPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<DialogueTriggerPopup>();
            }
            else
            {
                dialogueTriggerPopupInstatnce.gameObject.SetActive(true);
            }
            oringSpeed = warePoints.moveSpeed;
            warePoints.moveSpeed = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            warePoints.moveSpeed = oringSpeed;
            dialogueTriggerPopupInstatnce.gameObject.SetActive(false);
        }
    }
}
