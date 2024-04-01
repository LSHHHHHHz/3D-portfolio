using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/SkillDB")]
public class SkillDB : ScriptableObject
{
    public List<SkillInfo> skillDB = new List<SkillInfo>();

    public List<SkillInfo> GetSkillByGrade(SkillGrade skillGrade)
    {
        List<SkillInfo> result = new List<SkillInfo>();
        foreach (SkillInfo skillInfo in skillDB)
        {
            if(skillInfo.skillGrade == skillGrade)
            {
                result.Add(skillInfo);
            }
        }

        return result;
    }
}
