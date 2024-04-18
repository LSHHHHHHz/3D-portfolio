using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class GetHitState :  IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        Debug.Log("�ǰ� ����");
        sender.anim.SetTrigger("GetHit");
    }

    public void Exit(FSMController sender)
    {
        Debug.Log("�ǰ� ��");
    }

    public void Update(FSMController sender)
    {

        if (sender.enemyStatus.current_HP <= 0)
        {
            sender.ChangeState(new DieState());
        }
        else
        {
            sender.ChangeState(new IdleState());
        }
    }
}