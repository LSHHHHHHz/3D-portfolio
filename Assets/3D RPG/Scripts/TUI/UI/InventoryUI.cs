using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IPopup
{
    public GameObject slotPrefab;
    public RectTransform slotsTransform;
    private InventoryData inventoryData;
    private List<GameObject> slotsObject;
    private EquipInventoryUI equipInventoryPopup;

    private void Start()
    {
        slotsObject = new List<GameObject>();
        inventoryData = UserData.instance.inventoryData;
        RefreshData();
        SetChildItemSlots(slotsTransform);
    }
    private void OnEnable()
    {
        GameManager.instance.OnItemPurchased += RefreshData;
    }
    private void OnDisable()
    {
        GameManager.instance.OnItemPurchased -= RefreshData;
    }
    private void RefreshData()
    {
        RefreshSlots();
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
                slotUI.SetData(inventoryData.slotDatas[i], inventoryData);
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
                // slot.beingDragSlot += () => RemoveSlotData(slotIndex);
                slot.endDragSlot += (slotData) => SetSlotData(slotData,slotIndex);
                slot.OnDropSlot += (slotData) => SetSlotData(slotData, slotIndex);
                slotButton.onClick.AddListener(() =>
                {
                    Debug.Log("여긴 구현 안함 삭제 예정");
                });
            }
        }
    }
    void SetSlotData(SlotData slot, int num)
    {
        for (int i = 0; i < inventoryData.slotDatas.Count; i++)
        {
            if (i == num)
            {
                inventoryData.slotDatas[i] = slot;
                break;
            }
        }
        RefreshData();
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
        if (equipInventoryPopup != null)
        {
            equipInventoryPopup.gameObject.SetActive(false);
        }
    }
}
