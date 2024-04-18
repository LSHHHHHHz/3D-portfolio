using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class SetBaseSkill : MonoBehaviour
{
    public IActor subject;
    public Player player;
    public int MPConsum;
    public int damage;
    public virtual void Start()
    {
        player = UnitManager.instance.player;
    }
    public virtual void Execute(IActor actor, int damage)
    {
    }
}
