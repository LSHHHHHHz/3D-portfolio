using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInventoryUI : MonoBehaviour, IPopup
{
    private EquipmentData equipmentData;
    public RectTransform slotsTransform;
    private void Start()
    {
        equipmentData = UserData.instance.equipmentData;
        SetChildItemSlots(slotsTransform);
        RefreshSlots();

    }
    private void RefreshSlots()
    {
        for(int i =0; i< slotsTransform.childCount; i++)
        {
            Transform imageTransform = slotsTransform.GetChild(i);
            SlotUI slotUI = imageTransform.GetComponentInChildren<SlotUI>();
            if(slotUI != null)
            {
                slotUI.SetData(equipmentData.slotDatas[i], equipmentData);
            }
        }
    }
    private void SetChildItemSlots(RectTransform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform imageTransform = parent.GetChild(i);
            SlotUI slot = imageTransform.GetComponentInChildren<SlotUI>();
            if (slot != null)
            {
                int slotIndex = i;
                slot.endDragSlot += (slotData) => SetSlotData(slotData, slotIndex);
                slot.OnDropSlot += (slotData) => SetSlotData(slotData, slotIndex);
            }
        }
    }
    void SetSlotData(SlotData slot, int num)
    {
        for (int i = 0; i < equipmentData.slotDatas.Count; i++)
        {
            if (i == num)
            {
                equipmentData.slotDatas[i]= slot;
            }
        }
        RefreshSlots();
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
