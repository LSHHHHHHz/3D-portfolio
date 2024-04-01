using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SkillInventoryData
{
    public List<SkillInstance> mySkills = new();
    public List<SkillInstance> equippedSkills = new();
}
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

    public void ChangeEquipSkillSet(List<SkillInfo> skillList)
    {
        var equippedSkillList = equippedSkills;

        equippedSkillList.Clear();
        foreach (SkillInfo skillInfo in skillList)
        {
            SkillInstance existItem = mySkills.Find(item => item.skillInfo == skillInfo); //
            equippedSkillList.Add(existItem);
        }

        OnEquippedSkillsChanged?.Invoke();

        Save();
    }

    public void Save()
    {
        var skillInventoryData = new SkillInventoryData();
        skillInventoryData.mySkills = mySkills;
        skillInventoryData.equippedSkills = equippedSkills;

        string json = JsonUtility.ToJson(skillInventoryData);

        PlayerPrefs.SetString("SkillInventoryData", json);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        string json = PlayerPrefs.GetString("SkillInventoryData");

        if (string.IsNullOrEmpty(json) == false)
        {
            var data = JsonUtility.FromJson<SkillInventoryData>(json);
            for (int i = 0; i < data.mySkills.Count; ++i)
            {
                var item = data.mySkills[i];
                if (item.skillInfo == null)
                    continue;

                mySkills.Add(item);
            }

            for (int i = 0; i < data.equippedSkills.Count; ++i)
            {
                var item = data.equippedSkills[i];
                if (item.skillInfo == null)
                    continue;

                equippedSkills.Add(item);
            }
        }

        OnEquippedSkillsChanged?.Invoke();
    }
}
