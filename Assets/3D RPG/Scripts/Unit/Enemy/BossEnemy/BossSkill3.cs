using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossSkill3 : MonoBehaviour
{
    [SerializeField] int damage = 50;
    [SerializeField] GameObject owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            IActor targetActor = other.GetComponent<IActor>();
            IActor subject= other.GetComponent<IActor>();
            if (targetActor != null)
            {
                SendDamageEvent damage = new SendDamageEvent(subject, this.damage);
                damage.ExcuteEvent(targetActor);
            }
        }
    }
}
