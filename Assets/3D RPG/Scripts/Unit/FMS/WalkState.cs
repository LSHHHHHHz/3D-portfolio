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
        Debug.Log("움직임 시작");
        sender.anim.SetBool("Walk", true);
        sender.enemyStatus.navMeshAgent.speed = sender.speed;
    }
    public void Exit(FSMController sender)
    {
        Debug.Log("움직임 끝");
        sender.anim.SetBool("Walk", false);
    }
    public void Update(FSMController sender)
    {
        if (sender.enemyStatus.detectPlayer.closeTarget != null)
        {
            //이동 가능한 구역을 정하고 그 값을 넣어서 이동시켜야 함.
            float distanceToTarget = Vector3.Distance(sender.transform.position, sender.enemyStatus.detectPlayer.closeTarget.transform.position);
            if (distanceToTarget > sender.attackRange)
            {
                sender.enemyStatus.navMeshAgent.SetDestination(sender.enemyStatus.detectPlayer.closeTarget.transform.position);
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