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
            new PortionData("Portion/HpPortion1", "���� HP ����", "ü���� 100��ŭ ȸ���˴ϴ�.","Portion",100,0, 5,100),
            new PortionData("Portion/HpPortion2", "�⺻ HP ����" ,"ü���� 300��ŭ ȸ���˴ϴ�.", "Portion",300,0,5,300),
            new PortionData("Portion/HpPortion3", "�߱� HP ����", "ü���� 500��ŭ ȸ���˴ϴ�.","Portion",500,0,5,500),
            new PortionData("Portion/HpPortion4", "��� HP ����", "ü���� 1000��ŭ ȸ���˴ϴ�.","Portion",1000,0,5,1000),
            new PortionData("Portion/MpPortion1", "���� MP ����", "������ 10��ŭ ȸ���˴ϴ�.","Portion",0,10,5,100),
            new PortionData("Portion/MpPortion2", "�⺻ MP ����", "������ 30��ŭ ȸ���˴ϴ�.","Portion",0,30,5,300),
            new PortionData("Portion/MpPortion3", "�߱� MP ����", "������ 50��ŭ ȸ���˴ϴ�.","Portion",0,50,5,500),
            new PortionData("Portion/MpPortion4", "��� MP ����", "������ 100��ŭ ȸ���˴ϴ�.","Portion",0,100,5,1000)
        };
        equipShopData = new List<EquipData> {
            new EquipData("EquipIcon/Sword1", "���� ����","���ݷ� 100�� ������ŵ�ϴ�.","Equip",100,0, 100),
            new EquipData("EquipIcon/Sword2", "�⺻ ����","���ݷ� 200�� ������ŵ�ϴ�.","Equip",200,0, 200),
            new EquipData("EquipIcon/Sword3", "�߱� ����","���ݷ� 300�� ������ŵ�ϴ�.","Equip",300,0, 300),
            new EquipData("EquipIcon/Sword4", "��� ����","���ݷ� 400�� ������ŵ�ϴ�.","Equip",400,0, 400),
            new EquipData("EquipIcon/Shield1", "���� ����","ü�� 100�� ������ŵ�ϴ�.","Equip",0,100, 100),
            new EquipData("EquipIcon/Shield2", "�⺻ ����","ü�� 200�� ������ŵ�ϴ�.","Equip",0,200, 200),
            new EquipData("EquipIcon/Shield3", "�߱� ����","ü�� 300�� ������ŵ�ϴ�.","Equip",0,300, 300),
            new EquipData("EquipIcon/Shield4", "��� ����","ü�� 400�� ������ŵ�ϴ�.","Equip",0,400, 400)
        };
        skillData = new List<SkillData> {
            new SkillData("SkillIcon/Skill1", "ũ����", "���İ��� ���� �޼Ҹ� ����.\n ��ٿ� 10��", "Skill",1000,10,10,"SkillIcon/EffectSkill1"),
            new SkillData("SkillIcon/Skill2", "�����", "����� ������ ���� ���߽�Ų��..\n ��ٿ� 10��", "Skill",1000,10,10,"SkillIcon/EffectSkill2"),
            new SkillData("SkillIcon/Skill3", "�����̻���", "10���� �������� ���� �����Ѵ�.\n ��ٿ� 10��", "Skill",10,10,10,"SkillIcon/EffectSkill3"),
            new SkillData("SkillIcon/Skill4", "��������", "���� ���� ��� ���� �����Ѵ�.\n ��ٿ� 10��", "Skill",100,10,10,"SkillIcon/EffectSkill4"),
            new SkillData("SkillIcon/Skill5", "ũ����", "���İ��� ���� �޼Ҹ� ����.\n ��ٿ� 10��", "Skill",1000,10,10,""),
            new SkillData("SkillIcon/Skill6", "ũ����", "���İ��� ���� �޼Ҹ� ����.\n ��ٿ� 10��", "Skill",1000,10,10,""),
            new SkillData("SkillIcon/Skill7", "ũ����", "���İ��� ���� �޼Ҹ� ����.\n ��ٿ� 10��", "Skill",1000,10,10,""),

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