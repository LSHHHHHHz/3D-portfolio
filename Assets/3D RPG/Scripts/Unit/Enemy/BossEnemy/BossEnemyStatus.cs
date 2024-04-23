using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BossEnemyStatus : EnemyStatus
{

    BossMonster bossMonster;
    private void OnEnable()
    {
        bossMonster = EnemyData.Instance.bossMonster[monsterNumber];
        characterName= bossMonster.monsterName;
        max_HP = bossMonster.maxHP;
        current_HP = max_HP;
        totalAttack = bossMonster.damage;
        rewardExp = bossMonster.exp;
    }
    private void OnDisable()
    {
        elapsed = 0;
        isRewardExp = false;
        capsuleCollider.enabled = true; 
    }
    private void Update()
    {
        if (current_HP <= 0)
        {
            current_HP = 0;
            if (!isRewardExp)
            {
                navMeshAgent.enabled= false;
                capsuleCollider.enabled= false;
            }
            elapsed += Time.deltaTime;
            if (elapsed > 5)
            {
                UnitManager.instance.player.playerStatus.currentExp += rewardExp;
                isRewardExp = true;
                prefab.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack") && current_HP>0 || other.CompareTag("Skill") && current_HP>0)
        {
            monsterFSMController.ChangeState(new GetHitState());
        }
    }
}
