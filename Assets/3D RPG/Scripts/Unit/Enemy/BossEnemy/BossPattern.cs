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
        anim.SetTrigger("Skill1");
        controller.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(2.2f);
        controller.GetComponent<NavMeshAgent>().speed = 4.9f;
        controller.ChangeState(new IdleState());
    }
    IEnumerator Attack2(Animator anim)
    {
        anim.SetTrigger("Skill2");
        controller.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(2.4f);
        controller.GetComponent<NavMeshAgent>().speed = 4.9f;
        controller.ChangeState(new IdleState());
    }
    IEnumerator Attack3(Animator anim)
    {
        anim.SetTrigger("Skill3");
        controller.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(2.5f);
        controller.GetComponent<NavMeshAgent>().speed = 4.9f;
        controller.ChangeState(new IdleState());
    }
}
