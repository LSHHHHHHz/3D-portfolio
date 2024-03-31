using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AttackState : IState
{
    CharacterFSMBase character;
    int originSpeed;
    
    public AttackState(CharacterFSMBase character)
    {
        this.character = character;
    }
    public void Enter(CharacterFSMBase pos)
    {
        originSpeed = character.moveSpeed;
    }

    public void Execute(CharacterStatusBase target)
    {
        character.anim.SetBool("IsAttack", true);
        character.moveSpeed = 0;
        character.transform.LookAt(target.transform.position);
    }

    public void Exit()
    {
        character.anim.SetBool("IsAttack", false);
        character.moveSpeed = originSpeed;
    }
}