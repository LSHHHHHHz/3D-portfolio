using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
public interface IEquip 
{
    void Equip(IActor actor, int stats, ItemData data);
    void Unequip(IActor actor, int stats);
    string GetItemName();
}