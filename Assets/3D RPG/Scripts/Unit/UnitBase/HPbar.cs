using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
public class HPbar : MonoBehaviour
{
    public CharacterStatusBase characterstatus;
    public Slider slider;
    public Slider backgroundSlider;
    public Transform character;

    private float lastHP;
    private float delay = 0.5f;
    private float delayTimer = 0;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        if (characterstatus != null)
        {
            lastHP = characterstatus.current_HP / characterstatus.max_HP;
            slider.value = lastHP;
            backgroundSlider.value = lastHP;
        }
    }
    private void Update()
    {
        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }

        transform.position = character.position + new Vector3(0,2.5f,0);

        if (characterstatus != null)
        {
            float currentHP = characterstatus.current_HP / characterstatus.max_HP;
            if (currentHP < lastHP)
            {
                delayTimer = delay; 
            }
            slider.value = Mathf.Lerp(slider.value, currentHP, Time.deltaTime * 8);
            if (delayTimer > 0)
            {
                delayTimer -= Time.deltaTime;
            }
            else
            {
                backgroundSlider.value = Mathf.Lerp(backgroundSlider.value, currentHP, Time.deltaTime * 2);
            }
            lastHP = currentHP;
        }
    }
}
