using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class GetHitState :  IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        Debug.Log("피격 시작");
        sender.anim.SetTrigger("GetHit");
    }

    public void Exit(FSMController sender)
    {
        Debug.Log("피격 끝");
    }

    public void Update(FSMController sender)
    {
        sender.ChangeState(new IdleState());
    }
}