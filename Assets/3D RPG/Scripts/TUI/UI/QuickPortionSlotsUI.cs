using Assets._3D_RPG.Scripts.TUI.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickPortionSlotsUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public PlayerStatus playerStatus;
    public RectTransform slotsTransform; 
    private QuickPortionSlotData quickPortionSlotData;
    private List<GameObject> slotsObject;

    private Action Actionaction;
    private void Start()
    {
        slotsObject= new List<GameObject>();
        quickPortionSlotData = UserData.instance.quickPortionSlotData;
        RefreshSlots();
        SetChildItemSlots(slotsTransform);
    }
    private void RefreshSlots()
    {
        for (int i = slotsObject.Count; i < quickPortionSlotData.slotDatas.Count; i++)
        {
            GameObject slotObject = Instantiate(slotPrefab, slotsTransform);
            slotsObject.Add(slotObject);
        }
        for (int i = 0; i < quickPortionSlotData.slotDatas.Count; i++)
        {
            SlotUI slotUI = slotsObject[i].GetComponentInChildren<SlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(quickPortionSlotData.slotDatas[i], quickPortionSlotData);
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
                Button slotButton = slot.GetComponent<Button>();
                slot.endDragSlot += (slotData) => SetSlotData(slotData, slotIndex);
                slot.OnDropSlot += (slotData) => SetSlotData(slotData, slotIndex);
                slotButton.onClick.AddListener(() =>
                {
                    ClickPortionQuickSlot(slot, slotIndex);
                });
            }
        }
    }
    void SetSlotData(SlotData slot, int num)
    {
        for (int i = 0; i < quickPortionSlotData.slotDatas.Count; i++)
        {
            if (i == num)
            {
                quickPortionSlotData.slotDatas[i] = slot;
            }
        }
        RefreshSlots();
    }
    void ClickPortionQuickSlot(SlotUI slot, int slotNum)
    {
        Debug.Log(slotNum);
        if (slotNum < 0 || slotNum >= quickPortionSlotData.slotDatas.Count)
        {
            return;
        }

        if (quickPortionSlotData.slotDatas[slotNum].item is PortionData portion && !slot.activeCoolDown)
        {
            slot.CoolDown(portion.coolDown);
            if (portion.hpRecovery > 0)
            {
                playerStatus.RecoveryHP(portion.hpRecovery);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.CunsumePortion);
            }
            if(portion.mpRecovery > 0)
            {
                playerStatus.RecoveryMP(portion.mpRecovery);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.CunsumePortion);
            }
            if (quickPortionSlotData.slotDatas[slotNum].count > 0)
            {
                quickPortionSlotData.slotDatas[slotNum].count--;
            }
            else
            {
                quickPortionSlotData.slotDatas[slotNum].RemoveItem();
            }
            RefreshSlots();
        }
            
    }
}
