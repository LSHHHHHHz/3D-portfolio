using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerEquipInventoryItem : CharacterEquipInventoryItemBase
{
    private void Start()
    {
        ItemInventoryManager.instance.characterEquipInventoryItemBase = this;
    }
    public override void EquipItem(ItemInstance item, InfoType type)
    {
        base.EquipItem(item, type);
    }
}