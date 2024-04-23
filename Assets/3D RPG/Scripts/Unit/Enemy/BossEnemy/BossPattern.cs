using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPattern : MonoBehaviour
{
    public FSMController controller;
    public IEnumerator DragonBossSkillPattern(Animator anim)
    {
        yield return new WaitForSeconds(1f);
        int random = UnityEngine.Random.Range(0, 3);
        switch (random)
        {
            case 0:
                yield return StartCoroutine(Attack1(anim));
                break;
            case 1:
                yield return StartCoroutine(Attack2(anim));
                break;
            case 2:
                yield return StartCoroutine(Attack3(anim));
                break;
        }
    }
    IEnumerator Attack1(Animator anim)
    {
        if (controller.enemyStatus.current_HP <= 0)
        {
            controller.ChangeState(new DieState());
            yield break;
        }
        anim.SetTrigger("Skill1");
        Debug.Log("Skill1  계속 들어오나");
        controller.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(2.2f);        
        anim.ResetTrigger("Skill1");
        controller.GetComponent<NavMeshAgent>().speed = 4.9f;
        controller.ChangeState(new IdleState());
    }
    IEnumerator Attack2(Animator anim)
    {
        if (controller.enemyStatus.current_HP <= 0)
        {
            controller.ChangeState(new DieState());
            yield break;
        }
        Debug.Log("Skill2  계속 들어오나");
        anim.SetTrigger("Skill2");
        controller.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(2.4f);
        anim.ResetTrigger("Skill2");
        controller.GetComponent<NavMeshAgent>().speed = 4.9f;
        controller.ChangeState(new IdleState());
    }
    IEnumerator Attack3(Animator anim)
    {
        if (controller.enemyStatus.current_HP <= 0)
        {
            controller.ChangeState(new DieState());
            yield break;
        }
        Debug.Log("Skill3  계속 들어오나");
        anim.SetTrigger("Skill3");
        controller.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(2.5f);
        anim.ResetTrigger("Skill3");
        controller.GetComponent<NavMeshAgent>().speed = 4.9f;
        controller.ChangeState(new IdleState());
    }
}
