using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class ChaseState : IState
{
    CharacterFSMBase character;
    public ChaseState(CharacterFSMBase character)
    {
        this.character = character;
    }
    public void Enter(CharacterFSMBase pos)
    {
    }

    public void Execute(CharacterStatusBase target)
    {
        character.anim.SetBool("IsWalk", true);
        float distanceToMove = 0f;
        float distanceToTarget = Vector3.Distance(character.transform.position, target.transform.position);
        if (character is ColleagueAnimation && target is PlayerStatus)
        {
            distanceToMove = 3f; 
        }
        else if (character is ColleagueAnimation && target is EnemyStatus)
        {
            distanceToMove = 0f; 
        }
        //character is EnemyAnimation && target is PlayerStatus // ColleagueStatus 해야함 지금 EnemyAnimation이 없어서 안함
        if (target != null && distanceToTarget > distanceToMove)
        {
            this.character.transform.position = Vector3.MoveTowards(this.character.transform.position, target.transform.position, character.moveSpeed * Time.deltaTime);
            character.transform.LookAt(target.transform);
        }
        if(distanceToTarget<=distanceToMove)
        {
            Exit();
        }
    }
    public void Exit()
    {
        character.anim.SetBool("IsWalk", false);
    }
}