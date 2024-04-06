using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public CharacterType characterType;
    public EnemyStatus enemyStatus; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyStatus = other.GetComponent<EnemyStatus>();
            CheckCharacterType(characterType);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyStatus = null;
            CheckCharacterType(characterType); 
        }
    }

    void CheckCharacterType(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Player:
                DetectManager.instance.playerDetectedEnemyStatus = enemyStatus;
                break;
            case CharacterType.MeleeColleague:
                DetectManager.instance.meleeColleagueDetectedEnemyStatus = enemyStatus;
                break;
            case CharacterType.RangeColleague:
                DetectManager.instance.rangeColleagueDetectedEnemyStatus = enemyStatus;
                break;
        }
    }
}
