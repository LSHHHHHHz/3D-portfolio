using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
public class WalkState : IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        sender.anim.SetBool("Walk", true);
        sender.enemyStatus.navMeshAgent.speed = sender.speed;
    }
    public void Exit(FSMController sender)
    {
        sender.anim.SetBool("Walk", false);
    }
    public void Update(FSMController sender)
    {
        Debug.Log("공격이나 죽을 때 여기 들어오면 안됨");
        if(sender.enemyStatus.current_HP<= 0)
        {
            sender.ChangeState(new DieState());
        }
        if (sender.enemyStatus.detectPlayer.closestTarget != null)
        {
            float distanceToTarget = Vector3.Distance(sender.transform.position, sender.enemyStatus.detectPlayer.closestTarget.transform.position);
            if (distanceToTarget > sender.attackRange)
            {
                if (sender.enemyStatus.navMeshAgent.isActiveAndEnabled && sender.enemyStatus.navMeshAgent.isOnNavMesh)
                {
                    sender.enemyStatus.navMeshAgent.SetDestination(sender.enemyStatus.detectPlayer.closestTarget.transform.position);
                }
            }
            else
            {
                sender.enemyStatus.navMeshAgent.speed = 0;
                sender.ChangeState(new AttackState());
            }
        }
        else
        {
            sender.enemyStatus.navMeshAgent.SetDestination(sender.enemyStatus.originalPosition);
            if (Vector3.Distance(sender.enemyStatus.transform.position, sender.enemyStatus.originalPosition) < 2.1f)
            {
                sender.ChangeState(new IdleState());
            }
        }
    }
}