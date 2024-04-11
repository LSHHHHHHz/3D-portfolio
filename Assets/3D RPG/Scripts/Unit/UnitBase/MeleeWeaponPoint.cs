using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MeleeWeaponPoint : MonoBehaviour
{
    public CharacterStatusBase characterStatus;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection"))
        {
            CharacterStatusBase monsterStatus = other.GetComponent<CharacterStatusBase>();
            monsterStatus.current_HP -= characterStatus.totalAttack;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection"))
        {

        }
    }
}