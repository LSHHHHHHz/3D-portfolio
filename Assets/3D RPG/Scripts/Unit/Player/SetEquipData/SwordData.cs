using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
public class SwordData :MonoBehaviour, IEquip
{
    public string swordName;
    public EquipType equipType;

    public EquipType EquipType => equipType;

    public void Equip(IActor actor, ItemData data)
    {
        int stats = 0;
        if (actor is CharacterStatusBase character)
        {
            if (data is EquipData equipData)
            {
                gameObject.SetActive(true);
                stats = equipData.addAttack;
                character.UpdateTotalAttack(stats);
            }
            else
            {
                throw new Exception();
            }
        }
    }
    public void Unequip(IActor actor)
    {
        if (actor is CharacterStatusBase character)
        {
            gameObject.SetActive(false);
        }
    }
    public string GetItemName()
    {
        return swordName;
    }
}