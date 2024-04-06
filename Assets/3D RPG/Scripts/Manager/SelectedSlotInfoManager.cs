using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SelectedSlotInfoManager : MonoBehaviour
{
    public static SelectedSlotInfoManager instance;
    public ItemInfo selectedItemInfo;
    public int selectedItemInfoNum;
    public SkillInfo selectedSkillInfo;

    private void Awake()
    {
        instance = this;
    }
    public void SetItemInfo(ItemInfo info, int num)
    {
        selectedItemInfo = info;
        selectedItemInfoNum = num;
    }
    public void ClearItemInfo()
    {
        selectedItemInfo = null;
    }
    public void SetSkillInfo(SkillInfo info)
    {
        selectedSkillInfo = info;
    }
    public void ClearSkillInfo()
    {
        selectedSkillInfo = null;
    }
}
