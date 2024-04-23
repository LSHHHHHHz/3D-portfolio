using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState<FSMController>
{
    Coroutine bossPattern;
    public void Enter(FSMController sender)
    {
        Debug.Log("죽으면 절대 여기 절대 NONO");
        if (sender.enemyStatus is BossEnemyStatus)
        {
            if (!sender.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !sender.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && !sender.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                bossPattern = sender.StartCoroutine(sender.bossPattern.DragonBossSkillPattern(sender.anim));
            }
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

        if(sender.enemyStatus.detectPlayer.closestTarget != null) 
        {
            Vector3 dir = (sender.enemyStatus.detectPlayer.closestTarget.transform.position - sender.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            sender.transform.rotation = Quaternion.Slerp(sender.transform.rotation, targetRotation, sender.turnSpeed * Time.deltaTime);
        }
        if ((sender.enemyStatus.detectPlayer.closestTarget == null ||
            Vector3.Distance(sender.transform.position, sender.enemyStatus.detectPlayer.closestTarget.transform.position) > sender.attackRange + 0.5f)
            && sender.GetComponent<NavMeshAgent>().speed == 0)
        {
            sender.ChangeState(new WalkState());
        }
        if (sender.enemyStatus.detectPlayer.closestTarget == null)
            {
            sender.ChangeState(new IdleState());
        }
    }

}