using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillInfo skillInfo;
    public Image icon;
    public Text countText;

    public void SetData(SkillInfo skillinfo)
    {
        this.skillInfo= skillinfo;
        icon.sprite = skillinfo.iconImage; 
    }
    public void SetData(SkillInstance skillInstance)
    {
        SetData(skillInstance.skillInfo);
        if(countText !=null)
        {
            countText.gameObject.SetActive(true);
            countText.text = skillInstance.count.ToString();
        }
    }
}
