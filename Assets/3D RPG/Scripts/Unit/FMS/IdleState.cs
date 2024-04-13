using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class IdleState : IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        Debug.Log("기본상태 시작");
    }

    public void Exit(FSMController sender)
    {
        Debug.Log("기본 상태 아님");
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
