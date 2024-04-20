using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
public class ShieldData : MonoBehaviour, IEquip
{
    public string shieldName;
    public ItemData shieldData;
    public void Equip(IActor actor, int stats, ItemData data)
    {
        shieldData = data;
        if (actor is CharacterStatusBase character)
        {
            gameObject.SetActive(true);
            character.UpdateToTalHP(stats);
        }
    }

    public void Unequip(IActor actor, int stats)
    {
        if (actor is CharacterStatusBase character)
        {
            gameObject.SetActive(false);
            character.UpdateToTalHP(-stats);
        }
    }
    public string GetItemName()
    {
        return shieldName;
    }
}