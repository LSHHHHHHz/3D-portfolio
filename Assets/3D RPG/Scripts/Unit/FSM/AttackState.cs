using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AttackState :  IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        if (sender.enemyStatus is BossEnemyStatus)
        {
            sender.StartCoroutine(sender.bossPattern.DragonBossSkillPattern(sender.anim));
        }
        else
        {
            sender.anim.SetBool("Attack", true);
        }
        sender.transform.LookAt(sender.enemyStatus.detectPlayer.closestTarget.transform.position);
    }

    public void Exit(FSMController sender)
    {
        if (!(sender.enemyStatus is BossEnemyStatus))
        {
            sender.anim.SetBool("Attack", false);
        }
    }

    public void Update(FSMController sender)
    {
        if (sender.enemyStatus.current_HP <= 0)
        {
            sender.ChangeState(new DieState());
            return;
        }
        if ((sender.enemyStatus.detectPlayer.closestTarget == null||
            Vector3.Distance(sender.transform.position, sender.enemyStatus.detectPlayer.closestTarget.transform.position) > sender.attackRange + 0.5f)
            &&sender.GetComponent<NavMeshAgent>().speed !=0)
        {
            sender.ChangeState(new WalkState());
        }
    }
   
}