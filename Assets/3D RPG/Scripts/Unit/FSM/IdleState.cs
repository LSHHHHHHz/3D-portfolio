using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class IdleState : IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        Debug.Log("�⺻���� ����");
    }

    public void Exit(FSMController sender)
    {
        Debug.Log("�⺻ ���� �ƴ�");
    }

    public void Update(FSMController sender)
    {
        Debug.Log("�⺻ ���� ������Ʈ");
        Debug.Log(sender.name);
        if (sender.enemyStatus.current_HP <= 0)
        {
            Debug.Log("���� ���� Die");
            sender.ChangeState(new DieState());
            return;
        }
        if (sender.enemyStatus.detectPlayer.closestTarget != null && sender.enemyStatus.current_HP > 0)
        {
            Debug.Log("���� ���� Walk");
            sender.ChangeState(new WalkState());
        }
        if (sender.enemyStatus.detectPlayer.closestTarget != null && Vector3.Distance(sender.transform.position, sender.enemyStatus.detectPlayer.closestTarget.transform.position)<sender.attackRange && sender.enemyStatus.current_HP>0)
        {
            Debug.Log("���� ���� Attack");
            sender.ChangeState(new AttackState());
        }
        if (sender.enemyStatus.navMeshAgent.isActiveAndEnabled && sender.enemyStatus.detectPlayer.closestTarget == null && Vector3.Distance(sender.transform.position, sender.enemyStatus.originalPosition) > sender.spawnAreaDis && sender.enemyStatus.current_HP > 0)
        {
            sender.ChangeState(new WalkState());
        }
    }
}
