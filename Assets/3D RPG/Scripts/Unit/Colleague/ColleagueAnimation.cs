using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColleagueAnimation : CharacterFSMBase
{
    PlayerStatus playerStatus;
    EnemyStatus enemyStatus;
    EnemyStatus colleagueDetecteEnemyStatus;
    private void Update()
    {
        if (DetectManager.instance.checkEnemyDetect == false)
        {
            ChaseToTarget();
        }
        else if(DetectManager.instance.checkEnemyDetect == true)
        {
            AttactToEnemy();
        }
    }
    void ChaseToTarget()
    {
        playerStatus = UnitManager.instance.player.GetComponent<PlayerStatus>();
        if (DetectManager.instance.playerDetectedEnemyStatus != null)
        {
            enemyStatus = DetectManager.instance.playerDetectedEnemyStatus.GetComponent<EnemyStatus>();
        }
        else
        {
            enemyStatus = null;
        }

        if (enemyStatus == null)
        {
            if (playerStatus != null && !(currentState is ChaseState))
            {
                ChangeState(new ChaseState(this));
            }
            currentState?.Execute(playerStatus);
        }
        else
        {
            ChangeState(new ChaseState(this));
            currentState?.Execute(enemyStatus);
        }
    }
    void AttactToEnemy()
    {
        if(characterType == CharacterType.MeleeColleague && DetectManager.instance.meleeColleagueDetectedEnemyStatus != null)
        {
            colleagueDetecteEnemyStatus = DetectManager.instance.meleeColleagueDetectedEnemyStatus;
            ChangeState(new AttackState(this));
            currentState?.Execute(colleagueDetecteEnemyStatus);
        }
        if(characterType == CharacterType.RangeColleague && DetectManager.instance.rangeColleagueDetectedEnemyStatus != null)
        {
            colleagueDetecteEnemyStatus = DetectManager.instance.rangeColleagueDetectedEnemyStatus;
            ChangeState(new AttackState(this));
            currentState?.Execute(colleagueDetecteEnemyStatus);
        }
    }
}
