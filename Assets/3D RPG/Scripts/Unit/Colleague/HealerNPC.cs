using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class HealerNPC : MonoBehaviour, IActor
{
    public int healAmount = 20;
    GameObject dialog;
    public Animator anim;
    public HealerNPC owner;
    private void Awake()
    {
        owner = this;
        anim = GetComponent<Animator>();
    }
    public void OnReceiveEvent(IEvent source)
    {
       source.ExcuteEvent(this);
    }

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
                    StartCoroutine(HealingAfterDelay(1.5f, actor, ownerActor, playerStatus));
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
