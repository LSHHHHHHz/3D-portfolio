using Assets._3D_RPG.Scripts.TUI.Inventory.DragAndDrop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventoryUI : MonoBehaviour,IPopup
{
    private SkillInventoryData skillInventoryDatas;
    public RectTransform slotsParent;
    private void Start()
    {
        skillInventoryDatas = UserData.instance.skillInventoryData;
        RefreshSlots();
        SetChildSkillSlots(slotsParent);
    }

    private void RefreshSlots()
    {
        for(int i =0; i< skillInventoryDatas.slotDatas.Count; i++)
        {
            skillInventoryDatas.slotDatas[i].item = StaticData.instance.skillData[i];
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
    private void SetChildSkillSlots(RectTransform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform imageTransform = parent.GetChild(i);
            SkillSlotUI slot = imageTransform.GetComponentInChildren<SkillSlotUI>();
            if (slot != null)
            {
                int slotIndex = i;
                Button slotButton = slot.GetComponent<Button>();
                slotButton.onClick.AddListener(() =>
                {
                    Debug.Log("여긴 구현 안함 삭제 예정");
                });
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
