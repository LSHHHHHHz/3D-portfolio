using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventoryManager : MonoBehaviour
{
    public static SkillInventoryManager instance;
    public event Action OnSkillInventoryChanged;
    public event Action OnEquippedSkillsChanged;

    public List<SkillInstance> mySkills = new();
    public List<SkillInstance> equippedSkills = new();
    public SkillSlot skillSlotData;
    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InitializeEquippedSkillSlots();
    }
    private void InitializeEquippedSkillSlots()
    {
        for (int i = 0; i < EquipSlotIndicateManager.instance.ingameSkillSlotsParent.transform.childCount; i++)
        {
            equippedSkills.Add(new SkillInstance()); 
        }
    }
    public void AddSkill(SkillInfo skillInfo)
    {
        SkillInstance existItem = mySkills.Find(item => item.skillInfo == skillInfo);

        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            mySkills.Add(new SkillInstance()
            {
                skillInfo = skillInfo,
                count = 1,
                upgradeLevel = 1
            });
        }

        OnSkillInventoryChanged?.Invoke();
    }

    public void SetEquipSkill(SkillInfo info, int num)
    {
        SkillInstance setSkill = new SkillInstance
        {
            skillInfo = info,
            count = 1,
            upgradeLevel = 1
        };
        for (int i =0; i<equippedSkills.Count; i++) 
        {
            if(i == num)
            {
                equippedSkills[i] = setSkill;
            }
        }
        OnEquippedSkillsChanged?.Invoke();
    }

    public void ClearSlotData(SkillSlot slot)
    {
        for (int i = 0; i < equippedSkills.Count; i++)
        {
            if (i == slot.skillSlotNumber)
            {
                equippedSkills[i] = null;
                OnEquippedSkillsChanged?.Invoke();
                return;
            }
        }
    }
}
