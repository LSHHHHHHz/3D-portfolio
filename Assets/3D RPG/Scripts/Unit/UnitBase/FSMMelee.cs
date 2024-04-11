using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FSMMelee : DetectBase
{
    public enum State
    {
        Idle,
        Move,
        Attack
    }
    public State currentState = State.Idle;
    WaitForSeconds delay500 = new WaitForSeconds(0.5f);
    WaitForSeconds delay250 = new WaitForSeconds(0.25f);

    protected void Start()
    {
        StartCoroutine(FSM());
    }
    protected virtual IEnumerator FSM()
    {
        while (true)
        {
            switch (currentState)
            {
                case State.Idle:
                    yield return StartCoroutine(Idle());
                    break;
                case State.Move:
                    yield return StartCoroutine(Move());
                    break;
                case State.Attack:
                    yield return StartCoroutine(Attack());
                    break;
            }
        }
    }

    protected virtual IEnumerator Idle()
    {
        yield return delay500; 
        if (detectedTarget)
        {
            currentState = State.Move;
        }

        //�������� ������ �� �ʱ� ��ġ�� Ư�� �Ÿ� �̻� ���̰� �ִٸ� State.Move

    }

    protected virtual IEnumerator Move()
    {
        //���� �Ÿ��� ������
        //currentState = State.Attack
        Debug.Log("�̵���");
        yield return null;
    }

    protected virtual IEnumerator Attack()
    {
        yield return delay250; 
        currentState = State.Idle;
    }

}