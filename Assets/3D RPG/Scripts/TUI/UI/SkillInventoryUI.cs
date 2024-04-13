using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventoryUI : MonoBehaviour,IPopup
{
    private SkillInventoryData skillInventoryData;
    public Transform slotsParent;
    private void Start()
    {
        skillInventoryData = UserData.instance.skillInventoryData;
        RefreshSlots();
    }

    private void RefreshSlots()
    {
        for(int i =0; i< slotsParent.childCount; i++)
        {
            SlotUI slotUI = slotsParent.GetChild(i).GetComponent<SlotUI>();
            if(slotUI != null)
            {
                slotUI.SetData(skillInventoryData.slotDatas[i], skillInventoryData);
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
