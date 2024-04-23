using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class DieState :  IState<FSMController>
{
    public void Enter(FSMController sender)
    {
        Debug.Log("죽음");
        sender.anim.SetTrigger("Die");
        if (sender.enemyStatus is BossEnemyStatus)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.DragonDie);
        }
        else
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.GovlinDie);
        }
    }

    public void Exit(FSMController sender)
    {
        Debug.Log("들어오면 끝 Exit");
    }

    public void Update(FSMController sender)
    {
        Debug.Log("들어오면 끝 update");
       // sender.ChangeState(new IdleState());
    }
}