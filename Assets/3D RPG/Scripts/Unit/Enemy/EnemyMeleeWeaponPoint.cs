using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class EnemyMeleeWeaponPoint : MonoBehaviour
{
    public GameObject owner;
    public CharacterStatusBase characterStatus;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForEnemyDetection"))
        {
            IActor actor = other.GetComponent<IActor>();
            CharacterStatusBase status = other.GetComponent<CharacterStatusBase>();

            if (actor != null)
            {
                IActor ownerActor = owner.GetComponent<IActor>();
                if(ownerActor!= null )
                {
                    if(status.current_HP<=0)
                    {
                        return;
                    }
                    SendDamageEvent damageEvent = new SendDamageEvent(ownerActor, characterStatus.totalAttack);
                    actor.OnReceiveEvent(damageEvent);
                }
            }
        }
    }
}