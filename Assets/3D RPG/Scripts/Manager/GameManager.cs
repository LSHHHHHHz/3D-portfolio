using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action OnItemPurchased;
    public event Action ChangeItemSlot;
    public event Action StartGachaPopup;
    public event Action SetSkillInventory;
    public GameObject[] cameras;

    public GameObject TextBoss;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        PlayerData.Load();
    }
    private void Start()
    {
        AudioManager.instance.PlayBGM(0, true);
    }
    public void ItemPurchased()
    {
        OnItemPurchased?.Invoke();
    }
    public void ChangeSlot()
    {
        ChangeItemSlot?.Invoke();
    }
    public void StartGacha()
    {
        StartGachaPopup?.Invoke();
    }
    public void SetSkillI()
    {
        SetSkillInventory?.Invoke();
    }
    public void ChangeCameraTarget(int num)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == num)
            {
                cameras[i].gameObject.SetActive(true);
                cameras[i].GetComponent<AudioListener>().enabled = true;
            }
            else
            {
                cameras[i].gameObject.SetActive(false); 
                cameras[i].GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    public void TestBoss()
    {
        TextBoss.SetActive(true);
    }
}
