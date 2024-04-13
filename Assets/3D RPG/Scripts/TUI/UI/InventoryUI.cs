using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour,IPopup
{
    public GameObject slotPrefab;
    public Transform slotsTransform; 
    private InventoryData inventoryData;
    private List<GameObject> slotsObject;
    private EquipInventoryUI equipInventoryPopup;

    private void Start()
    {
        slotsObject= new List<GameObject>();
        inventoryData = UserData.instance.inventoryData;
        RefreshSlots();
    }
    private void OnEnable()
    {
        GameManager.instance.OnItemPurchased += RefreshSlots;
    }
    private void OnDisable()
    {
        GameManager.instance.OnItemPurchased -= RefreshSlots;
    }
    private void RefreshSlots()
    {
        for (int i = slotsObject.Count; i < inventoryData.slotDatas.Count; i++)
        {
            GameObject slotObject = Instantiate(slotPrefab, slotsTransform);
            slotsObject.Add(slotObject);
        }
        for (int i = 0; i < inventoryData.slotDatas.Count; i++)
        {
            SlotUI slotUI = slotsObject[i].GetComponentInChildren<SlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(inventoryData.slotDatas[i],inventoryData);
            }
        }
    }
    public void EquipInventoryOpen()
    {
        if (equipInventoryPopup == null)
        {
            equipInventoryPopup = Instantiate(PopupFactory.instance.equipInventoryPopupPrefab, PopupFactory.instance.popupTransForm).GetComponent<EquipInventoryUI>();
        }
        else
        {
            equipInventoryPopup.OpenPopupUI();
        }
    }
    public void OpenPopupUI()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
        equipInventoryPopup.gameObject.SetActive(false);
    }
}
