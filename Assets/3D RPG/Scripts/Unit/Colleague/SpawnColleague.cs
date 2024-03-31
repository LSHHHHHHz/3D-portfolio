using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class SpawnColleague : MonoBehaviour
{
    public Transform colleagueTransform;
    ColleagueStatus[] colleagueStaus;
    private void Awake()
    {
        colleagueStaus = new ColleagueStatus[3];
    }
    public void SpawnAccompanyColleague(int index)
    {
        ColleagueStatus newColleagueStatus = UnitManager.instance.colleague[index].GetComponent<ColleagueStatus>();
        string newColleagueName = newColleagueStatus.characterName;
        for (int i = 0; i < colleagueStaus.Length; i++)
        {
            if (colleagueStaus[i] != null && colleagueStaus[i].characterName == newColleagueName)
            {
                return;
            }
        }
        for (int i = 0; i < colleagueStaus.Length; i++)
        {
            if (colleagueStaus[i] == null) 
            {
                colleagueStaus[i] = Instantiate(UnitManager.instance.colleague[index], colleagueTransform).GetComponent<ColleagueStatus>();
                return; 
            }
        }
    }
}
