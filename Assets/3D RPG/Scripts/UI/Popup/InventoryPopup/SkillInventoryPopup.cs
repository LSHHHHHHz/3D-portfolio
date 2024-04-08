using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventoryPopup : MonoBehaviour
{
    public RectTransform skillSlotsParent;
    public Text skillName;
    public Text simpleSkillinfo;
    public Text detailSkillInfo;
    SkillSlot[] skillInventorySlots;
    SkillGachaPopup skillgachaPopup;
    public SkillDB skilldb;

    private void Awake()
    {
        skillInventorySlots = GetChildSkillSlots(skillSlotsParent);
    }
    private void Start()
    {
        SkillInventoryManager.instance.OnSkillInventoryChanged += OnSkillInventoryChangedCallback;
        SetData();
    }
    SkillSlot[] GetChildSkillSlots(RectTransform parent)
    {
        List<SkillSlot> slots = new List<SkillSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            SkillSlot slot = parent.GetChild(i).GetComponent<SkillSlot>();
            var button = slot.GetComponent<Button>();
            button.onClick.AddListener((UnityEngine.Events.UnityAction)(() =>
            {
                //버튼 클릭 시 장착이나 데이터 전송, 강화 팝업
                SelectedSlotInfoManager.instance.SetSkillInfo(slot.skillInfo);
                EquipSlotIndicateManager.instance.SetIngameSlotIndicate(InfoType.Skill);
                UpdateSkillInfo(slot.skillInfo);
                ClickInventorySkillSlot(slot);
            }));
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    void ClickInventorySkillSlot(SkillSlot slot)
    {
        SkillInventoryManager.instance.skillSlotData = slot;
    }
    void UpdateSkillInfo(SkillInfo Info)
    {
        skillName.text = Info.skillName;
        simpleSkillinfo.text = Info.simpleSkillinfo;
        detailSkillInfo.text = Info.detailSkillInfo;
    }
    void OnSkillInventoryChangedCallback()
    {
        SetData();
    }
    void SetData()
    {
        foreach (SkillInstance skill in SkillInventoryManager.instance.mySkills)
        {
            int number = skill.skillInfo.number;
            SkillSlot slot = skillInventorySlots[number - 1];
            slot.SetData(skill);
        }
    }
    public void RunSkillGacha(int count)
    {
        if (skillgachaPopup == null)
        {
            skillgachaPopup = Instantiate(PopupFactory.instance.skillGachaPopupPrefab, PopupFactory.instance.gachaTransform).GetComponent<SkillGachaPopup>();
        }
        SkillGachaResult skillGachaResult = SkillGachaCalculator.Calculate(skilldb, count);
        foreach (var item in skillGachaResult.skillInfos)
        {
            SkillInventoryManager.instance.AddSkill(item);
        }
        skillgachaPopup.Initialize(skillGachaResult, this.RunSkillGacha);
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
