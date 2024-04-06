using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ItemInventory : UIInventory
{
    public RectTransform slotsParent;
    public GameObject portionSlotsParent;
    public GameObject indicateParent;
    ItemSlot[] itemInventorySlots;
    ItemInstance firstClickedItem;
    ItemInstance secondClickedItem;

    public bool dataDirty = false;
    private void Awake()
    {
        itemInventorySlots = GetChildItemSlots(slotsParent);
        SetData(PlayerData.instance);

        // load data 
    }

    public void Update()
    {
        /* if (dataDirty)
         {
             dataDirty = false;
             data1.sa();
         }*/
    }
    public override void Refresh()
    {
        if (playerData != null && playerData.inventoryData != null)
        {
            foreach (var item in playerData.inventoryData.items)
            {
                // 슬롯 프리팹으로부터 새 슬롯 인스턴스 생성
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // item 정보를 바탕으로 슬롯을 구성
                // slot.GetComponent<ItemSlot>().Setup(item);
            }
        }
    }
    ItemSlot[] GetChildItemSlots(RectTransform parent)
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            ItemSlot slot = parent.GetChild(i).GetComponent<ItemSlot>();

            // Slot 초기화 할 때 
            slot.onContentChanged += () => OnSlotChanged(i);
            slot.onClicked += () => OnClickedSlot(i);
            slot.onTouched += () => FirstTouchSlot(i);
            slot.onTouched += () => SecondTouchSlot(i);

            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener(() =>
            {
                if (slot.itemInfo != null)
                {
                    SelectedSlotInfoManager.instance.SetItemInfo(slot.itemInfo, slot.slotNumber);
                    EquipSlotIndicateManager.instance.SetIngameSlotIndicate(slot.itemInfo.itemSort);
                }
            });
            slots.Add(slot);
        }
        return slots.ToArray();
    }

    /*  private void OnSlotChanged(int slotIndex)
      {
          // 한 프레임에 1000번 수정이 일어나면 - 그때그때 저장 불가
          // 그 때 필요한 게 isDirty 

          dataDirty = true;
          this.data.Set, Modify, Remove 

      }*/
    private void OnSlotChanged(int slotIndex)
    {
    }
    private void OnClickedSlot(int slotIndex)
    {
        playerData.inventoryData.GetItem(slotIndex);
    }
    private void FirstTouchSlot(int slotIndex)
    {
        if (firstClickedItem == null)
        {
            firstClickedItem = playerData.inventoryData.GetItem(slotIndex);
        }
    }
    private void SecondTouchSlot(int slotIndex)
    {
        if (firstClickedItem != null)
        {
            secondClickedItem = playerData.inventoryData.GetItem(slotIndex);
        }
    }
}
