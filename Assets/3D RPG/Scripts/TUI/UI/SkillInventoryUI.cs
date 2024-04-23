using Assets._3D_RPG.Scripts.TUI.Inventory.DragAndDrop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventoryUI : MonoBehaviour,IPopup
{
    private SkillInventoryData skillInventoryDatas;
    public RectTransform slotsParent;

    public Text skillName;
    public Text skillDescription;
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
            skillInventoryDatas.slotDatas[i].item = StaticData.Instance.skillData[i];
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
                slot.viewSkillInfo += ViewSkilInfo;
                slotButton.onClick.AddListener(() =>
                {
                    ViewSkilInfo(slot.currentSlotData.item.itemName, slot.currentSlotData.item.itemDescription);
                });
            }
        }
    }
    void ViewSkilInfo(string name, string description)
    {
        skillName.text = name;
        skillDescription.text = description;
    }
    public void OpenPopupUI()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClosePopup);
    }
}
