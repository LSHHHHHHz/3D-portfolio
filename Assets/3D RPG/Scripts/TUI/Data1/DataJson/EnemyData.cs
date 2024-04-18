using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using Newtonsoft.Json;
using System;

[Serializable]
public class EnemyData : MonoBehaviour
{
    public static EnemyData instance;

    public List<NormarMonster> normarMonster;
    public List<BossMonster> bossMonster;
    [ContextMenu("Save To Json Data")]
    private void Awake()
    {
        instance = this;
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

    [ContextMenu("Load From Json Data")]
    public void Load()
    {
        string path = Path.Combine(Application.dataPath, "EnemyData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, instance);
        }
    }

}