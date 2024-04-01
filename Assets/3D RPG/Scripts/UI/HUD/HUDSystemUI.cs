using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HUDSystemUI : MonoBehaviour
{
    SkillInventoryPopup skillInventoryPopup;
    SkillGachaPopup skillgachaPopup;
    public SkillDB skilldb;
    public void RunItemGacha()
    {
        if (skillInventoryPopup == null)
        {
            skillInventoryPopup = Instantiate(PopupFactory.instance.skillInventoryPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<SkillInventoryPopup>();
        }
        else
        {
            skillInventoryPopup.gameObject.SetActive(true);
        }
    }
    public void RunSkillGacha(int count)
    {
        if(skillgachaPopup == null)
        {
            skillgachaPopup = Instantiate(PopupFactory.instance.skillGachaPopupPrefab, PopupFactory.instance.gachaTransform).GetComponent<SkillGachaPopup>();
        }
        SkillGachaResult skillGachaResult = SkillGachaCalculator.Calculate(skilldb, count);
        foreach (var item in skillGachaResult.skillInfos)
        {
            SkillInventoryManager.instance.AddSkill(item);
        }
        SkillInventoryManager.instance.Save();
        skillgachaPopup.Initialize(skillGachaResult, this.RunSkillGacha);
    }
}
