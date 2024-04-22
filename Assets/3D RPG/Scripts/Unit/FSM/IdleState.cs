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
        Debug.Log("기본 상태 업데이트");
        Debug.Log(sender.name);
        if (sender.enemyStatus.current_HP <= 0)
        {
            sender.ChangeState(new DieState());
            return;
        }
        if (sender.enemyStatus.detectPlayer.closestTarget != null)
        {
            sender.ChangeState(new WalkState());
        }
        if (sender.enemyStatus.detectPlayer.closestTarget == null && Vector3.Distance(sender.enemyStatus.transform.position, sender.enemyStatus.detectPlayer.transform.position)<1)
        {
            sender.ChangeState(new WalkState());
        }
        if (sender.enemyStatus.detectPlayer.closestTarget != null && Vector3.Distance(sender.transform.position, sender.enemyStatus.detectPlayer.closestTarget.transform.position)<sender.attackRange)
        {
            sender.ChangeState(new AttackState());
        }
    }
}
