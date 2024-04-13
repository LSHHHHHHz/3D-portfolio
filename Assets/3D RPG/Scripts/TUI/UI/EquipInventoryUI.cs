using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInventoryUI : MonoBehaviour, IPopup
{
    private EquipmentData equipmentData;
    public Transform slotsParent;
    private void Start()
    {
        equipmentData = UserData.instance.equipmentData;
        RefreshSlots();
    }

    private void RefreshSlots()
    {
        for(int i =0; i< slotsParent.childCount; i++)
        {
            SlotUI slotUI = slotsParent.GetChild(i).GetComponent<SlotUI>();
            if(slotUI != null)
            {
                slotUI.SetData(equipmentData.slotDatas[i], equipmentData);
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
