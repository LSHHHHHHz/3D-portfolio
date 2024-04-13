using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestSkill : SetBaseSkill
{
    public Animator animator;
    public GameObject prefab;
    public void TestSkill2()
    {
        animator.SetTrigger("Skill2");
        Instantiate(prefab);
    }
}
