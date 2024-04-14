using Assets._3D_RPG.Scripts.TUI.Inventory.DragAndDrop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventoryUI : MonoBehaviour,IPopup
{
    private SkillInventoryData skillInventoryDatas;
    public Transform slotsParent;
    private void Start()
    {
        skillInventoryDatas = UserData.instance.skillInventoryData;
        RefreshSlots();
    }

    private void RefreshSlots()
    {
        for(int i =0; i< skillInventoryDatas.slotDatas.Count; i++)
        {
            skillInventoryDatas.slotDatas[i].item = UserData.instance.skillData[i];
        }

        for (int i = 0; i < slotsParent.childCount; i++)
        {
            Transform child = slotsParent.GetChild(i);
            SkillSlotUI slotUI = child.GetComponentInChildren<SkillSlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(skillInventoryDatas.slotDatas[i], skillInventoryDatas);
            }
        }
    }

    public void OpenPopupUI()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }
}
