using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventoryPopup : MonoBehaviour
{
    public Text skillName;
    public Text simpleSkillinfo;
    public Text detailSkillInfo;
    LaunchSkillGachaPopup launchSkillgachaPopup;
    public SkillDB skilldb;
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
    public void RunSkillGacha(int count)
    {
        if (launchSkillgachaPopup == null)
        {
            launchSkillgachaPopup = Instantiate(PopupFactory.instance.skillGachaPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<LaunchSkillGachaPopup>();
        }
        else
        {
            launchSkillgachaPopup.OpenPopupUI();
        }
        SkillGachaResult skillGachaResult = SkillGachaCalculator.Calculate(skilldb, count);
        foreach (var item in skillGachaResult.skillInfos)
        {
            PlayerData.instance.playerSkillInventoryData[item.number-1].SetData(item);
        }
        launchSkillgachaPopup.Initialize(skillGachaResult, this.RunSkillGacha);
        GameManager.instance.StartGacha();
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
