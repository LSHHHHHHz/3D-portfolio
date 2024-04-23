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

    public void ExcuteEvent(IActor target)
    {
        if(target is CharacterStatusBase characterStatus)
        {
            if(characterStatus is EnemyStatus enemyStatus)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.GetHit);
            }
            characterStatus.Damaged(damage);
        }
    }
}