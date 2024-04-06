using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;
using UnityEngine.UI;
public class IngameSkillSlots : MonoBehaviour
{
    public RectTransform slotsParent;
    SkillSlot[] ingameSkillslots;

    private void Awake()
    {
        ingameSkillslots = GetChildItemSlots(slotsParent);
        GetSlotNumber();
    }
    private void Start()
    {
        SetData();
        SkillInventoryManager.instance.OnEquippedSkillsChanged += SetData;
    }
    SkillSlot[] GetChildItemSlots(RectTransform parent)
    {
        List<SkillSlot> slots = new List<SkillSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            SkillSlot slot = parent.GetChild(i).GetComponent<SkillSlot>();
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(() =>
            {
                EquipIngameSkillSlot(slot);
            });
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    void SetData()
    {
        foreach (SkillSlot slot in ingameSkillslots)
        {
            slot.ClearData();
        }
        for (int i = 0; i < SkillInventoryManager.instance.equippedSkills.Count; i++)
        {
            SkillInstance instance = SkillInventoryManager.instance.equippedSkills[i];
            SkillSlot slot = ingameSkillslots[i];
            if (slot.IsEmpty())
            {
                slot.SetData(instance);
            }
        }
    }
    void EquipIngameSkillSlot(SkillSlot slot)
    {
        if (SelectedSlotInfoManager.instance.selectedSkillInfo != null)
        {
            bool matchFound = false;
            for (int i = 0; i < ingameSkillslots.Length; i++)
            {
                if (ingameSkillslots[i].skillInfo == SelectedSlotInfoManager.instance.selectedSkillInfo)
                {
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound)
            {
                SkillInventoryManager.instance.SetEquipSkill(SelectedSlotInfoManager.instance.selectedSkillInfo, slot.skillSlotNumber);
                //slot.SetData(GameManager.instance.selectedSkillInfo);
            }
            SelectedSlotInfoManager.instance.ClearSkillInfo();
            EquipSlotIndicateManager.instance.ReverseSetIngameSlotIndicate(InfoType.Skill);
        }
    }
    void GetSlotNumber()
    {
        for (int i = 0; i < ingameSkillslots.Length; i++)
        {
            ingameSkillslots[i].skillSlotNumber = i;
        }
    }
}
