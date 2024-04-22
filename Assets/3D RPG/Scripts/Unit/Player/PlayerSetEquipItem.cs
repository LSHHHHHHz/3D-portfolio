using Assets._3D_RPG.Scripts.TUI.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSetEquipItem : MonoBehaviour
{
    public GameObject[] swords;
    public GameObject[] shields;
    public CharacterStatusBase characterStatus;
    private void Start()
    {
        EventManager.instance.changeItemSlot += RefreshWeapon;
    }
    void RefreshWeapon(ItemData item)
    {
        if (UserData.instance.equipmentData == null)
        {
            return;
        }
        if(UserData.instance.equipmentData.slotDatas[0].item is EquipData equipSwordData)
        {
            if(equipSwordData == item)
            {
                EquipItem(equipSwordData, swords, new EquipSwordEvent(equipSwordData.addAttack));
            }
        }
        if (UserData.instance.equipmentData.slotDatas[1].item is EquipData equipShieldData)
        {
            if (equipShieldData == item)
            {
                EquipItem(equipShieldData, shields, new EquipShieldEvent(equipShieldData.addHp));
            }
        }
        for(int i =0; i< UserData.instance.equipmentData.slotDatas.Count; i++)
        {
            if(UserData.instance.equipmentData.slotDatas[i].item.iconPath == "")
            {
                if(i==0)
                {
                    UnEquipItem(swords, new EquipSwordEvent(0));
                }
                if(i==1)
                {
                    UnEquipItem(shields, new EquipShieldEvent(0));
                }
            }
        }
    }

    void EquipItem(EquipData equipData, GameObject[] items, IEvent eventSource)
    {
        IActor actor = characterStatus.GetComponent<IActor>();
        for(int i =0; i < items.Length;i++)
        {
            if (i == equipData.itemNum)
            {
                items[i].SetActive(true);
                eventSource.ExcuteEvent(actor);
            }
            else
            {
                items[i].SetActive(false);
            }
        }        
    }
    void UnEquipItem(GameObject[] items, IEvent eventSource)
    {
        IActor actor = characterStatus.GetComponent<IActor>();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);            
        }
        eventSource.ExcuteEvent(actor);
    }
}
