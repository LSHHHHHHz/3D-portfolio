using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class SelectIngameSlotDataPopup : MonoBehaviour, IPopup
{
    ItemInfo itemInfo;
    SkillInfo skillInfo;

    private void OnDisable()
    {
        itemInfo = null;
        skillInfo = null;
    }
    public void SetItemInfo(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
    }
    public void SetSkillInfo(SkillInfo skillInfo)
    {
        this.skillInfo = skillInfo;
    }

    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenPopupUI()
    {
        gameObject.SetActive(true);
    }
}
