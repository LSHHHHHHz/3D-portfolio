using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserData
{
    public static UserData instance;

    public InventoryData inventoryData;
    public EquipmentData equipmentData;
    public QuickSlotData quickSlotData;

    public void Save()
    {
        // ������ ����
    }

    public static UserData Load()
    {
        // ������ �ҷ�����
        return new UserData();
    }

}