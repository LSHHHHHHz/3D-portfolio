using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadSceneController.LoadScene("Scene2");
        }
    }
}
