using Assets._3D_RPG.Scripts.TUI.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using Newtonsoft.Json;

public class DataView : MonoBehaviour
{
    public static DataView instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Load();
    }

    [ContextMenu("Save All Data")]
    public void Save()
    {
        StaticData.instance.Save(); 
        EnemyData.instance.Save(); 
    }

    [ContextMenu("Load All Data")]
    public void Load()
    {
        StaticData.instance.Load(); 
        EnemyData.instance.Load(); 
    }
}