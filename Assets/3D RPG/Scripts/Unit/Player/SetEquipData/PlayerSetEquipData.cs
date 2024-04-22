using Assets._3D_RPG.Scripts.TUI.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSetEquipData : MonoBehaviour
{
    public GameObject[] equipItems;
    public CharacterStatusBase characterStatus;
    private IActor actor;
    IEquip currentEquippedSword;
    IEquip currentEquippedShield;
    private void Start()
    {
        actor = characterStatus as IActor;
        EventManager.instance.changeItemSlot += RefreshWeapon;
        EventManager.instance.unEquipPreItem += RemoveItem;
    }
    void RefreshWeapon(ItemData item)
    {
        if (item == null || item.itemName == "")
        {
            return;
        }
        EquipData equipData = item as EquipData;

        foreach (var equip in equipItems)
        {
            IEquip equipment = equip.GetComponent<IEquip>();
            if (equipment.GetItemName() == equipData.itemName)
            {
                if (equipData.addAttack > 0)
                {
                    equipment.Equip(actor, equipData.addAttack, item);
                    currentEquippedSword = equipment;
                }
                if (equipData.addHp > 0)
                {
                    equipment.Equip(actor, equipData.addHp, item);
                    currentEquippedShield =equipment;
                }
            }
        }
    }
    void RemoveItem(int num)
    {
        foreach (var equip in equipItems)
        {
            IEquip equipment = equip.GetComponent<IEquip>();
            if(num ==0 && equipment == currentEquippedSword)
            {
                equipment.Unequip(actor, 0);
                currentEquippedSword = null;
            }            
            if(num ==1 && equipment == currentEquippedShield)
            {
                equipment.Unequip(actor, 0);
                currentEquippedShield = null;
            }
        }
    }
}

