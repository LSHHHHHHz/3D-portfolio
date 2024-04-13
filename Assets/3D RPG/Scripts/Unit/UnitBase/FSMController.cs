using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FSMController : MonoBehaviour
{
    public EnemyStatus enemyStatus;
    public Animator anim;
    public float speed = 5.0f;
    public float attackRange = 1.5f;
    public float moveArea = 10;
    private IState<FSMController> currentState;
    private void Awake()
    {
        anim= GetComponent<Animator>();
    }
    private void Start()
    {
        ChangeState(new IdleState());
    }
    private void Update()
    {
        currentState?.Update(this);
    }
    public void ChangeState(IState<FSMController> newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
    public void OnGetHit()
    {
        ChangeState(new GetHitState());
    }
}