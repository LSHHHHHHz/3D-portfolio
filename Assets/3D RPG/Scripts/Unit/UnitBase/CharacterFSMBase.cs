using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterFSMBase : MonoBehaviour
{
    public IState currentState;
    public CharacterType characterType;
    private IdleState idleState;
    public Animator anim;
    public int moveSpeed;
    public GameObject AttackPoint;

    private void Awake()
    {
        idleState = new IdleState(this);
        ChangeState(idleState);
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
    }
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState= newState;
        newState.Enter(this);
    }
}