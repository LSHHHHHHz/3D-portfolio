using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
public class SendHealEvent : IEvent
{
    public int heal;
    private IActor healer;

    public SendHealEvent(IActor healer, int heal)
    {
        this.healer = healer;
        this.heal = heal;
    }

    public void ExcuteEvent(IActor excute)
    {
        if(excute is CharacterStatusBase characterStatus)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Heal);
            characterStatus.RecoveryHP(heal);
        }
    }
}