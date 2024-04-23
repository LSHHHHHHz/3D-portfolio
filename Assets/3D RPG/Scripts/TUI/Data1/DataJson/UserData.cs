using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using Newtonsoft.Json;

public class UserDataViewer : MonoBehaviour
{
    public UserData data;
}
public class UserData : MonoBehaviour
{
    public static UserData instance;

    public InventoryData inventoryData;
    public EquipmentData equipmentData;
    public QuickPortionSlotData quickPortionSlotData;
    public QuickSkillSlotData quickSkillSlotData;
    public SkillInventoryData skillInventoryData;
    [ContextMenu("Save To Json Data")]
    private void Awake()
    {
        instance = this;
    }
    public void Save()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        string path = Path.Combine(Application.dataPath, "UserData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("Load From Json Data")]
    public void Load()
    {
        string path = Path.Combine(Application.dataPath, "UserData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, instance);
        }
    }

}