using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class CharacterSetEquipItemBase : MonoBehaviour
{
    public GameObject[] swords;
    public GameObject[] shields;
    public CharacterEquipInventoryItemBase equipWeapon;
    private ItemInstance previousEquippedWeapon = null;
    private ItemInstance previousEquippedShield = null;
    protected virtual void RefreshWeapon()
    {
        if (ItemInventoryManager.instance.equipWeaponItems == null)
        {
            return;
        }
        foreach(var equipeditem in ItemInventoryManager.instance.equipWeaponItems)
        {
            if(equipeditem != null && equipeditem.itemInfo.itemSort == InfoType.Sword)
            {
                int swordIndex = equipeditem.itemInfo.slotNumber-1;
                if (previousEquippedWeapon == null || previousEquippedWeapon.itemInfo != equipeditem.itemInfo)
                {
                    for(int i =0; i<swords.Length; i++)
                    {
                        swords[i].SetActive(i== swordIndex);
                    }
                    previousEquippedWeapon = equipeditem;
                }
            }
            if (equipeditem != null && equipeditem.itemInfo.itemSort == InfoType.Shield)
            {
                int shieldIndex = equipeditem.itemInfo.slotNumber - 1;
                if (previousEquippedShield == null || previousEquippedShield.itemInfo != equipeditem.itemInfo)
                {
                    for (int i = 0; i < shields.Length; i++)
                    {
                        swords[i].SetActive(i == shieldIndex);
                    }
                    previousEquippedShield = equipeditem;
                }
            }
        }
    }
    protected virtual void equipItem(CharacterEquipInventoryItemBase equipitem)
    {
    }
}