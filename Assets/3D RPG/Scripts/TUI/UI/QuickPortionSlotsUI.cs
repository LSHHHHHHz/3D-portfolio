using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickPortionSlotsUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotsTransform; 
    private QuickPortionSlotData quickPortionSlotData;
    private List<GameObject> slotsObject;

    private void Start()
    {
        slotsObject= new List<GameObject>();
        quickPortionSlotData = UserData.instance.quickPortionSlotData;
        RefreshSlots();
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
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
}
