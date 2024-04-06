using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 1. �����Ϳ� UI 
// ������ �����ϴ� Ŭ���� 

// UI�� ����ϴ� Ŭ���� (InventoryPopup) 
// - �⺻������ UI�� ����ϴ� Ŭ�������� �����͸� �����ϴ� Ŭ�����κ��� �����͸� �޾ƿͼ� UI ���� 


// �巡�� ������ UI / Draggable 
// ����� ������ UI / Droppable (���� ĭ) 
// �巡�׾ص���� �����ϴ� �ھ� Ŭ���� 
// - �ھ� Ŭ������ �˾ƾ� �� �� Draggable, Droppable 
// 1. Ŭ���� ���� �� �巡�� ������ UI���� Ȯ�� 
// 2. �巡�� ������ UI��� ���콺�� UI ���� 
// 3. ����� �� �� �ش� ��ġ�� ����� ������ UI���� Ȯ�� 
// 4. ����� ������ UI��� ��� �������� ��û (ex. �ھ� Ŭ���� -> Droppable��, InventorySlot : Droppable 
// 5. ��� ������ �Ǹ� ���ʿ� ��� �ߴٰ� ��Ƽ�� ���ְ� ���콺�� �����ִ� Draggable�� ���� 
// 6. Droppable (ex. InventorySlot) 

//public class UIInventoryPopup
//{
//    public void SetData(ItemInventoryData data)
//    {
        
//    }

//    public void Refresh()
//    {
//        // Refresh InventoryDataMaanger���� - ������ �޾ƿ��� 
//        //InventoryDataManager.Instance;

//        // ������ ������� UI ���� (ex. ���� ä���) 


//        InventoryDataManager.Instance.RemoveItem();
//    }
//}

//public class InventoryDataManager
//{
//    public static InventoryDataManager Instance = new InventoryDataManager();
//    public void AddItem()
//    {

//    }
//    public void RemoveItem()
//    {
        
//    }
//}


//public class InventoryData
//{
//    public List<InventoryItemData> InventoryItems = new List<InventoryItemData>();

//    public void Modify()
//    {
        
//    }

//    public void AddItem()
//    {
        
//    }

//    public InventoryItemData GetItem(int index)
//    {
//        return InventoryItems[index];
//    }

//    public void SetItem(int index, InventoryItemData item)
//    {
//        if (index >= 0 && index < InventoryItems.Count)
//        {
//            InventoryItems[index] = item;
//        }
//        else
//        {
//        }
//    }
//    public void RemoveItem()
//    {
        
//    }

//    public void Clear()
//    {
        
//    }


//    public void Save()
//    {
//        // ���� ����� ������ �����ϱ�
//    }

//    public static InventoryData Load()
//    {
//        // ���� ����� �������� �ε� �ϱ�
//        return new InventoryData();
//    }
//}

//public class InventoryItemData
//{
//    public string Name;
//}

public class ItemInventoryManager : MonoBehaviour
{
    public static ItemInventoryManager instance;
    public ItemInfo selectedShopItemInfoData;
    public ItemInfo selectedInventoryItemInfoData;

    public event Action OnItemInventoryChanged;
    public event Action OnEquippedIngamePortionItemChanged;
    public event Action CharacterEquipInventoryItemBaseUpdate;

