using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerSkillData : IPlayerData
{
    public SkillInstance skill = null;
    public InfoType type;

    public object GetData()
    {
        return skill;
    }
    public void SetData(object newData)
    {
        skill = newData as SkillInstance;
    }
    public void SetData(SkillInfo info)
    {
        if (skill == null)
        {
            skill = new SkillInstance
            {
                skillInfo = info
            ,
                count = 1,
                upgradeLevel = 1
            };
        }
        else
        {
            skill.count++;
        }
    }

    public void ClearData()
    {
        skill = null;
    }
    public void RemoveData(int count)
    {
        skill.count -= count;
    }
    public InfoType GetItemType()
    {
        return type;
    }
}
public class PlayerSkillInventoryData : PlayerSkillData
{
}
public class PlayerIngameSkillData : PlayerSkillData
{
}