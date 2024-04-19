using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using Newtonsoft.Json;
using System;
using Unity.VisualScripting;

[Serializable]
public class EnemyData
{
    public static EnemyData _instance;
    public static EnemyData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemyData();
            }
            return _instance;
        }
    }
    public List<NormarMonster> normarMonster;
    public List<BossMonster> bossMonster;
    private EnemyData()
    {
        normarMonster = new List<NormarMonster>
        {
            new NormarMonster("그루트", "", 300,10,10),
            new NormarMonster("그루트병사", "", 500,15,15),
            new NormarMonster("그루트대장", "", 900,20,20),
        };
        bossMonster = new List<BossMonster>
        {
            new BossMonster("보스보스", "", 2222,100,50)
        };
    }
    public void Save()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        string path = Path.Combine(Application.dataPath, "EnemyData.json");
        File.WriteAllText(path, jsonData);
    }

    public void Load()
    {
        string path = Path.Combine(Application.dataPath, "EnemyData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, Instance);
        }
    }

}