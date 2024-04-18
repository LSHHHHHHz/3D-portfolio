using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class DieState :  IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        Debug.Log("����");
        sender.anim.SetTrigger("Die");
    }

    public void Exit(FSMController sender)
    {
        Debug.Log("������ �� Exit");
    }

    public void Update(FSMController sender)
    {
        Debug.Log("������ �� update");
       // sender.ChangeState(new IdleState());
    }
}