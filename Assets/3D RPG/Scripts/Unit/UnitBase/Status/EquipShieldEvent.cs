using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
public class EquipShieldEvent : IEvent
{
    public int addHp;

    public EquipShieldEvent( int addHp)
    {
        this.addHp = addHp;
    }

    public void ExcuteEvent(IActor target)
    {
        if(target is CharacterStatusBase characterStatus)
        {
            characterStatus.UpdateToTalHP(addHp);
        }
    }
}