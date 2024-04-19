using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    GameObject dialog;
    public Animator anim;
    public HealerNPC owner;
    private void OnTriggerStay(Collider other)
    {
        IActor actor = other.GetComponent<IActor>();
        if (actor != null && actor is PlayerStatus playerStatus)
        {
            IActor ownerActor = owner.GetComponent<IActor>();
            if (playerStatus.CheckPlayerLowHP())
            {
                if (dialog == null)
                {
                    dialog = Instantiate(PopupFactory.instance.healerPopupPrefab, PopupFactory.instance.popupTransForm);
                }
                else
                {
                    dialog.SetActive(true);
                }
                if (Input.GetButtonDown("Check"))
                {
                    transform.LookAt(playerStatus.transform.position);
                    anim.SetTrigger("Heal");
                    StartCoroutine(HealingAfterDelay(2f, actor, ownerActor, playerStatus)); 
                }
            }
        }
    }
    private IEnumerator HealingAfterDelay(float delay, IActor actor, IActor ownerActor, PlayerStatus playerStatus)
    {
        yield return new WaitForSeconds(delay);
        SendHealEvent heal = new SendHealEvent(ownerActor, owner.healAmount);
        actor.OnReceiveEvent(heal);
    }
    private void OnTriggerExit(Collider other)
    {
        IActor actor = other.GetComponent<IActor>();
        if (actor != null && actor is PlayerStatus)
        {
            if (dialog != null)
            {
                dialog.SetActive(false);
            }
        }
    }
}
