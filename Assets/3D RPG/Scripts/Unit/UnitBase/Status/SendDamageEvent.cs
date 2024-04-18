using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
public class SendDamageEvent : IEvent
{
    public int damage;
    private IActor attacker;

    public SendDamageEvent(IActor attacker, int damage)
    {
        this.attacker = attacker;
        this.damage = damage;
    }

    public void ExcuteEvent(IActor excute)
    {
        excute.Damaged(damage);
    }
}