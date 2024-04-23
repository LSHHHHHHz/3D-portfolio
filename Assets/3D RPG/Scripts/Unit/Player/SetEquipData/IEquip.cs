using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public enum EquipType
{
    Sword,
    Shield,
}

public interface IEquip 
{
    public EquipType EquipType { get; }
    void Equip(IActor actor, ItemData data);
    void Unequip(IActor actor);
    string GetItemName();
}