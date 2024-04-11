using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSetEquipItem : MonoBehaviour
{
    public GameObject[] swords;
    public GameObject[] shields;
    private ItemInfo previousEquippedWeapon = null;
    private ItemInfo previousEquippedShield = null;
    public CharacterStatusBase characterStatus;
    private int previousWeaponAttack = 0;
    private int previousShieldAttack = 0;
    private void Start()
    {
        GameManager.instance.ChangeItemSlot += RefreshWeapon;
    }
    void RefreshWeapon()
    {
        if (PlayerData.instance.playerEquipData == null)
        {
            return;
        }
        foreach(var equipItem in PlayerData.instance.playerEquipData)
        {
            if (equipItem.item != null && equipItem.item.itemInfo.itemType == InfoType.Sword)
            {
                int swordIndex = equipItem.item.itemInfo.slotNumber - 1;
                if (previousEquippedWeapon == null || previousEquippedWeapon != equipItem.item.itemInfo)
                {
                    for (int i = 0; i < swords.Length; i++)
                    {
                        swords[i].SetActive(i == swordIndex);
                    }
                    if (previousEquippedWeapon != null)
                    {
                        previousWeaponAttack = previousEquippedWeapon.additionalAttack;
                    }
                    previousEquippedWeapon = equipItem.item.itemInfo;
                    SetEquipItem();
                }
            }
            if (equipItem.item != null && equipItem.item.itemInfo.itemType == InfoType.Shield)
            {
                int swordIndex = equipItem.item.itemInfo.slotNumber - 5;
                if (previousEquippedShield == null || previousEquippedShield != equipItem.item.itemInfo)
                {
                    for (int i = 0; i < shields.Length; i++)
                    {
                        shields[i].SetActive(i == swordIndex);
                    }
                    previousEquippedShield = equipItem.item.itemInfo;
                }
            }
            if (equipItem.item == null && equipItem.type == InfoType.Sword)
            {
                previousEquippedWeapon = null;
                SetEquipItem();
            }
        }
    }
    public void SetEquipItem()
    {
        if (previousEquippedWeapon != null)
        {
            characterStatus.UpdateTotalAttack(-previousWeaponAttack);
        }

        if (previousEquippedWeapon != null)
        {
            characterStatus.UpdateTotalAttack(previousEquippedWeapon.additionalAttack);
            previousWeaponAttack = previousEquippedWeapon.additionalAttack;
        }

        else
        {
            characterStatus.UpdateTotalAttack(0);
        }
    }
}