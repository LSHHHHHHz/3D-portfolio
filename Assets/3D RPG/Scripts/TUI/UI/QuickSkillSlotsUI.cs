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
            SlotUI slotUI = slotsObject[i].GetComponentInChildren<SlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(quickSkillSlotData.slotDatas[i], quickSkillSlotData);
            }
        }
    }
    private void SetBaseSkill(List<SetBaseSkill> skillList)
    {
        foreach(var skill in UserData.instance.quickSkillSlotData.slotDatas)
        {
            if(skill.item != null)
            {

            }
            else
            {
                string path = (skill.item as SkillData).prefabPath;
                SetBaseSkill skillBase = Instantiate(Resources.Load<Sprite>(path)).GetComponent<SetBaseSkill>();
                skillList.Add(skillBase);
            }
        }
    }
}
