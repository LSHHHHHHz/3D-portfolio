using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/SkillDB")]
public class SkillDB : ScriptableObject
{
    List<SkillInfo> skillDB = new List<SkillInfo>();
}
