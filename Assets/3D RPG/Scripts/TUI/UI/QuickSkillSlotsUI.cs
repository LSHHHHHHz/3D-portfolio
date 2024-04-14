using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSkillSlotsUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotsTransform; 
    private QuickSkillSlotData quickSkillSlotData;
    private List<GameObject> slotsObject;
    SetBaseSkill[] skills;
    private void Start()
    {
        slotsObject= new List<GameObject>();
        quickSkillSlotData = UserData.instance.quickSkillSlotData;
        RefreshSlots();
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
    private void RefreshData()
    {
        RefreshSlots();
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
                slotUI.SetData(quickSkillSlotData.slotDatas[i],quickSkillSlotData);
            }
        }
    }
    private void RefreshSkill()
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
        foreach(var skill in UserData.instance.quickSkillSlotData.slotDatas)
        {
            if(skill.item != null)
            {
                skillList.Add(null);
            }
            else
            {
                string path = (skill.item as SkillData).prefabPath;
                SetBaseSkill skillBase = Instantiate(Resources.Load<Sprite>(path)).GetComponent<SetBaseSkill>();
                skillList.Add(skillBase);
            }
        }
    }
    private void ExecuteSkill(int index, int damage)
    {
        if (index >= 0 && index < skills.Length)
        {
            SetBaseSkill skill = skills[index];
            if (skill != null)
            {
                skill.Execute(damage);
            }
        }
    }
}
