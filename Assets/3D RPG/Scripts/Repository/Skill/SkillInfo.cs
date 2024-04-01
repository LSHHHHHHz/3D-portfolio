using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SkillGrade
{
    D,
    C,
    B,
    A
}
[CreateAssetMenu(menuName = "My Assets/SkillInfo")]

public class SkillInfo : ScriptableObject
{
    public SkillGrade skillGrade;
    public string skillName;
    public string simpleSkillinfo;
    public string detailSkillInfo;
    public Sprite iconImage;
    public int number;
}
