using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public int skillID;
    public string skillName;
    public string skillDescription;
    public int damage;
    public int coolDown;
    public GameObject effectPrefab;
}