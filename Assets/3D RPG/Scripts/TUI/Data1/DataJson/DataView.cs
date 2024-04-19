using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using Newtonsoft.Json;

public class DataView : MonoBehaviour
{
    private static DataView _instance;
    public static DataView Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataView>();
                if (_instance == null)
                {
                    GameObject container = new GameObject("DataView");
                    _instance = container.AddComponent<DataView>();
                }
            }
            return _instance;
        }
    }
    public StaticData staticData;
    public EnemyData enemyData;

    private void Start()
    {
        Load();
        staticData = StaticData.Instance;
        enemyData = EnemyData.Instance;
    }

    [ContextMenu("Save All Data")]
    public void Save()
    {
        StaticData.Instance.Save();
        EnemyData.Instance.Save();
    }

    [ContextMenu("Load All Data")]
    public void Load()
    {
        StaticData.Instance.Load();
        EnemyData.Instance.Load();
    }
}