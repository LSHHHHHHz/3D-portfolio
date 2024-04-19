using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyStatus : CharacterStatusBase
{
    public GameObject prefab;
    public FSMController monsterFSMController; 
    public DetectPlayer detectPlayer;
    public NavMeshAgent navMeshAgent;
    public Vector3 originalPosition;
    public float elapsed;
    public CapsuleCollider capsuleCollider;

    NormarMonster normarMonster;
    public int monsterNumber;
    public int rewardExp;
    bool isRewardExp =false;
    private void OnEnable()
    {
        normarMonster = EnemyData.Instance.normarMonster[monsterNumber];
        characterName= normarMonster.monsterName;
        max_HP = normarMonster.maxHP;
        current_HP = max_HP;
        totalAttack = normarMonster.damage;
        rewardExp = normarMonster.exp;
        originalPosition = transform.position;
    }
    private void OnDisable()
    {
        isRewardExp = false;
        navMeshAgent.enabled = true;
        capsuleCollider.enabled = true; ;
    }
    private void Update()
    {
        if (current_HP <= 0)
        {
            if (!isRewardExp)
            {
                UnitManager.instance.player.playerStatus.currentExp += rewardExp;
                navMeshAgent.enabled= false;
                capsuleCollider.enabled= false;
                isRewardExp = true;
            }
            elapsed += Time.deltaTime;
            if (elapsed > 2)
            {
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
