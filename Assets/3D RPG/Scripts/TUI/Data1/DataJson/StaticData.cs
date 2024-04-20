using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using Newtonsoft.Json;
using System;

[Serializable]
public class StaticData
{
    private static StaticData _instance;
    public static StaticData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StaticData();
            }
            else
            {
                _instance = new StaticData(); 
            }
            return _instance;
        }
    }
    public ShopData shopData;
    public List<PortionData> portionShopData;
    public List<EquipData> equipShopData;
    public List<SkillData> skillData;
    private StaticData()
    {
        portionShopData = new List<PortionData> {
            new PortionData("Portion/HpPortion1", "기초 HP 포션", "체력이 100만큼 회복됩니다.","Portion",100,0, 5,100),
            new PortionData("Portion/HpPortion2", "기본 HP 포션" ,"체력이 300만큼 회복됩니다.", "Portion",300,0,5,300),
            new PortionData("Portion/HpPortion3", "중급 HP 포션", "체력이 500만큼 회복됩니다.","Portion",500,0,5,500),
            new PortionData("Portion/HpPortion4", "고급 HP 포션", "체력이 1000만큼 회복됩니다.","Portion",1000,0,5,1000),
            new PortionData("Portion/MpPortion1", "기초 MP 포션", "마력이 10만큼 회복됩니다.","Portion",0,10,5,100),
            new PortionData("Portion/MpPortion2", "기본 MP 포션", "마력이 30만큼 회복됩니다.","Portion",0,30,5,300),
            new PortionData("Portion/MpPortion3", "중급 MP 포션", "마력이 50만큼 회복됩니다.","Portion",0,50,5,500),
            new PortionData("Portion/MpPortion4", "고급 MP 포션", "마력이 100만큼 회복됩니다.","Portion",0,100,5,1000)
        };
        equipShopData = new List<EquipData> {
            new EquipData("EquipIcon/Sword1", "기초 무기","공격력 100을 증가시킵니다.","Equip",100,0, 100,0),
            new EquipData("EquipIcon/Sword2", "기본 무기","공격력 200을 증가시킵니다.","Equip",200,0, 200,1),
            new EquipData("EquipIcon/Sword3", "중급 무기","공격력 300을 증가시킵니다.","Equip",300,0, 300,2),
            new EquipData("EquipIcon/Sword4", "고급 무기","공격력 400을 증가시킵니다.","Equip",400,0, 400,3),
            new EquipData("EquipIcon/Shield1", "기초 방패","체력 100을 증가시킵니다.","Equip",0,100, 100,0),
            new EquipData("EquipIcon/Shield2", "기본 방패","체력 200을 증가시킵니다.","Equip",0,200, 200,1),
            new EquipData("EquipIcon/Shield3", "중급 방패","체력 300을 증가시킵니다.","Equip",0,300, 300,2),
            new EquipData("EquipIcon/Shield4", "고급 방패","체력 400을 증가시킵니다.","Equip",0,400, 400,3)
        };
        skillData = new List<SkillData> {
            new SkillData("SkillIcon/Skill1", "크래쉬", "순식간에 적의 급소를 벤다.\n 쿨다운 10초", "Skill",1000,10,10,"SkillIcon/EffectSkill1"),
            new SkillData("SkillIcon/Skill2", "원기옥", "응축된 힘으로 적을 증발시킨다..\n 쿨다운 10초", "Skill",1000,10,10,"SkillIcon/EffectSkill2"),
            new SkillData("SkillIcon/Skill3", "유도미사일", "10개의 에너지바 적을 공격한다.\n 쿨다운 10초", "Skill",10,10,10,"SkillIcon/EffectSkill3"),
            new SkillData("SkillIcon/Skill4", "범위공격", "범위 내에 모든 적을 공격한다.\n 쿨다운 10초", "Skill",100,10,10,"SkillIcon/EffectSkill4"),
            new SkillData("SkillIcon/Skill5", "크래쉬", "순식간에 적의 급소를 벤다.\n 쿨다운 10초", "Skill",1000,10,10,""),
            new SkillData("SkillIcon/Skill6", "크래쉬", "순식간에 적의 급소를 벤다.\n 쿨다운 10초", "Skill",1000,10,10,""),
            new SkillData("SkillIcon/Skill7", "크래쉬", "순식간에 적의 급소를 벤다.\n 쿨다운 10초", "Skill",1000,10,10,""),

        };
        shopData = new ShopData();
}
    public void Save()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        string path = Path.Combine(Application.dataPath, "StaticData.json");
        File.WriteAllText(path, jsonData);
    }

    public void Load()
    {
        string path = Path.Combine(Application.dataPath, "StaticData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, Instance);
        }
    }

}