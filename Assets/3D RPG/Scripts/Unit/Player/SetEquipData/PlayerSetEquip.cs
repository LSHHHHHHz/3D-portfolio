using Assets._3D_RPG.Scripts.TUI.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetEquip : MonoBehaviour
{
    public GameObject[] equipItems;
    public CharacterStatusBase characterStatus;
    private IActor actor;

    private Dictionary<EquipType, IEquip> currentEquipped = new Dictionary<EquipType, IEquip>(); 
    private void Start()
    {
        actor = characterStatus;
        EventManager.instance.changeItemSlot += RefreshEquip;
    }
    void RefreshEquip(ItemData itemData)
    {
        if (itemData == null || itemData.itemName == "")
        {
            return;
        }
        EquipData equipData = itemData as EquipData;
        if (currentEquipped != null)
        {
            currentEquipped.Clear();
        }
        foreach (var equip in equipItems)
        {
            IEquip equipment = equip.GetComponent<IEquip>();
            equipment.Unequip(actor);
            if (equipment.GetItemName() == equipData.itemName)
            {
                equipment.Equip(actor, itemData);
                currentEquipped.Add(equipment.EquipType, equipment);
            }
        }
    }
}

