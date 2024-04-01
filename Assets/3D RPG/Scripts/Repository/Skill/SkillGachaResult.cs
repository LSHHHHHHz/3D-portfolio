using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillGachaResult
{
    public List<SkillInfo> skillInfos = new List<SkillInfo>();
}
internal class SkillGachaCalculator
{
    public static SkillGachaResult Calculate(SkillDB skillDB, int count)
    {
        SkillGachaResult result = new SkillGachaResult();
        List<SkillInfo> AtypeSkill = skillDB.GetSkillByGrade(SkillGrade.A);
        List<SkillInfo> BtypeSkill = skillDB.GetSkillByGrade(SkillGrade.B);
        List<SkillInfo> CtypeSkill = skillDB.GetSkillByGrade(SkillGrade.C);
        List<SkillInfo> DtypeSkill = skillDB.GetSkillByGrade(SkillGrade.D);
        for (int i = 0; i < count; i++)
        {
            float roll = UnityEngine.Random.Range(0, 1f);
            SkillInfo selected;
            if (roll < 0.015) //1.5%
            {
                selected = AtypeSkill[UnityEngine.Random.Range(0, AtypeSkill.Count)];
            }
            else if (roll < 0.05) // 3.5%
            {
                selected = BtypeSkill[UnityEngine.Random.Range(0, BtypeSkill.Count)];
            }
            else if (roll < 0.2) //15 %
            {
                selected = CtypeSkill[UnityEngine.Random.Range(0, CtypeSkill.Count)];
            }
            else //85%
            {
                selected = DtypeSkill[UnityEngine.Random.Range(0, DtypeSkill.Count)];

            }
            result.skillInfos.Add(selected);
        }

        return result;
    }
}
