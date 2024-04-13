using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class AttackState :  IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        Debug.Log("공격 시작");
        sender.anim.SetBool("Attack", true);
    }

    public void Exit(FSMController sender)
    {
        sender.anim.SetBool("Attack", false);
        Debug.Log("공격 끝");
    }

    public void Update(FSMController sender)
    {
        if (Vector3.Distance(sender.transform.position, sender.enemyStatus.detectPlayer.closeTarget.transform.position) > sender.attackRange + 0.5f)
        {
            sender.ChangeState(new WalkState());
        }
    }
}