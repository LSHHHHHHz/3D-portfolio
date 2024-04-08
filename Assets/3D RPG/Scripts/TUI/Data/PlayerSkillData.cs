using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerSkillData : IPlayerData
{
    public SkillInstance skill = null;

    public object GetData()
    {
        return skill;
    }
    public void SetData(object newData)
    {
        skill = newData as SkillInstance;
    }
    public void ClearData()
    {
        skill = null;
    }
    public void RemoveData(int count)
    {
        skill.count -= count;
    }
}
public class PlayerSkillInventoryData : PlayerSkillData
{
}
public class PlayerIngameSkillData : PlayerSkillData
{
}