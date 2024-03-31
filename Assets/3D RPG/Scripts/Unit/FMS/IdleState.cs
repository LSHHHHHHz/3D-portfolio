using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class IdleState : IState
{
    CharacterFSMBase character;
    public IdleState(CharacterFSMBase character)
    {
        this.character = character;
    }
    public void Enter(CharacterFSMBase pos)
    {
        Debug.Log("Idle ¿‘¿Â");
    }

    public void Execute(CharacterStatusBase target)
    {
        Debug.Log("Idle execute");
    }

    public void Exit()
    {
        Debug.Log("Idle ≈¿Â");
    }
}