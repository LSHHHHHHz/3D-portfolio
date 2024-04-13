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
        Debug.Log("������ ����");
        sender.anim.SetBool("Walk", true);
        sender.enemyStatus.navMeshAgent.speed = sender.speed;
    }
    public void Exit(FSMController sender)
    {
        Debug.Log("������ ��");
        sender.anim.SetBool("Walk", false);
    }
    public void Update(FSMController sender)
    {
        if (sender.enemyStatus.detectPlayer.closeTarget != null)
        {
            //�̵� ������ ������ ���ϰ� �� ���� �־ �̵����Ѿ� ��.
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