using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class SetBaseSkill : MonoBehaviour
{
    public Player player;
    public int MPConsum;
    public virtual void Start()
    {
        player = UnitManager.instance.player.playerStatus.GetComponent<Player>();
    }
    public virtual void Execute(int damage)
    {
    }
}
