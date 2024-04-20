using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData
{
    public static PlayerData instance;
    public UserData UserData;
    public PlayerItemInventoryData[] playerItemInventoryData;
    public PlayerIngamePortionData[] playerIngamePortionData;
    public PlayerEquipData[] playerEquipData;
    public PlayerSkillInventoryData[] playerSkillInventoryData;
    public PlayerIngameSkillData[] playerIngameSkillData;
    private PlayerData()
    {
        playerItemInventoryData = new PlayerItemInventoryData[28];
        playerIngamePortionData = new PlayerIngamePortionData[2];
        playerEquipData = new PlayerEquipData[2];
        playerSkillInventoryData = new PlayerSkillInventoryData[8];
        playerIngameSkillData = new PlayerIngameSkillData[6];

    }

    public void Save()
    {

    }

    public static void Load()
    {
        if (instance == null)
        {
            instance = new PlayerData();
        }
    }

}