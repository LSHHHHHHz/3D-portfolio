using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Canvas mainCanvas;
    public Canvas gachaCanavas;

    private void Awake()
    {
        instance = this;
    }
}
