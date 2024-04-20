using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class IngameSkillInventory : SkillInventoryBase
{
    SetBaseSkill[] skills;
    private void Awake()
    {
        SetData(PlayerData.instance);
    }
    private void OnEnable()
    {
        GameManager.instance.SetSkillInventory += RefreshData;
        GameManager.instance.SetSkillInventory += RefreshSkill;
    }
    private void RefreshData()
    {
        Refresh();
    }
    public override void SetData(PlayerData data)
    {
        playerData = data;
        playerSkillData = GetSkillInventoryDataByType(inventoryType);
        skillInventorySlots = GetChildSkillSlots(inventorySlotGrid);
        InitializeSkillInventoryData(inventoryType, skillInventorySlots.Length);
        Refresh();
    }
    public override SkillSlot[] GetChildSkillSlots(RectTransform parent)
    {
        List<SkillSlot> slots = new List<SkillSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform imageTransform = parent.GetChild(i);
            SkillSlot slot = imageTransform.GetComponentInChildren<SkillSlot>();
            if (slot != null)
            {
                int slotIndex = i;
                Button slotButton = slot.GetComponent<Button>();
                slot.beingDragSlot += () => SelectDragData(slotIndex);
                slot.OnDropSlot += () => SelectDropData(slotIndex);
                slot.OnDropSlotForRemove += () => RemoveData(slotIndex);
                slotButton.onClick.AddListener(() =>
                {
                    Debug.Log("스킬 공격");
                    ExecuteSkill(slotIndex, slot.skillInfo.skillDamage);
                });
            }
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    public void SelectDragData(int num)
    {
        DragAndDropManager.instance.dragData = playerSkillData[num];
        DragAndDropManager.instance.dragInventoryType = InventoryType.IngameSkillInventory;
    }
    public void SelectDropData(int num)
    {
        DragAndDropManager.instance.dropData = playerSkillData[num];
        DragAndDropManager.instance.dropInventoryType = InventoryType.IngameSkillInventory;
    }
    public void RemoveData(int num)
    {
        playerSkillData[num].ClearData();
        GameManager.instance.SetSkillI();
    }
    private void RefreshSkill()
    {
        ClearSkills();
        List<SetBaseSkill> skills = new();
        SetBaseSkill(skills);
        this.skills = skills.ToArray();
    }
    void SetBaseSkill(List<SetBaseSkill> skillsList)
    {
        foreach (var skill in PlayerData.instance.playerIngameSkillData)
        {
            if (skill.skill == null)
            {
                skillsList.Add(null);
            }
            else
            {
                SetBaseSkill skillBase = Instantiate(skill.skill.skillInfo.baseSkill).GetComponent<SetBaseSkill>();
                skillsList.Add(skillBase);
            }
        }
    }
    private void ClearSkills()
    {
        if (skills == null)
            return;

        foreach (var skill in skills)
        {
            if (skill == null)
                continue;

            Destroy(skill.gameObject);
        }
    }
    private void ExecuteSkill(int index, int damage)
    {
        if (index >= 0 && index < skills.Length)
        {
            SetBaseSkill skill = skills[index];
            if (skill != null)
            {
            }
        }
    }
}


