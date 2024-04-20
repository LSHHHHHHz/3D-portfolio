using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
public class EquipSwordEvent : IEvent
{
    public int addAttack;

    public EquipSwordEvent( int addAttack)
    {
        this.addAttack = addAttack;
    }

    public void ExcuteEvent(IActor target)
    {
        if(target is CharacterStatusBase characterStatus)
        {
            characterStatus.UpdateTotalAttack(addAttack);
        }
    }
}