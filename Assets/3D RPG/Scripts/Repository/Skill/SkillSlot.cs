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
    public int skillSlotNumber;
    public Sprite nullImage;

    public void SetData(SkillInfo skillinfo)
    {
        this.skillInfo= skillinfo;
        if(skillinfo ==null )
        {
            return;
        }
        icon.sprite = skillinfo.iconImage; 
    }
    public void SetData(SkillInstance skillInstance)
    {
        if(skillInstance ==null )
        {
            return;
        }
        SetData(skillInstance.skillInfo);
        if(countText !=null)
        {
            countText.gameObject.SetActive(true);
            countText.text = skillInstance.count.ToString();
        }
    }
    public void ClearData()
    {
        skillInfo = null;
        icon.sprite = nullImage;
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }
    public bool IsEmpty()
    {
        return skillInfo == null;
    }
}
