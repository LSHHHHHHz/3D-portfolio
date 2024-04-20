using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class SetBaseSkill4 : SetBaseSkill
{
    public override void Start()
    {
        base.Start(); 
    }
    private void OnEnable()
    {
        gameObject.SetActive(true);
    }
    public override void Execute(IActor actor,int damage)
    {
        this.subject = actor;
        this.damage = damage;
        transform.position = UnitManager.instance.player.transform.position;
        gameObject.SetActive(true);
        StartCoroutine(SetSkil());
    }
    IEnumerator SetSkil()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection"))
        {
            IActor actor = other.GetComponent<IActor>();
            CharacterStatusBase status = other.GetComponent<CharacterStatusBase>();

            if (actor != null)
            {
                SendDamageEvent damageEvent = new SendDamageEvent(this.subject, damage);
                actor.OnReceiveEvent(damageEvent);
            }
        }
    }
}