    public List<ItemInstance> myItems = new();
    public List<ItemInstance> equipPortionItems = new();
    public List<ItemInstance> equipWeaponItems = new();
    public List<ItemInstance>[] equipColleagueWeaponItems;
    public CharacterEquipInventoryItemBase characterEquipInventoryItemBase;
    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InitializeEquippedPortionSlots();
    }
    private void InitializeEquippedPortionSlots()
    {
        for (int i = 0; i < EquipSlotIndicateManager.instance.ingamePortionSlotsParent.transform.childCount; i++)
        {
            equipPortionItems.Add(new ItemInstance());
        }
        equipColleagueWeaponItems = new List<ItemInstance>[3];
        for (int i = 0; i < equipColleagueWeaponItems.Length; i++)
        {
            equipColleagueWeaponItems[i] = new List<ItemInstance>();
        }
    }
    public void AddItem(ItemInfo itemInfo)
    {
        ItemInstance existItem = myItems.Find(item => item.itemInfo == itemInfo);
        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            myItems.Add(new ItemInstance()
            {
                itemInfo = itemInfo,
                count = 1,
            });
        }
        OnItemInventoryChanged?.Invoke();
    }

    //�κ��丮�˾����� ����
    public void AddItem(ItemInfo itemInfo, int count)
    {
        bool added = false;
        for (int i = 0; i < myItems.Count; i++)
        {
            if (myItems[i] == null || myItems[i].itemInfo == null)
            {
                myItems[i] = new ItemInstance
                {
                    itemInfo = itemInfo,
                    count = count
                };
                added = true;
                break;
            }
        }

        if (!added)
        {
            myItems.Add(new ItemInstance
            {
                itemInfo = itemInfo,
                count = count
            });
        }

        OnItemInventoryChanged?.Invoke();
    }
    public void OnDragMergeItems(ItemSlot dropItemSlot, ItemSlot dragItemSlot)
    {
        ItemInstance dropItem = null;
        ItemInstance dragItem = null;
        for (int i = 0; i < myItems.Count; i++)
        {
            if (i == dropItemSlot.slotNumber)
            {
                dropItem = myItems[i];
            }
            if (i == dragItemSlot.slotNumber)
            {
                dragItem = myItems[i];
            }
        }
        dropItem.count += dragItem.count;
        myItems[dragItemSlot.slotNumber] = null;
        OnItemInventoryChanged?.Invoke();
    }
    public void OnDragTempItemsInInventory(ItemSlot dropItemSlot, ItemSlot dragItemSlot)
    {
        if (dropItemSlot.slotNumber < myItems.Count && dragItemSlot.slotNumber < myItems.Count)
        {
            ItemInstance temp = myItems[dropItemSlot.slotNumber];
            myItems[dropItemSlot.slotNumber] = myItems[dragItemSlot.slotNumber];
            myItems[dragItemSlot.slotNumber] = temp;

            OnItemInventoryChanged?.Invoke();
        }
    }
    public void ClearSlotData(ItemSlot slot, SlotParentType type)
    {
        switch (type)
        {
            case SlotParentType.InGamePortionInventory:
                for (int i = 0; i < equipPortionItems.Count; i++)
                {
                    if (i == slot.slotNumber)
                    {
                        equipPortionItems[i].itemInfo = null;
                        OnEquippedIngamePortionItemChanged?.Invoke();
                        return;
                    }
                }
                break;
            case SlotParentType.ItemInventory:
                for (int i = 0; i < myItems.Count; i++)
                {
                    if (i == slot.slotNumber)
                    {
                        myItems[i].itemInfo = null;
                        OnItemInventoryChanged?.Invoke();
                        return;
                    }
                }
                break;
        }
    }
    public void SetEquipPortion(ItemInfo info, int num, int inventoryItemSlotNum)
    {
        ItemInstance setItem = null;
        for (int i = 0; i < myItems.Count; i++)
        {
            if (i == inventoryItemSlotNum)
            {
                setItem = myItems[i];
                myItems[i] = null;
            }
        }
        if (info.itemSort == InfoType.HPPortion || info.itemSort == InfoType.MPPortion)
        {
            for (int i = 0; i < equipPortionItems.Count; i++)
            {
                if (i == num)
                {
                    equipPortionItems[i] = setItem;
                }
            }
        }
        OnItemInventoryChanged?.Invoke();
        OnEquippedIngamePortionItemChanged?.Invoke();
    }
    public void SetEquipWeapon(ItemInfo selectIteminfo, int inventoryItemSlotNum, ItemInfo preItemInfo)
    {
        ItemInstance setItem = null;
        ItemInstance preiteminfo = null;
        //���� �������� �ִٸ� �� ���� ����, ������ null
        if (preItemInfo != null)
        {
            preiteminfo = new ItemInstance
            {
                itemInfo = preItemInfo,
                count = 1
            };
        }
        else
        {
            preiteminfo = null;
        }
        //�κ��丮 �����ۿ��� ���� ������ �������� setItem�� ����
        //������ ������ 1�� ���ϸ� ���� �� null�� �ٲ�
        //������ ������ 1�� �ʰ��� ���� �� ���� -1
        for (int i = 0; i < myItems.Count; i++)
        {
            if (i == inventoryItemSlotNum)
            {
                setItem = new ItemInstance
                {
                    itemInfo = myItems[i].itemInfo,
                    count = 1
                };
                if (myItems[i].count <=1)
                {
                    myItems[i] = null;
                }
                else
                {
                    myItems[i] = new ItemInstance
                    {
                        itemInfo = myItems[i].itemInfo,
                        count = myItems[i].count - 1
                    };
                }
            }
        }

        for(int i =0; i< myItems.Count; i++)
        {
            if(myItems[i] == null)
            {
                myItems[i] = preiteminfo;
                break;
            }
        }
        characterEquipInventoryItemBase.EquipItem(setItem, selectIteminfo.itemSort);
        OnItemInventoryChanged?.Invoke();
    }
}
