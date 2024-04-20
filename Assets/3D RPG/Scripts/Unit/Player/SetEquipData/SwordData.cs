using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
public class SwordData :MonoBehaviour, IEquip
{
    public string swordName;
    public ItemData swordData;
    public void Equip(IActor actor, int stats, ItemData data)
    {
        swordData = data;
        if (actor is CharacterStatusBase character)
        {
            gameObject.SetActive(true);
            character.UpdateTotalAttack(stats);
        }
    }

    public void Unequip(IActor actor, int stats)
    {
        if (actor is CharacterStatusBase character)
        {
            gameObject.SetActive(false);
            character.UpdateTotalAttack(-stats);
        }
    }
    public string GetItemName()
    {
        return swordName;
    }
}