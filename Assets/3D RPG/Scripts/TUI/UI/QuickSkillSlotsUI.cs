using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSkillSlotsUI : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public GameObject slotPrefab;
    public RectTransform slotsTransform;
    private QuickSkillSlotData quickSkillSlotData;
    private List<GameObject> slotsObject;
    SetBaseSkill[] skills;
    SlotData slotData;
    private void Start()
    {
        slotsObject = new List<GameObject>();
        quickSkillSlotData = UserData.instance.quickSkillSlotData;
        RefreshData();
    }
    private void RefreshData()
    {
        RefreshSlots();
        SetChildSkillSlots(slotsTransform);
    }
    private void RefreshSlots()
    {
        for (int i = slotsObject.Count; i < quickSkillSlotData.slotDatas.Count; i++)
        {
            GameObject slotObject = Instantiate(slotPrefab, slotsTransform);
            slotsObject.Add(slotObject);
        }
        for (int i = 0; i < quickSkillSlotData.slotDatas.Count; i++)
        {
            SkillSlotUI slotUI = slotsObject[i].GetComponentInChildren<SkillSlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(quickSkillSlotData.slotDatas[i], quickSkillSlotData);
            }
        }
    }
    private void SetChildSkillSlots(RectTransform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform imageTransform = parent.GetChild(i);
            SkillSlotUI slot = imageTransform.GetComponentInChildren<SkillSlotUI>();
            if (slot != null)
            {
                int slotIndex = i;
                Button slotButton = slot.GetComponent<Button>();
                slot.OnDropSlot += (slotData) => SetSlotData(slotData, slotIndex);
                slotButton.onClick.AddListener(() =>
                {
                    ClickSkillQuickSlot(slot, slotIndex);
                });
            }
        }
    }
    void SetSlotData(SlotData slot, int num)
    {
        for (int i = 0; i < quickSkillSlotData.slotDatas.Count; i++)
        {
            if (quickSkillSlotData.slotDatas[i].item.itemName == slot.item.itemName)
            {
                return;
            }
        }
        for (int i = 0; i < quickSkillSlotData.slotDatas.Count; i++)
        {
            if (i == num)
            {
                quickSkillSlotData.slotDatas[i].item = slot.item;
            }
        }
        RefreshSetQuilSlotSkill();
        RefreshData();
    }
    private void RefreshSetQuilSlotSkill()
    {
        ClearSkills();
        List<SetBaseSkill> skills = new();
        SetBaseSkill(skills);
        this.skills = skills.ToArray();
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
    private void SetBaseSkill(List<SetBaseSkill> skillList)
    {
        foreach (var skill in UserData.instance.quickSkillSlotData.slotDatas)
        {
            if (skill.item != null && skill.item is SkillData skillData)
            {
                string path = skillData.prefabPath;
                if (skill.item.itemName == "")
                {
                    skillList.Add(null);
                }
                else
                {
                    if (path == "")
                    {
                        skillList.Add(null);
                    }
                    else
                    {
                        SetBaseSkill skillBase = Instantiate(Resources.Load<GameObject>(path)).GetComponent<SetBaseSkill>();
                        skillList.Add(skillBase);
                    }
                }
            }
            if (skill.item.iconPath == "")
            {
                skillList.Add(null);
            }
        }

    }
    void ClickSkillQuickSlot(SkillSlotUI slot, int slotNum)
    {
        if (slotNum < 0 || slotNum >= quickSkillSlotData.slotDatas.Count)
        {
            return;
        }
        if (slot.currentSlotData.item.itemName != "" && !slot.activeCoolDown && quickSkillSlotData.slotDatas[slotNum].item is SkillData skillData)
        {
            slot.CoolDown(skillData.coolDown);
            SetBaseSkill skill = skills[slotNum];
            if (skill != null)
            {
                playerStatus.ConsumMP(skillData.consumMP);
                skill.Execute(playerStatus, skillData.damage);
            }
        }
        RefreshSlots();
    }
}
