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
        if (sender.enemyStatus.detectPlayer.closeTarget != null)
        {
            sender.ChangeState(new WalkState());
        }
        if (sender.enemyStatus.detectPlayer.closeTarget != null && Vector3.Distance(sender.transform.position, sender.enemyStatus.detectPlayer.closeTarget.transform.position)<sender.attackRange)
        {
            sender.ChangeState(new AttackState());
        }
    }
}
