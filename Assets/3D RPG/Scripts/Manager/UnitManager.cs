using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    public Player player;
    public Colleague[] colleague;

    private void Awake()
    {
        instance = this;
    }
}
